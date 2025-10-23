
// class specify for chest on layer endRun.
public class SuccesChestUi : Entity
{
    private static Vector sizeSuccesChest = new(355, 274);
    private List<Succes> listSucces = new();
    private int indexSucces = 0;
    private bool isPrintTheChest = true;

    public SuccesChestUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(0, 0);

        this.geometryTrigger = new Rect(
            sizeSuccesChest * -0.5f,
            sizeSuccesChest
        );
    }

    // set list succes.
    public void setListSucces(List<Succes> listSucces)
    {
        this.listSucces = listSucces;
        this.indexSucces = 0;
        this.isPrintTheChest = true;
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        // nothing more to print.
        if (this.indexSucces >= this.listSucces.Count)
        {
            return;
        }

        //TODO: print light ray with rotation.

        if (this.isPrintTheChest)
        {
            Vector sizeChestScaled = sizeSuccesChest * this.scale * CanvasManager.scaleCanvas;
            Rect destDraw = new Rect(
                posToDraw - sizeChestScaled * 0.5f,
                sizeChestScaled
            );

            Raylib_cs.Raylib.DrawTexturePro( // draw chest.
                texture: this.sprite.texture,
                source: this.sprite.getSpriteTileBySpriteType(SpriteType.UiCoffreWin).getRectSource(),
                dest: destDraw,
                origin: origine,
                rotation: 0,
                Raylib_cs.Color.White
            );
        }
        else
        {
            //TODO: print the contend of chest.
        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        this.isPrintTheChest = !this.isPrintTheChest;
        if (this.isPrintTheChest)
            this.indexSucces++;
    }


}