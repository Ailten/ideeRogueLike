
public class Cel : Entity
{

    public CelType celType;

    public Cel(CelType celType, int x, int y) : base(RunLayer.layer.idLayer, SpriteType.Cel)
    {

        this.isActive = false;

        this.size = new(126, 126);

        this.pos = new(
            x * this.size.x, 
            y * this.size.y
        );

        this.zIndex = 1000;

        this.geometryTrigger = new Rect(
            new(-64, -64), 
            new(126, 126)
        );

        this.celType = celType;
        updateStateCelType();
    }


    //draw over the cel.
    public override void drawAfter(Vector posToDraw)
    {
        if(celType == CelType.Cel) //skip draw effect if cel is basic.
            return;

        //TODO : draw sprite of effect cel base on type.
    }


    //update SpriteType based on CelType.
    private void updateStateCelType()
    {

        switch(celType){

            case(CelType.Cel):
                this.spriteType = SpriteType.Cel;
                break;

            //TODO : other sprite Cel.

        }

    }



}