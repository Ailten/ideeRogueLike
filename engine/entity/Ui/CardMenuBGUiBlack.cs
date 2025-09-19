
using Raylib_cs;

public class CardMenuBGUiBlack : CardMenuBGUi
{
    public static Color colorBlackTranspa = new Color(0, 0, 0, 64);

    public CardMenuBGUiBlack(int idLayer) : base(idLayer)
    {
        this.zIndex = 2999;

        this.size = new(0, 0);

        this.geometryTrigger = new Rect( //no effect, just for prevent click on element under card menu.
            new(0, 0),
            CanvasManager.sizeWindow
        );
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        Raylib.DrawRectangle(
            (int)rectDest.posStart.x,
            (int)rectDest.posStart.y,
            (int)CanvasManager.sizeWindow.x,
            (int)CanvasManager.sizeWindow.y,
            colorBlackTranspa
        );
    }
}