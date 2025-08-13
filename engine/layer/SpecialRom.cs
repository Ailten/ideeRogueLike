
public class SpecialRoom : Layer
{
    
    private static SpecialRoom _layer = new SpecialRoom();
    public static SpecialRoom layer 
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