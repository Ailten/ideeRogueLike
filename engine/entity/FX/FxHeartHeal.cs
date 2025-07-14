using Raylib_cs;

public class FxHeartHeal : Fx
{
    private bool isOneFxAtTimeInstance = false;

    public FxHeartHeal(Vector pos, bool isOneFxAtTimeInstance = false) : base(SpriteType.FX_heartHeal)
    {
        this.pos = pos;

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.3f);

        this.isOneFxAtTimeInstance = isOneFxAtTimeInstance;
    }


    private static int amountOfHeart = 6; //amount of heart to spread.
    private static Vector directionStartSpreading = new(0, -1); //up.
    private static Vector distanceSpreadingRange = new(40f, 120f); //amount of pixels spreading in every direction.
    private static float turnAroundStar = 0.4f; //amount of turn around of all stars.
    private static Vector scaleRange = new(0.4f, 1f);
    private static Vector baseSize = new Vector(63, 62);
    private static float coneHideHeart = 40f;
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

        Vector sizeAtScreenScaled = sizeAtScreenBase * Vector.lerpF(scaleRange.x, scaleRange.y, i);
        Vector origineFx = sizeAtScreenScaled * encrage * scale * CanvasManager.scaleCanvas;
        float rotateFx = i * (360f * turnAroundStar);
        float distSpreadI = Vector.lerpF(distanceSpreadingRange.x, distanceSpreadingRange.y, i) * CameraManager.zoomCam;
        Vector directionStarSpeadingI = directionStartSpreading * distSpreadI;

        for (int j = 0; j < amountOfHeart; j++)
        {
            float rotateSpead = ((float)j / amountOfHeart) * 360f;
            rotateSpead += rotateFx;
            rotateSpead %= 360f;

            if (rotateSpead < coneHideHeart || rotateSpead > (360f - coneHideHeart))
                continue;

            Vector vectorSpreadingStar = Vector.rotateAVector(directionStarSpeadingI, rotateSpead);

            Rectangle rectDestFx = new(
                posToDraw + vectorSpreadingStar,
                sizeAtScreenScaled
            );

            Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceFx, //rect source from texture.
                rectDestFx, //rect desintation at screen.
                origineFx, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                0, //rotation.
                Color.White //color (already white).
            );
        }
    }

}