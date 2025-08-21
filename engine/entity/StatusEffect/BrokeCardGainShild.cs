
public class BrokeCardGainShild : StatusEffect
{
    private int shildByCrack;

    public BrokeCardGainShild(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int shildByCrack = 1) :
    base(SpriteType.StatusEffect_BrokeCardGainShild, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.shildByCrack = shildByCrack;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne du bouclier a chaque carte cassee.\n" +
            $"plus {shildByCrack} points de bouclier.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Bouclier fissure";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenCardBroke(ref PackageRefCard packageRefCard)
    {
        this.getCharacterWhoHasEffect.takeShild(shildByCrack, this.getCharacterWhoHasEffect);
    }
}