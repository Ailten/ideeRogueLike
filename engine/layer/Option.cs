
public class Option : Layer
{
    private static Option _layer = new Option();
    public static Option layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        // TODO. print many element over all other payer.
        

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