
public class CharacterPlayer : Character
{

    public CharacterPlayer(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.isInRedTeam = true;
    }

    //instantiate a CharacterPlayer.
    public static CharacterPlayer init(SpriteType spriteType, Vector posIndexCel)
    {
        switch (spriteType)
        {

            case (SpriteType.Character_Ailten):
                return new CharacterAilten(spriteType, posIndexCel);

            //add heer other CharacterPlayer.

            default:
                throw new Exception("SpriteType has no CharacterPlayer match !");

        }
    }


    //call when character dead.
    public override void death(Character? characterMakeKill = null)
    {
        if (!isAPlayer)
        {
            base.death(characterMakeKill);
            return;
        }

        //hidde.
        isActive = false;

        //execute death of every entity in turn has invoc by this one.
        TurnManager.getAllInvocOfACharacter(this).ForEach((c) => c.death());

        //do not remove main player of turnManager, for prevent getter overage.

        //TODO : game over screen ? (not remove, jsut unactive, so endFight redWin never call ?).
    }

    //overide for aditionnal process for start turn when player.
    public override void startTurn()
    {
        base.startTurn(); //do the basic start turn.
        RunHudLayer.layer.cardHandListCardUi?.setListCard(this.deck.cardsInHand); //link card list UI with card player character current turn.
    }
    //overide for aditionnal process for skip turn when player.
    public override void skipTurn()
    {
        List<Card> newListCard = new();
        RunHudLayer.layer.cardHandListCardUi?.setListCard(newListCard);
        base.skipTurn();
    }

}