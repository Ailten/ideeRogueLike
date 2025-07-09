using System.Numerics;
using Raylib_cs;

public class FxTextHit : Fx
{
    private bool isOneFxAtTimeInstance = false;
    private string txt = "";
    private Color colorTxt;
    private static Font fontTxt = FontManager.getFontByFontType(FontType.IntensaFuente);

    public FxTextHit(Vector pos, string txt, Color colorTxt, bool isOneFxAtTimeInstance = false) : base(SpriteType.FX_starHit)
    {
        this.constructor(pos, txt, colorTxt);
    }
    public FxTextHit(FxTextHitParam param) : base(SpriteType.FX_starHit)
    {
        this.constructor(param.pos, param.txt, param.colorTxt, true);
    }
    private void constructor(Vector pos, string txt, Color colorTxt, bool isOneFxAtTimeInstance = false)
    {
        this.pos = pos;
        this.zIndex = 1401; //1 upper than Fx in battle.

        this.encrage = new(0.5f, 0.5f);

        this.setTimeAnimeDelay(0.3f);

        this.isOneFxAtTimeInstance = isOneFxAtTimeInstance;

        this.txt = txt;
        this.colorTxt = colorTxt;
    }


    private static Queue<FxTextHitParam> posNextFx = new();

    // function for init one fx at time (cancel init if an fx of this type is already instanciate).
    public static void initOnlyOneFxAtTime(Vector pos, string txt, Color colorTxt)
    {
        FxTextHitParam param = new FxTextHitParam()
        {
            pos = pos,
            txt = txt,
            colorTxt = colorTxt
        };
        posNextFx.Enqueue(param);
        if (posNextFx.Count > 1)
            return;
        new FxTextHit(param);
    }


    private static Vector[] posLerpFx = new Vector[] { new(0, -20f), new(0, -160f) };
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float i = this.getTimeI();
        if (i < 0f || i > 1f)
        {
            if (i > 1f)
                if (!this.isOneFxAtTimeInstance)
                    return;
                posNextFx.Dequeue();
                if (posNextFx.Count > 0)
                {
                    new FxTextHit(posNextFx.ElementAt(0));
                }
            return;
        }

        //draw sprite with effect based on time i.

        float fontSize = 400f * CanvasManager.scaleCanvas * CameraManager.zoomCam;
        float fontSpacingSize = 2f * CanvasManager.scaleCanvas * CameraManager.zoomCam;

        fontSize *= (i<=0.5f? // scale text size.
            Vector.lerpF(0.2f, 1f, Vector.lerpF(0f, 0.5f, i)):
            Vector.lerpF(1f, 0.2f, Vector.lerpF(0.5f, 1f, i))
        );

        Vector sizeText = Raylib.MeasureTextEx(fontTxt, this.txt, fontSize, fontSpacingSize);

        Vector posText = posToDraw;
        posText += Vector.lerp(posLerpFx[0], posLerpFx[1], i) * CanvasManager.scaleCanvas * CameraManager.zoomCam; // lerp up.
        posText -= sizeText / 2; // center text.

        Raylib.DrawTextEx(
            font: fontTxt,
            text: this.txt,
            position: posText,
            fontSize: fontSize,
            spacing: fontSpacingSize,
            this.colorTxt
        );

    }

}

public class FxTextHitParam
{
    public Vector pos { get; set; }
    public string txt = "";
    public Color colorTxt { get; set; }
}