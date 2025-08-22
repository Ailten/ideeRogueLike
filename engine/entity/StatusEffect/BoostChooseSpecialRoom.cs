
public class BoostChooseSpecialRoom : StatusEffect
{

    private int chooseBoost = 0;

    public BoostChooseSpecialRoom(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int chooseBoost = 1) :
    base(SpriteType.StatusEffect_BoostChooseSpecialRoom, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.chooseBoost = chooseBoost;
    }
    public override void ActivateEffect()
    {
        if (this.getCharacterWhoHasEffect.isAPlayer)
            SpecialRoom.layer.amountChoise += chooseBoost;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.chooseBoost} choix dans les pieces specials.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Choix aux pieces specials";
    }
    public override bool isAMalus()
    {
        return this.chooseBoost < 0;
    }
    

    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false)
    {
        if (this.getCharacterWhoHasEffect.isAPlayer)
            SpecialRoom.layer.amountChoise += chooseBoost; // cancel effect.
    }
}