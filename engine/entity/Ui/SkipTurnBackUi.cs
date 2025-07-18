
public class SkipTurnBackUi : Entity
{

    public bool isLeftSpriteTo = false;
    public bool isTopRightSpriteTo = false;

    public SkipTurnBackUi(int idLayer) : base(idLayer, SpriteType.SkipTurnBack)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.encrage = new(1, 1);

        this.size = new(269, 126);
    }

    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {

        //draw right background ui.
        if (this.isLeftSpriteTo)
        {
            Vector posAtScreen = this.pos;
            posAtScreen.x -= CanvasManager.sizeWindow.x; //edit pos for place it at left.
            posAtScreen.x -= 130; //decal mid size.
            posAtScreen *= CanvasManager.scaleCanvas;
            posAtScreen += CanvasManager.posDecalCanvas;

            Vector sizeAtScreen = this.size * this.scale * CanvasManager.scaleCanvas;

            Rect rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(this.spriteType).getRectSource();
            rectSourceInTexture.size.x *= -1; //revercie horizontal sprite.

            Rect leftRectDest = new Rect(
                new(posAtScreen.x, posAtScreen.y),
                new(sizeAtScreen.x, sizeAtScreen.y)
            );
            Vector leftOrigine = new Vector(0, 1) * this.size * this.scale * CanvasManager.scaleCanvas;

            Raylib_cs.Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceInTexture, //rect source from texture.
                leftRectDest, //rect desintation at screen.
                leftOrigine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                this.rotate, //rotation.
                Raylib_cs.Color.White //color (already white).
            );
        }

        //draw right background ui (top right).
        if (this.isTopRightSpriteTo)
        {
            Vector posAtScreen = this.pos;
            posAtScreen.y -= CanvasManager.sizeWindow.y; //edit pos for place it at left.
            posAtScreen.y += this.size.y; //decal mid size.
            posAtScreen += new Vector(100, -30); //replace sprite in corner.
            posAtScreen *= CanvasManager.scaleCanvas;
            posAtScreen += CanvasManager.posDecalCanvas;

            Vector sizeAtScreen = this.size * this.scale * CanvasManager.scaleCanvas;

            Rect rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(this.spriteType).getRectSource();
            rectSourceInTexture.size.y *= -1; //revercie verticale sprite.

            Rect rightTopRectDest = new Rect(
                new(posAtScreen.x, posAtScreen.y),
                new(sizeAtScreen.x, sizeAtScreen.y)
            );
            Vector rightTopOrigine = new Vector(1, 1) * this.size * this.scale * CanvasManager.scaleCanvas;

            Raylib_cs.Raylib.DrawTexturePro(
                this.sprite.texture, //texture.
                rectSourceInTexture, //rect source from texture.
                rightTopRectDest, //rect desintation at screen.
                rightTopOrigine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                this.rotate, //rotation.
                Raylib_cs.Color.White //color (already white).
            );
        }

    }

}