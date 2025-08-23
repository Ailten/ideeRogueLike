
public class MultDamageByHPLeft : StatusEffect
{

    public MultDamageByHPLeft(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife) :
    base(SpriteType.StatusEffect_MultDamageByHPLeft, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }


    public float getPurcentHPLeft()
    {
        Character characterHasEffect = this.getCharacterWhoHasEffect;
        float i = (float)characterHasEffect.HP / characterHasEffect.HPmax;
        i = 1f - i;
        return i;
    }


    public override string getDescription()
    {
        int purcent = (int)(this.getPurcentHPLeft() * 100);
        return (
            $"- {this.getName()} :\n" +
            $"augmente les degats en fonction du pourcent d'HP manquant.\n" +
            $"gagnie {purcent}% degats." +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Berzerker";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        atk += (int)(atk * this.getPurcentHPLeft());
    }
}