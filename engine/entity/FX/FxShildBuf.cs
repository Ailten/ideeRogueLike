using Raylib_cs;

public class FxShildBuf : Fx
{
    private bool isOneFxAtTimeInstance = false;
    public FxShildBuf(Vector pos, bool isOneFxAtTimeInstance = false) : base(SpriteType.FX_shildBuf)
    {
        this.pos = pos;

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.3f);

        this.isOneFxAtTimeInstance = isOneFxAtTimeInstance;
    }


    private static Vector baseSize = new Vector(126, 126);
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if (i < 0f || i > 1f)
        {
            if (i > 1f)
                if (this.isOneFxAtTimeInstance)
                    FxManager.endFxOnqueue();
            return;
        }

        //draw sprite with effect based on time i.

        Rectangle rectSourceFx = this.sprite.getSpriteTileBySpriteType(this.spriteType).getRectSource();
        Vector sizeAtScreenBase = baseSize * scale * CanvasManager.scaleCanvas * CameraManager.zoomCam;
        
        if (i >= 0.3f) //big shild.
        {
            float scaleFx = (i <= 0.8f ?
                Vector.lerpF(0.2f, 2.2f, Vector.reverceLerpF(0.3f, 0.8f, i)) : //scale up.
                Vector.lerpF(2.2f, 2f, Vector.reverceLerpF(0.8f, 1f, i)) //scale down.
            );
            Vector sizeAtScreenScaled = sizeAtScreenBase * scaleFx;
            Vector origineFx = sizeAtScreenScaled * encrage * scale * CanvasManager.scaleCanvas;

            Rectangle rectDestFx = new(
                posToDraw,
                sizeAtScreenScaled
            );

            Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceFx, //rect source from texture.
                rectDestFx, //rect desintation at screen.
                origineFx, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                0f, //rotation.
                Color.White //color (already white).
            );
        }

        if (true) //little shild.
        { 
            float scaleFx = (
                (i <= 0.5f) ? Vector.lerpF(0.1f, 1.8f, Vector.reverceLerpF(0f, 0.5f, i)) : //scale up.
                (i <= 0.7f) ? Vector.lerpF(1.8f, 1.6f, Vector.reverceLerpF(0.5f, 0.7f, i)) : //scale down.
                1.3f
            );

            Vector sizeAtScreenScaled = sizeAtScreenBase * scaleFx;
            Vector origineFx = sizeAtScreenScaled * encrage * scale * CanvasManager.scaleCanvas;

            Rectangle rectDestFx = new(
                posToDraw,
                sizeAtScreenScaled
            );

            Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceFx, //rect source from texture.
                rectDestFx, //rect desintation at screen.
                origineFx, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                0f, //rotation.
                Color.White //color (already white).
            );
        }
    }

}