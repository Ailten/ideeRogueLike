
public class SpecialRoom : Layer
{
    private static SpecialRoom _layer = new SpecialRoom();
    public static SpecialRoom layer 
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        Room currentRoom = RunManager.currentRoom ?? throw new Exception("no current room !");
        currentRoom.refreshRngSpecialRoom();

        CardMenuBGUi bg = new CardMenuBGUi(this.idLayer);
        bg.pos = new(240, 0);

        switch (currentRoom.roomType)
        {
            case (RoomType.Room_Chest):
                this.isCleanSpecialFromRoom = true;

                int rngForTypeChest = currentRoom.rngSpecialRoom.Next(1000);
                bool isAnEffectChest = (rngForTypeChest < 330);

                isAnEffectChest = true; // DEBUG.

                if (isAnEffectChest)
                {
                    List<StatusEffect> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        listChoose.Add(StatusEffectManager.generateARandomEffect(TurnManager.getMainPlayerCharacter().idEntity));
                    }

                    StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer);
                    statusEffetUi.pos = new(380, 5);
                    statusEffetUi.setWidthSize(720);
                    statusEffetUi.setListEffect(listChoose);
                    // TODO : make statusEffectUi without arrow and details. (change upSelected ?)
                    // TODO : add event click and unClick.

                    // TODO : click for select an effect.
                }
                else
                {
                    List<Card> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        listChoose.Add(CardManager.generateARandomCard());
                    }

                    ListCardUi cardsUi = new ListCardUi(this.idLayer);
                    cardsUi.pos = new(250, 388);
                    cardsUi.upCardWhenSelected = 45f;
                    cardsUi.clickOnCard = (cardClicked, isLeftClick) =>
                    {
                        // TODO : select card on details.
                    };
                    cardsUi.unClickOnCard = (cardClicked, isLeftClick) =>
                    {
                        // TODO : remove card from details.
                    };

                    // TODO : click for select a card.
                }

                break;

            default:
                throw new Exception("RoomType has no SpecialRoom UI definition !");
        }

        // button back.
        ButtonUi buttonBack = new ButtonUi(this.idLayer);
        buttonBack.text = "back";
        buttonBack.pos = new(442, 661);
        buttonBack.eventClick = () =>
        {
            SpecialRoom.layer.unActive();
        };

        // button valid.
        this.buttonValid = new ButtonUi(this.idLayer);
        this.buttonValid.text = "valid";
        this.buttonValid.pos = new(838, 661);
        this.buttonValid.eventClick = () =>
        {
            // apply choice.
            SpecialRoom.layer.validateChoise();

            // make the room as normal.
            if (SpecialRoom.layer.isCleanSpecialFromRoom)
                RunManager.currentRoom?.unSpecialTheRoom();

            SpecialRoom.layer.unActive();
        };


        base.active();
    }


    public int amountChoise = 2; // amount of elements choise for a chest or a special room with choise.
    private bool isCleanSpecialFromRoom = false;
    public ButtonUi? buttonValid;
    public Action validateChoise = () => { };


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        this.buttonValid = null;

        base.unActive();
    }

}