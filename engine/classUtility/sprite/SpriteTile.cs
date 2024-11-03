using Raylib_cs;

public class SpriteTile
{
    public SpriteType spriteType;
    public Vector posStart;
    public Vector size;


    public SpriteTile(SpriteType spriteType, Vector posStart, Vector size)
    {
        this.spriteType = spriteType;
        this.posStart = posStart;
        this.size = size;
    }


    //get rectangle of source.
    public Rectangle getRectSource()
    {
        return new Rectangle( //get rectangle source by state.
            posStart.x, posStart.y,
            size.x, size.y
        );
    }

    //get rect pixel source in ramp map (horizontal ramp). -- implemente when need.
    /*
    public Rectangle getRectSourceColorRamp(float i)
    {
        return new Rectangle( //get rectangle source by state.
            posStart.x + (int)(i*255), posStart.y,
            1, 1
        );
    }
    */

    //get rectange source depent on time loop anime. -- implemente when need.
    /*
    public Rectangle getRectSourceAnime()
    {
        
    }
    */

}