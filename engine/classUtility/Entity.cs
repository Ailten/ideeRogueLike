
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

    public virtual void drawAfter(Vector posToDraw, Rect rectDest, Vector origine){}

    //no need geometrySolid for this game.
    public Rect? geometryTrigger = null;
    public Rect geometryTriggerNN
    {
        get { return geometryTrigger ?? throw new Exception("GeometryTrigger is null !"); }
    }
    public Rect? geometryTriggerSecond = null; //second geometry, only for complex forme (like list card selected UI).
    public Rect geometryTriggerSecondNN
    {
        get { return geometryTriggerSecond ?? throw new Exception("GeometryTriggerSecond is null !"); }
    }

    public virtual void eventTriggerEnter(Entity entityEnter) { }

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
        this.size = new(0, 0);
    }


    //print all data of entity.
    public void debug(){
        Console.WriteLine(
            "[ entity : \n"+
            $"  isActive: {this.isActive}\n"+
            $"  idEntity: {this.idEntity}\n"+
            $"  idLayer: {this.idLayer}\n"+
            $"  pos: (x: {this.pos.x}; y: {this.pos.y})\n"+
            $"  size: (x: {this.size.x}; y: {this.size.y})\n"+
            $"  zIndex: {this.zIndex}\n"+
            $"  scale: (x: {this.scale.x}; y: {this.scale.y})\n"+
            $"  rotate: {this.rotate}\n"+
            $"  encrage: (x: {this.encrage.x}; y: {this.encrage.y})\n"+
            $"  spriteType: {this.spriteType}\n"+
            $"  sprite: {this.sprite.texture.ToString()}\n"+
            $"  geometryTrigger: {((this.geometryTrigger == null)? "null": "contain geometry")}\n"+
            $"  isUi: {this.isUi}\n"+
            "]"
        );
    }

}