
public class MoneyLoot : StatusEffect
{
    private int money;

    public MoneyLoot(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int money) :
    base(SpriteType.StatusEffect_MoneyLoot, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.money = money;

        this.getCharacterWhoHasEffect.PO += this.money;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ajoute {this.money} d'or.\n"+
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return "Fortune";
    }
    public override bool isAMalus()
    {
        return this.money < 0;
    }


    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false)
    {
        if (isEndLifeEffect || isEndOfFight || isCharacterWhoApplyEffectDie) // cancel effect.
        {
            this.getCharacterWhoHasEffect.PO -= this.money;
        }
    }

}