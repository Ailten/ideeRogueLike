
public class Entity
{

    public bool isActive = true;
    public int idEntity = -1;
    public int idLayer = -1;

    public Vector pos = new();
    public Vector size = new(100, 100);

    public int zIndex = -1;

    public Vector scale = new(1, 1);
    public float rotate = 0;
    public Vector encrage = new(0.5f, 0.5f);

    public SpriteType spriteType = SpriteType.none;
    public Sprite sprite;

    public virtual void drawAfter(Vector posToDraw){}

    //TODO : geometrySolid & geometry Trigger.
    public Rect? geometryTrigger = null;
    public Rect geometryTriggerNN
    {
        get { return geometryTrigger ?? throw new Exception("GeometryTrigger is null !"); }
    }

    public virtual void eventTriggerEnter(Entity entityEnter){}

    public bool isUi = false;
    public virtual void eventMouseClick(bool isLeftClick, bool isClickDown){}
    public virtual void eventMouseEnter(){}
    public virtual void eventMouseExit(){}


    public Entity(int idLayer, SpriteType spriteType)
    {
        this.idLayer = idLayer;
        this.spriteType = spriteType;
        this.sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
        EntityManager.pushNewEntity(this);
    }

    //constructor for an entity without link to any layer and without sprite.
    public Entity(){
        this.sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
    }

}