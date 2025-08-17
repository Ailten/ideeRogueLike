
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

        // X button to exit special room menu.
        CheckBoxUi buttonExit = new CheckBoxUi(idLayer); // button exit card menu.
        buttonExit.isActive = false;
        buttonExit.zIndex = 3400;
        buttonExit.scale = new(0.5f, 0.5f);
        buttonExit.pos = new(1007, 33);
        buttonExit.eventClick = () =>
        {
            buttonExit.switchIsOn(); // stay on "X".
            SpecialRoom.layer.unActive(); // close the layer special room.
        };

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

                    // details effect for select an effect.
                    StatusEffectDetailsUi statusEffectDetailsUi = new StatusEffectDetailsUi(this.idLayer);
                    statusEffectDetailsUi.pos = new(313, 73); // edit pos.
                    statusEffectDetailsUi.scaleEffectIllu = 2f;

                    StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer);
                    statusEffetUi.pos = new(380, 5);
                    statusEffetUi.setWidthSize(720);
                    statusEffetUi.setListEffect(listChoose);
                    statusEffetUi.isWithDetail = false;
                    statusEffetUi.heightSizeDownSelected = -20;
                    statusEffetUi.clickOnEffect = (effectClicked, isLeftClick) =>
                    {
                        statusEffectDetailsUi.setStatusEffect(effectClicked);
                        SpecialRoom.layer.buttonValid!.setIsDisabled(false);
                    };
                    statusEffetUi.unClickOnEffect = (effectClicked, isLeftClick) =>
                    {
                        statusEffectDetailsUi.setStatusEffect(null);
                        SpecialRoom.layer.buttonValid!.setIsDisabled(true);
                    };

                    // when valide, set statusEffect selected to player.
                    this.validateChoise = () =>
                    {
                        StatusEffect effectSelected = statusEffectDetailsUi.getStatusEffect() ?? throw new Exception("effectSelected is null !");
                        TurnManager.getMainPlayerCharacter().statusEffects.Add(effectSelected);
                    };
                }
                else
                {
                    List<Card> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        Card cardGenerate = CardManager.generateARandomCard();
                        
                        bool isCardRecto = RandomManager.rng.Next(1000) < 100;
                        cardGenerate.isRecto = isCardRecto;

                        listChoose.Add(cardGenerate);
                    }

                    // details card for select a card.
                    CardDetails cardDetails = new CardDetails(this.idLayer);
                    cardDetails.pos = new(250, 10);

                    ListCardUi cardsUi = new ListCardUi(this.idLayer);
                    cardsUi.pos = new(250, 388);
                    cardsUi.upCardWhenSelected = 45f;
                    cardsUi.clickOnCard = (cardClicked, isLeftClick) =>
                    {
                        cardDetails.setCard(cardClicked);
                        SpecialRoom.layer.buttonValid!.setIsDisabled(false);
                    };
                    cardsUi.unClickOnCard = (cardClicked, isLeftClick) =>
                    {
                        cardDetails.setCard(null);
                        SpecialRoom.layer.buttonValid!.setIsDisabled(true);
                    };

                    // when valide, set statusEffect selected to player.
                    this.validateChoise = () =>
                    {
                        Card cardSelected = cardDetails.getCard() ?? throw new Exception("cardSelected is null !");
                        cardSelected.isRecto = false; // force return card if recto (masked).
                        TurnManager.getMainPlayerCharacter().deck.addCardToDeck(cardSelected);
                    };
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
        this.buttonValid.setIsDisabled(true);
        this.buttonValid.eventClick = () =>
        {
            // apply choice.
            SpecialRoom.layer.validateChoise();

            // make the room as normal.
            if (SpecialRoom.layer.isCleanSpecialFromRoom)
                RunManager.currentRoom!.unSpecialTheRoom();

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