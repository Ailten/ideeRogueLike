
using Raylib_cs;

public class CardMenuBGUi : Entity
{
    public static Color color = new Color(52, 47, 36, 255);

    public CardMenuBGUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3000;

        this.size = new(800, CanvasManager.sizeWindow.y);

        this.encrage = new(0, 0);

        this.geometryTrigger = new Rect( //no effect, just for prevent click on element under card menu.
            new(0, 0),
            new(800, CanvasManager.sizeWindow.y)
        );
    }

    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        Raylib.DrawRectangle(
            (int)rectDest.posStart.x, 
            (int)rectDest.posStart.y, 
            (int)rectDest.size.x, 
            (int)rectDest.size.y, 
            color
        );
    }
}