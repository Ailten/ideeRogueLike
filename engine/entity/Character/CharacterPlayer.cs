
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
                return new CharacterAilten(posIndexCel);
            case (SpriteType.Character_DarumaNico):
                return new CharacterDarumaNico(posIndexCel);
            case (SpriteType.Character_Axolootl):
                return new CharacterAxolootl(posIndexCel);
            case (SpriteType.Character_Blacacia):
                return new CharacterBlacacia(posIndexCel);
            case (SpriteType.Character_Barbak):
                return new CharacterBarbak(posIndexCel);

            default:
                throw new Exception("SpriteType has no CharacterPlayer match !");

        }
    }


    //call when character dead.
    public override void death(Character? characterMakeKill = null, PackageRefCard? refCard = null)
    {
        if (!isAPlayer)
        {
            base.death(characterMakeKill, refCard);
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
        RunHudLayer.layer.cardHandListCardUi!.setListCard(this.deck.cardsInHand); //link card list UI with card player character current turn.
        RunHudLayer.layer.statusEffectUi!.setListEffect(this.statusEffects);
    }
    //overide for aditionnal process for skip turn when player.
    public override void skipTurn()
    {
        RunHudLayer.layer.statusEffectUi!.setListEffect(new());
        RunHudLayer.layer.cardHandListCardUi!.setListCard(new());
        base.skipTurn();
    }

}