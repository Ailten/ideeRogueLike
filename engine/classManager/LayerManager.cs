
public static class LayerManager
{

    private static List<Layer> _layers = new(); //all layers instanciate.
    public static List<Layer> layers
    {
        get { return _layers; }
    }
    private static int layersIndexCount = 0;


    //push a new layer in list layers (after init).
    public static void pushNewLayer(Layer l)
    {
        l.idLayer = layersIndexCount++; //set id unique.

        _layers.Add(l); //push in list entities.
    }

    //find a layer by his name.
    public static Layer getLayerByName(string layerName)
    {
        return _layers.Find((l) => l.layerName == layerName) ?? throw new Exception("Layer not found !");
    }

    //find a layer by his id.
    public static Layer getLayerByIdLayer(int idLayer)
    {
        return _layers.Find((l) => l.idLayer == idLayer) ?? throw new Exception("Layer not found !");
    }


    private static bool _isTransitionActive;
    public static bool isTransitionActive{
        get{ return _isTransitionActive; }
    }
    private static int[] idLevelStartTransition = new int[]{};
    private static int[] idLevelEndTransition = new int[]{};
    private static int timeWhenStartTransition;
    private static float _transitionOpacity;
    public static float transitionOpacity
    {
        get { return _transitionOpacity; }
    }
    private static Action? midTransitionAction = null;
    private static bool isMidTransitionPast;
    public static void transition(int idLevelStart, int idLevelEnd, Action? midAction=null)
    {
        transition(new int[]{ idLevelStart }, new int[]{ idLevelEnd }, midAction);
    }
    public static void transition(int[] idLevelStart, int[] idLevelEnd, Action? midAction=null)
    {
        _isTransitionActive = true;
        idLevelStartTransition = idLevelStart;
        idLevelEndTransition = idLevelEnd;
        timeWhenStartTransition = UpdateManager.timeFromStartGame;
        _transitionOpacity = 0f;
        midTransitionAction = midAction;
        isMidTransitionPast = false;
    }
    public static void updateTransition()
    {
        if(!isTransitionActive) //skip if no transition active.
            return;

        const float timeOfFullTransition = 1000f; //delay of full transition anime.

        float i = Math.Min((float)(UpdateManager.timeFromStartGame - timeWhenStartTransition) / timeOfFullTransition, 1f); //interpolation of transation (0f~1f).

        if(i == 1f){ //end transition.
            _isTransitionActive = false;
            return;
        }

        if(i >= 0.5f && !isMidTransitionPast){ //mid transition execution.
            isMidTransitionPast = true;
            _transitionOpacity = 1f;
            foreach(int idLevelUnActive in idLevelStartTransition){ //disable all level for transition.
                getLayerByIdLayer(idLevelUnActive).unActive();
            }
            foreach(int idLevelActive in idLevelEndTransition){ //active all level for transition.
                getLayerByIdLayer(idLevelActive).active();
            }
            if(midTransitionAction != null){ //action in mid transition (if has one).
                midTransitionAction();
                midTransitionAction = null;
            }
            return;
        }

        _transitionOpacity = ((i <= 0.5f)? //transition opacity.
            i*2f : //transition opacity 0 to 1.
            1f-(i-0.5f)*2f //transition opacity 1 to 0.
        );

    }


    //do update of layers active.
    public static void updateLayers()
    {
        if(isTransitionActive) //skip all update if in transition layers.
            return;

        layers.Where((l) =>  //filter on layers active.
            l.isActive 
        ).ToList().ForEach((l) =>  //execute update of layers.
            l.update() 
        );
    }


    //dehinit all entities in layers active (and hidde).
    public static void deinit()
    {
        foreach(Layer l in _layers){ //unActive all level active.
            if(l.isActive || l.entities.Count() != 0)
                l.unActive();
        }
    }

}