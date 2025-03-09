using Raylib_cs;

public class FxTurnOn : Fx
{

    public FxTurnOn(Vector pos) : base(SpriteType.FX_turnOn)
    {
        this.pos = pos;

        this.setTimeAnimeDelay(0.5f);
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if(i < 0f || i > 1f)
            return;

        //draw sprite with effect based on time i.

        Raylib.DrawTexturePro( // ------ TODO : finish and test.
            e.sprite.texture, //texture.
            rectSourceInTexture, //rect source from texture.
            rectDest, //rect desintation at screen.
            origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
            e.rotate, //rotation.
            Color.White //color (already white).
        );

    }

}