
public class MobType
{

    public SpriteType spriteType;
    public int dificulty;
    public bool isBoss;

    public MobType(SpriteType spriteType, int dificulty, bool isBoss)
    {
        this.spriteType = spriteType;
        this.dificulty = dificulty;
        this.isBoss = isBoss;
    }

}