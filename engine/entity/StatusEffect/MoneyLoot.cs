
public class MoneyLoot : StatusEffect
{
    private int money;

    public MoneyLoot(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int money) :
    base(SpriteType.StatusEffect_MoneyLoot, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.money = money;
    }
    public override void ActivateEffect()
    {
        this.getCharacterWhoHasEffect!.PO += this.money;
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
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false)
    {
        if (isCharacterWhoHasEffectDie)
            return;

        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        whoHas.PO -= this.money; // cancel effect.
    }

}