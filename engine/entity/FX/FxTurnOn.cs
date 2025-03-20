using Raylib_cs;

public class FxTurnOn : Fx
{

    public FxTurnOn(Vector pos) : base(SpriteType.FX_turnOn)
    {
        this.pos = pos;

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.5f);
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if(i < 0f || i > 1f)
            return;

        //draw sprite with effect based on time i.

        // --- TODO : find why FX is not print (zindex fine, order entity fine, drawAfter call, pos to draw look fine, rotate no change).


        Vector sizeFx = new(63, 63);
        float iEulerAngleRotate = i * (float)(Math.PI * 2);
        Vector posWorldEncrageRotate = Vector.rotate(new Vector(-60, -60), iEulerAngleRotate);
        //posWorldEncrageRotate -= (sizeFx /2) * 0.0f; //no need replacement at all, because texture is already not centered.
        
        posWorldEncrageRotate *= CameraManager.zoomCam; //scale for zoom camera.
        posWorldEncrageRotate *= CanvasManager.scaleCanvas; //scale to canvas resized.

        posWorldEncrageRotate += posToDraw; //add pos entity base eval in canvasDraw.


        Rectangle rectSourceFx = this.sprite.getSpriteTileBySpriteType(this.spriteType).getRectSource();

        Vector sizeAtScreenFx = sizeFx * this.scale * CanvasManager.scaleCanvas * CameraManager.zoomCam;

        Rectangle rectDestFx = new Rectangle(
            posWorldEncrageRotate.x, posWorldEncrageRotate.y,
            sizeAtScreenFx.x, sizeAtScreenFx.y
        );

        Vector origineFx = this.encrage * sizeFx * this.scale * CanvasManager.scaleCanvas * CameraManager.zoomCam;

        Raylib.DrawTexturePro(
            this.sprite.texture, //texture.
            rectSourceFx, //rect source from texture.
            rectDestFx, //rect desintation at screen.
            origineFx, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
            Vector.eulerToAngle(iEulerAngleRotate), //rotation.
            Color.White //color (already white).
        );

    }

}