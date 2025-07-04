using Raylib_cs;

public class FxStarHit : Fx
{

    public FxStarHit(Vector pos) : base(SpriteType.FX_starHit)
    {
        this.pos = pos;

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.5f);
    }


    private static int amountOfStar = 5; //amount of star to spread.
    private static Vector directionStartSpreading = new(0, -1); //up.
    private static Vector distanceSpreadingRange = new(0f, 160f); //amount of pixels spreading in every direction.
    private static float turnAroundStar = 2f; //amount of turn around of all stars.
    private static Vector scaleRange = new(1f, 0f);
    private static Vector baseSize = new Vector(126, 126);
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if (i < 0f || i > 1f)
            return;

        //draw sprite with effect based on time i.

        Rectangle rectSourceFx = this.sprite.getSpriteTileBySpriteType(this.spriteType).getRectSource();
        Vector sizeAtScreenBase = baseSize * scale * CanvasManager.scaleCanvas * CameraManager.zoomCam;

        Vector sizeAtScreenScaled = sizeAtScreenBase * Vector.lerpF(scaleRange.x, scaleRange.y, i);
        Vector origineFx = sizeAtScreenScaled * encrage * scale * CanvasManager.scaleCanvas;
        float rotateFx = i * (360 * turnAroundStar);
        float distSpreadI = Vector.lerpF(distanceSpreadingRange.x, distanceSpreadingRange.y, i) * CameraManager.zoomCam;
        Vector directionStarSpeadingI = directionStartSpreading * distSpreadI;

        for (int j = 0; j < amountOfStar; j++)
        {
            Vector vectorSpreadingStar = Vector.rotateAVector(directionStarSpeadingI, ((float)j / amountOfStar) * 360);

            Rectangle rectDestFx = new(
                posToDraw + vectorSpreadingStar,
                sizeAtScreenScaled
            );

            Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceFx, //rect source from texture.
                rectDestFx, //rect desintation at screen.
                origineFx, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                rotateFx, //rotation.
                Color.White //color (already white).
            );
        }

    }
}