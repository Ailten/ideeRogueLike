
public class SpecialRoom : Layer
{
    private static SpecialRoom _layer = new SpecialRoom();
    public static SpecialRoom layer 
    {
        get { return _layer; }
    }

    public int amountChoise = 2; // amount of elements choise for a chest or a special room with choise.

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

                    // TODO : click for select an effect.
                }
                else
                {
                    List<Card> listChoose = new();
                    for (int i = 0; i < this.amountChoise; i++)
                    {
                        listChoose.Add(CardManager.generateARandomCard());
                    }

                    // TODO : UI list card selection.
                }

                break;

            default:
                throw new Exception("RoomType has no SpecialRoom UI definition !");
        }

        // TODO : button back.

        // TODO : button valid.

        base.active();
    }

    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        base.unActive();
    }

}