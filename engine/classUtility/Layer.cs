
public class Layer
{

    public bool isActive = false;
    public int idLayer = -1;
    public string layerName = "";

    private List<Entity> _entities = new();
    public List<Entity> entities
    {
        get { return _entities; }
    }

    private int _milisecInLevel = 0;
    public int milisecInLevel
    {
        get { return _milisecInLevel; }
    }


    public Layer()
    {
        LayerManager.pushNewLayer(this);
    }

    //instantiate all entities for the layer.
    public virtual void active()
    {
        EntityManager.sortAllEntities(); //sort all entities (for new one instantiate).

        isActive = true; //switch bool active.
    }

    //event update for the layer.
    public virtual void update()
    {
        //increment milisecInLevel every update, if layer is active.
        if(isActive){
            _milisecInLevel += UpdateManager.deltaTime;
        }
    }

    //clean all entities of the layer.
    public virtual void unActive()
    {
        EntityManager.removeEntitiesOfLayer(idLayer); //drop all entity in layer (stay order).
        _entities = new(); //drop all entities in layer.

        isActive = false; //switch bool active.
    }

}

/* --- exemple.

public class MainMenu : Layer
{

    private static MainMenu _layer = new MainMenu(){ layerName="MainMenu" };
    public static MainMenu layer 
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        base.active();
    }

    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        base.unActive();
    }

}

*/