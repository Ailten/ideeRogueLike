
public class DetailsCharacter : Layer
{
    private static DetailsCharacter _layer = new DetailsCharacter();
    public static DetailsCharacter layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        // TODO : print a layer like special room, with data of this.characterSelected .
        

        base.active();
    }


    public Character? characterSelected;


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        this.characterSelected = null;

        base.unActive();
    }

}