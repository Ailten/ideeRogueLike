
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
        Random rng = currentRoom.rngSpecialRoom;

        CardMenuBGUi bg = new CardMenuBGUi(this.idLayer);
        bg.pos = new(240, 0);

        // X button to exit special room menu.
        CheckBoxUi buttonExit = new CheckBoxUi(idLayer); // button exit card menu.
        buttonExit.zIndex = 3400;
        buttonExit.scale = new(0.5f, 0.5f);
        buttonExit.pos = new(1007, 33);
        buttonExit.eventClick = () =>
        {
            SpecialRoom.layer.unActive(); // close the layer special room.
        };

        string nameButtonValidate = "valid";

        switch (currentRoom.roomType)
        {
            case (RoomType.Room_Chest):
                this.isCleanSpecialFromRoom = true;

                bool isAnEffectChest = this.isAnEffectOrCard(rng);

                if (isAnEffectChest)
                {
                    List<StatusEffect> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        listChoose.Add(StatusEffectManager.generateARandomEffect(TurnManager.getMainPlayerCharacter().idEntity, rng: rng));
                    }

                    // details effect for select an effect.
                    StatusEffectDetailsUi statusEffectDetailsUi = new StatusEffectDetailsUi(this.idLayer);
                    float centerY = Vector.lerpF(10, 515, 0.5f);
                    statusEffectDetailsUi.pos = new(442, centerY);
                    statusEffectDetailsUi.scaleEffectIllu = 2f;
                    statusEffectDetailsUi.zIndex = 3200;
                    statusEffectDetailsUi.isPrintDetails = true;

                    StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer);
                    float sizeX = ((listChoose.Count - 1) * 73) + 63;
                    statusEffetUi.setWidthSize(sizeX);
                    statusEffetUi.pos = new(
                        CanvasManager.centerWindow.x - (sizeX / 2),
                        CanvasManager.sizeWindow.y - 180
                    );
                    statusEffetUi.setListEffect(listChoose);
                    statusEffetUi.isWithDetail = false;
                    statusEffetUi.heightSizeDownSelected = -20;
                    statusEffetUi.zIndex = 3200;
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
                        TurnManager.getMainPlayerCharacter().AddStatusEffect(effectSelected);

                        // update UI.
                        RunHudLayer.layer.statusEffectUi!.setListEffect(TurnManager.getMainPlayerCharacter().statusEffects);
                    };
                }
                else
                {
                    List<Card> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        Card cardGenerate = CardManager.generateARandomCard(rng: rng);

                        bool isCardRecto = rng.Next(1000) < 100;
                        cardGenerate.isRecto = isCardRecto;

                        listChoose.Add(cardGenerate);
                    }

                    // details card for select a card.
                    CardDetails cardDetails = new CardDetails(this.idLayer);
                    cardDetails.pos = new(250, 10);
                    cardDetails.zIndex = 3200;

                    ListCardUi cardsUi = new ListCardUi(this.idLayer);
                    cardsUi.pos = new(250, 388);
                    cardsUi.upCardWhenSelected = 45f;
                    cardsUi.zIndex = 3200;
                    cardsUi.isMakeReOrdered = false;
                    cardsUi.setListCard(listChoose);
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
                        TurnManager.getMainPlayerCharacter().deck.addCardToDeck(cardSelected, isSameColor: true);
                    };
                }

                break;

            case (RoomType.Room_Shop):
                const int AmountOfProductInShop = 5;
                nameButtonValidate = "acheter";

                this.setDictionaryShop(rng, AmountOfProductInShop);
                this.productPrice = new TextPriceProductUi[AmountOfProductInShop];

                bool isAnEffectShop = this.isAnEffectOrCard(rng);

                if (isAnEffectShop)
                {
                    List<StatusEffect> listChoose = new();
                    for (int i = 0; i < AmountOfProductInShop; i++)
                    {
                        listChoose.Add(StatusEffectManager.generateARandomEffect(TurnManager.getMainPlayerCharacter().idEntity, rng: rng));
                        this.productPrice[i] = new TextPriceProductUi(this.idLayer, $"{this.getPriceProduct(i)}");
                    }

                    // details effect for select an effect.
                    StatusEffectDetailsUi statusEffectDetailsUi = new StatusEffectDetailsUi(this.idLayer);
                    float centerY = Vector.lerpF(10, 455, 0.5f); // TODO: up the pos max.
                    statusEffectDetailsUi.pos = new(442, centerY);
                    statusEffectDetailsUi.scaleEffectIllu = 2f;
                    statusEffectDetailsUi.zIndex = 3200;
                    statusEffectDetailsUi.isPrintDetails = true;

                    StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer);
                    float sizeX = ((listChoose.Count - 1) * 73) + 63;
                    statusEffetUi.setWidthSize(sizeX);
                    statusEffetUi.pos = new(
                        CanvasManager.centerWindow.x - (sizeX / 2),
                        CanvasManager.sizeWindow.y - 240
                    );
                    statusEffetUi.setListEffect(listChoose);
                    statusEffetUi.setArrayIsSolded(SpecialRoom.layer.getArrayBoolIsSold());
                    statusEffetUi.isWithDetail = false;
                    statusEffetUi.heightSizeDownSelected = -20;
                    statusEffetUi.zIndex = 3200;
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

                    // replace price and maskSolded.
                    float pricePosY = statusEffetUi.pos.y + (StatusEffectUi.statusEffectSize.y + 35);
                    for (int i = 0; i < AmountOfProductInShop; i++)
                    {
                        float decalX = (StatusEffectUi.statusEffectSize.x + 10f) * i;
                        this.productPrice[i].pos = new(
                            statusEffetUi.pos.x + (StatusEffectUi.statusEffectSize.x / 2) + decalX,
                            pricePosY
                        );
                    }

                    // when valide, set statusEffect selected to player.
                    this.validateChoise = () =>
                    {
                        Character player = TurnManager.getMainPlayerCharacter();
                        int indexSelected = statusEffetUi.getIndexEffectSelected;
                        int priceSelected = this.getPriceProduct(indexSelected);

                        if (player.PO < priceSelected) // verif if can't paye.
                            return;

                        player.decreaseGold(priceSelected); // paye.
                        SpecialRoom.layer.setProductIsSoleded(indexSelected); // update bool is payed.
                        statusEffetUi.setArrayIsSolded(SpecialRoom.layer.getArrayBoolIsSold()); // update array bool isSolded for mask click.

                        if (isEntireShopSolded()) // close shop definitively, if sold every articles.
                            SpecialRoom.layer.isCleanSpecialFromRoom = true;

                        StatusEffect effectSelected = statusEffectDetailsUi.getStatusEffect() ?? throw new Exception("effectSelected is null !");
                        TurnManager.getMainPlayerCharacter().AddStatusEffect(effectSelected);

                        // update UI.
                        RunHudLayer.layer.statusEffectUi!.setListEffect(TurnManager.getMainPlayerCharacter().statusEffects);

                        // unselect.
                        statusEffectDetailsUi.setStatusEffect(null);
                        statusEffetUi.resetSelection();
                        SpecialRoom.layer.buttonValid!.setIsDisabled(true);
                    };
                }
                else
                {
                    List<Card> listChoose = new();
                    for (int i = 0; i < AmountOfProductInShop; i++)
                    {
                        Card cardGenerate = CardManager.generateARandomCard(rng: rng);

                        bool isCardRecto = rng.Next(1000) < 100;
                        cardGenerate.isRecto = isCardRecto;

                        listChoose.Add(cardGenerate);

                        this.productPrice[i] = new TextPriceProductUi(this.idLayer, $"{this.getPriceProduct(i)}");
                    }

                    // details card for select a card.
                    CardDetails cardDetails = new CardDetails(this.idLayer);
                    cardDetails.pos = new(250, 10);
                    cardDetails.zIndex = 3200;

                    ListCardUi cardsUi = new ListCardUi(this.idLayer);
                    cardsUi.pos = new(250, 388);
                    cardsUi.upCardWhenSelected = 45f;
                    cardsUi.zIndex = 3200;
                    cardsUi.isMakeReOrdered = false;
                    cardsUi.setListCard(listChoose);
                    cardsUi.setArrayIsSolded(SpecialRoom.layer.getArrayBoolIsSold());
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

                    // replace price and maskSolded.
                    float pricePosY = cardsUi.pos.y - 20;
                    float cardEndPosX = cardsUi.pos.x + 780 - Card.cardSize.x;
                    for (int i = 0; i < AmountOfProductInShop; i++)
                    {
                        float interpolateI = (float)i / (AmountOfProductInShop - 1);
                        float cardPosX = Vector.lerpF(cardsUi.pos.x, cardEndPosX, interpolateI);
                        float decalX = (StatusEffectUi.statusEffectSize.x + 10f) * i;
                        this.productPrice[i].pos = new(
                            cardPosX + (Card.cardSize.x / 2),
                            pricePosY
                        );
                    }

                    // when valide, set statusEffect selected to player.
                    this.validateChoise = () =>
                    {
                        Character player = TurnManager.getMainPlayerCharacter();
                        int indexSelected = cardsUi.getIndexCardSelected;
                        int priceSelected = this.getPriceProduct(indexSelected);

                        if (player.PO < priceSelected) // verif if can't paye.
                            return;

                        player.decreaseGold(priceSelected); // paye.
                        SpecialRoom.layer.setProductIsSoleded(indexSelected); // update bool is payed.
                        cardsUi.setArrayIsSolded(SpecialRoom.layer.getArrayBoolIsSold()); // update the array bool to print marker solded on card.

                        if (isEntireShopSolded()) // close shop definitively, if sold every articles.
                            SpecialRoom.layer.isCleanSpecialFromRoom = true;

                        Card cardSelected = cardDetails.getCard() ?? throw new Exception("cardSelected is null !");
                        cardSelected.isRecto = false; // force return card if recto (masked).
                        TurnManager.getMainPlayerCharacter().deck.addCardToDeck(cardSelected, isSameColor: true);

                        // unselect.
                        cardDetails.setCard(null);
                        cardsUi.unselectCard();
                        SpecialRoom.layer.buttonValid!.setIsDisabled(true);
                    };
                }

                break;

            case (RoomType.Room_Discard):
                this.isCleanSpecialFromRoom = true;
                nameButtonValidate = "suprimer";

                { // block generation.
                    // details card for select a card.
                    CardDetails cardDetails = new CardDetails(this.idLayer);
                    cardDetails.pos = new(250, 10);
                    cardDetails.zIndex = 3200;

                    ListCardUi cardsUi = new ListCardUi(this.idLayer);
                    cardsUi.pos = new(250, 388);
                    cardsUi.upCardWhenSelected = 45f;
                    cardsUi.zIndex = 3200;
                    cardsUi.isMakeReOrdered = false;
                    cardsUi.setListCard(TurnManager.getMainPlayerCharacter().deck.cardsInCimetier); // in a special room all card player is in cimetier.
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
        buttonBack.zIndex = 3400;
        buttonBack.eventClick = () =>
        {
            SpecialRoom.layer.unActive();
        };

        // button valid.
        this.buttonValid = new ButtonUi(this.idLayer);
        this.buttonValid.text = nameButtonValidate;
        this.buttonValid.pos = new(838, 661);
        this.buttonValid.setIsDisabled(true);
        this.buttonValid.zIndex = 3400;
        this.buttonValid.eventClick = () =>
        {
            // apply choice.
            SpecialRoom.layer.validateChoise();

            // make the room as normal.
            if (SpecialRoom.layer.isCleanSpecialFromRoom)
            {
                RunManager.currentRoom!.unSpecialTheRoom();
                SpecialRoom.layer.unActive();
                return;
            }
        };


        base.active();
    }


    public int amountChoise = 2; // amount of elements choise for a chest or a special room with choise.
    private bool isCleanSpecialFromRoom = false;
    public int cardInDeckPlayerAtStartStage = 10;
    public void updateCardCountInDeckPlayer()
    {
        this.cardInDeckPlayerAtStartStage = TurnManager.getMainPlayerCharacter().deck.countCardInFullDeck;
    }
    private bool isAnEffectOrCard(Random rng)
    {
        // do random based on rng, not the amont of card deck.
        return (rng.Next(1000) < 500);
        /*/
        int rngForTypeChest = rng.Next(1000);
        const int minCard = 8;
        const int maxCard = 12;
        int rangeRngTypeChest = (int)(
            999 - Vector.lerpF(0, 999,
                Vector.reverceLerpF(minCard, maxCard,
                    Math.Clamp(this.cardInDeckPlayerAtStartStage, minCard, maxCard)
                )
            )
        );
        return (rng.Next(1000) < rangeRngTypeChest);
        //*/
    }
    public TextPriceProductUi[] productPrice = new TextPriceProductUi[0];

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

        this.isCleanSpecialFromRoom = false;

        this.buttonValid = null;
        this.productPrice = new TextPriceProductUi[0];

        this.validateChoise = () => { };

        base.unActive();
    }


    // Dictionary use for stock data of eatch product (SE or Card).
    public Dictionary<int, KeyValuePair<int, bool>[]> dictionarShop = new Dictionary<int, KeyValuePair<int, bool>[]>();
    // call when change stage. for clear data from specialRoom of this stage.
    public void cleanSpecialRoomDataOfStage()
    {
        // dictioarShop take a KeyValuePair of price and isSolded, for eatch StatusEffect (of Card) of all shop stage (key Dictionary is SpecialRoom indexPos in stage concat : x*100 + y).
        this.dictionarShop = new Dictionary<int, KeyValuePair<int, bool>[]>();
    }
    private int getKeyOfDictionarShop(Vector? indexPosSpecialRoom = null)
    {
        Vector indexPosSpecialRoomNN = indexPosSpecialRoom ?? RunManager.currentStage.currentIndexRoom;
        return ((int)indexPosSpecialRoomNN.x) * 100 + ((int)indexPosSpecialRoomNN.y);
    }
    private void setDictionaryShop(Random rng, int AmountOfProductInShop)
    {
        int keySpecialRoomPos = this.getKeyOfDictionarShop();

        bool isAlreadyInit = dictionarShop.ContainsKey(keySpecialRoomPos);

        if (!isAlreadyInit)
            dictionarShop.Add(keySpecialRoomPos, new KeyValuePair<int, bool>[AmountOfProductInShop]);

        for (int i = 0; i < AmountOfProductInShop; i++)
        {
            // need to get Next for stay seed stable.
            int price = this.randomPriseProductShop(rng);

            // skip if already init.
            if (isAlreadyInit)
                continue;

            dictionarShop[keySpecialRoomPos][i] = new KeyValuePair<int, bool>(
                price,
                false
            );
        }
    }
    private int getPriceProduct(int indexProduct) {
        return dictionarShop[this.getKeyOfDictionarShop()][indexProduct].Key;
    }
    private bool getProductIsSold(int indexProduct) {
        return dictionarShop[this.getKeyOfDictionarShop()][indexProduct].Value;
    }
    private void setProductIsSoleded(int indexProduct)
    {
        dictionarShop[this.getKeyOfDictionarShop()][indexProduct] = new KeyValuePair<int, bool>(
            getPriceProduct(indexProduct),
            true
        );
    }
    private bool isEntireShopSolded()
    {
        return !getArrayBoolIsSold().Contains(false);
    }
    private bool[] getArrayBoolIsSold()
    {
        return dictionarShop[this.getKeyOfDictionarShop()].Select(kvp => kvp.Value).ToArray();
    }
    private int randomPriseProductShop(Random rng)
    {
        const int min = 10;
        const int max = 30;
        return rng.Next(min, max + 1);
    }

}