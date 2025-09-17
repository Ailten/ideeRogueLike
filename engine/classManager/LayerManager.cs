
public static class LayerManager
{

    private static List<Layer> _layers = new(); //all layers instanciate.
    public static List<Layer> layers
    {
        get { return _layers; }
    }
    private static int layersIndexCount = 0;

    private static bool _isADetailsLayerAreOpen = false; // flag for check when a layer details or menu are open (only one active). 
    public static bool isADetailsLayerAreOpen
    {
        get { return _isADetailsLayerAreOpen; }
        set { _isADetailsLayerAreOpen = value; }
    }


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
    private static bool isMidTransitionSet;
    private static bool isMidTransitionPast;
    public static void transition(Action midAction)
    {
        transition(new int[]{}, new int[]{}, midAction);
    }
    public static void transition(int idLevelStart, int idLevelEnd, Action? midAction=null)
    {
        transition(new int[]{ idLevelStart }, new int[]{ idLevelEnd }, midAction);
    }
    public static void transition(int[] idLevelStart, int[] idLevelEnd, Action? midAction=null)
    {
        _isTransitionActive = true;
        idLevelStartTransition = idLevelStart;
        idLevelEndTransition = idLevelEnd;
        timeWhenStartTransition = UpdateManager.timeSpeedForAnime(UpdateManager.timeFromStartGame);
        _transitionOpacity = 0f;
        midTransitionAction = midAction;
        isMidTransitionSet = false;
        isMidTransitionPast = false;
    }
    public static void updateTransition()
    {
        if(!isTransitionActive) //skip if no transition active.
            return;

        const float timeOfFullTransition = 500f; //delay of full transition anime.
        int timeSpeeded = UpdateManager.timeSpeedForAnime(UpdateManager.timeFromStartGame); //time with speed game.
        float i = (float)(timeSpeeded - timeWhenStartTransition) / timeOfFullTransition; //interpolation of transation (0f~1f).

        //during transition, set opacity up or down.
        if(i < 1f){
            _transitionOpacity = ((!isMidTransitionPast)? i: 1f-i); //transition opacity.
            return;
        }
        
        //execute all process for switch layer and do action.
        if(!isMidTransitionSet){
            isMidTransitionSet = true;
            _transitionOpacity = 1f;
            foreach(int idLevelUnActive in idLevelStartTransition){ //disable all level for transition.
                getLayerByIdLayer(idLevelUnActive).unActive();
            }
            if(midTransitionAction != null){ //action in mid transition (if has one).
                midTransitionAction();
                midTransitionAction = null;
            }
            foreach (int idLevelActive in idLevelEndTransition)
            { //active all level for transition.
                getLayerByIdLayer(idLevelActive).active();
            }
            return;
        }

        //update time from start before execute an update (for action and update time).
        if(!isMidTransitionPast){
            isMidTransitionPast = true;
            timeWhenStartTransition = UpdateManager.timeFromStartGame;
            return;
        }

        //end transition.
        _isTransitionActive = false;

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
        foreach (Layer l in _layers)
        { //unActive all level active.
            if (l.isActive || l.entities.Count() != 0)
                l.unActive();
        }
    }

}