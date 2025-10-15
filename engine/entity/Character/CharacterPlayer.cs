
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
            case (SpriteType.Character_LunAlly):
                return new CharacterLunAlly(posIndexCel);
            case (SpriteType.Character_Chlow):
                return new CharacterChlow(posIndexCel);
            case (SpriteType.Character_Babulle):
                return new CharacterBabulle(posIndexCel);

            default:
                throw new Exception("SpriteType has no CharacterPlayer match !");

        }
    }


    //call when character dead.
    public override void death(Character? characterMakeKill = null, PackageRefCard? refCard = null, bool isMarkAsDead = true)
    {
        if (isMarkAsDead)
        {
            this.isMarkDeath = true;
            this.killedBy = characterMakeKill;
            return;
        }
        characterMakeKill = this.killedBy;
        
        if (!this.isAPlayer)
        {
            base.death(characterMakeKill, refCard, isMarkAsDead);
            return;
        }

        //hidde.
        isActive = false;

        //execute death of every entity in turn has invoc by this one.
        TurnManager.getAllInvocOfACharacter(this.idEntity).ForEach(c => c.death());

        //remove from list turn.
        TurnManager.removeCharacterInRoom(this); // warning, can make error to remove player from TurnManager.
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