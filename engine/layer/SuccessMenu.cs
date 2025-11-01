
public class SuccessMenu : Layer
{
    private static SuccessMenu _layer = new SuccessMenu();
    public static SuccessMenu layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        CardMenuBGUi bg = new CardMenuBGUi(this.idLayer); // draw back.
        bg.pos = new(0, 0);
        bg.size.x = CanvasManager.sizeWindow.x;
        bg.geometryTrigger = new Rect(new(), CanvasManager.sizeWindow);
        bg.zIndex = 3000;

        CheckBoxUi buttonExit = new CheckBoxUi(idLayer); // button exit.
        buttonExit.zIndex = 3400;
        buttonExit.scale = new(0.5f, 0.5f);
        buttonExit.pos = new(1247, 33);
        buttonExit.eventClick = () =>
        {
            LayerManager.transition( // with transition anime.
                idLevelStart: new int[] { SuccessMenu.layer.idLayer },
                idLevelEnd: new int[0]
            );

            buttonExit.setIsOn(false); // lock on "x" char.
        };


        // print all success.
        const float spacingSuccess = 200;
        const float basePosY = 200;
        const float basePosX = 320;
        this.maxYScroll = 0;
        Succes[] allSuccess = (Succes[])Enum.GetValues(typeof(Succes));
        for (int i = 0; i < allSuccess.Length; i += 2)
        {
            Succes s = allSuccess[i];

            SuccessDetails sd = new SuccessDetails(this.idLayer, s);
            sd.pos.x = CanvasManager.centerWindow.x - basePosX;
            sd.pos.y = basePosY + spacingSuccess * (i/2);
            sd.isUnlocked = SaveManager.isHasSucces(s);

            this.maxYScroll += spacingSuccess;  // set max range scroll.

            if (i + 1 >= allSuccess.Length)
                break;

            s = allSuccess[i + 1];
            
            sd = new SuccessDetails(this.idLayer, s);
            sd.pos.x = CanvasManager.centerWindow.x + basePosX;
            sd.pos.y = basePosY + spacingSuccess * (i/2);
            sd.isUnlocked = SaveManager.isHasSucces(s);
        }
        ;
        this.maxYScroll -= 600f;  // decrease for ajust end scroll.


        base.active();
    }


    private float posYScroll = 0f;
    private float maxYScroll;


    public override void update()
    {
        //do the update. --->

        // edit elements pos based on wheel scroll.
        const float wheelScrollIntencity = 60f;
        float wheelMove = Raylib_cs.Raylib.GetMouseWheelMove();
        if (wheelMove != 0)
        {
            float posY = posYScroll + wheelScrollIntencity * -wheelMove;
            if (posY < 0f || posY > maxYScroll)  // cancel scrool if out of range.
                return;
            this.posYScroll = posY;

            EntityManager.getEntitiesByIdLayer(this.idLayer)
                .Where(e => e is SuccessDetails).ToList()
                .ForEach(e =>
                {
                    e.pos.y += wheelScrollIntencity * wheelMove;
                });
        }

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        LayerManager.isADetailsLayerAreOpen = false;

        this.posYScroll = 0f;
        this.maxYScroll = 0;

        base.unActive();
    }

}