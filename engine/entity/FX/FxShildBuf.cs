using Raylib_cs;

public class FxShildBuf : Fx
{
    public FxShildBuf(Vector pos) : base(SpriteType.FX_shildBuf)
    {
        this.pos = pos;

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.5f);
    }


    private static bool isOneFxAleadyInstanciate = false;

    // function for init one fx at time (cancel init if an fx of this type is already instanciate).
    public static FxShildBuf? initOnlyOneFxAtTime(Vector pos)
    {
        if (isOneFxAleadyInstanciate)
            return null;
        isOneFxAleadyInstanciate = true;
        return new FxShildBuf(pos);
    }


    private static Vector baseSize = new Vector(126, 126);
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if (i < 0f || i > 1f)
        {
            if (i > 1f)
                isOneFxAleadyInstanciate = false;
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

        if (i <= 0.7f) //little shild.
        {
            float scaleFx = (i <= 0.5f?
                Vector.lerpF(0.1f, 1.6f, Vector.reverceLerpF(0f, 0.5f, i)): //scale up.
                Vector.lerpF(1.6f, 1.3f, Vector.reverceLerpF(0.5f, 0.7f, i)) //scale down.
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