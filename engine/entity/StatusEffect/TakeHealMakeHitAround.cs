
public class TakeHealMakeHitAround : StatusEffect
{
    private int damageMake;

    public TakeHealMakeHitAround(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damageMake = 1) :
    base(SpriteType.StatusEffect_TakeHealMakeHitAround, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageMake = damageMake;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"inflige des degat autour de soi pour chaque soin recu.\n" +
            $"{this.damageMake} domage par point de soin.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return "Soin destructeur";
    }
    public override bool isAMalus()
    {
        return this.damageMake < 0;
    }


    public override void eventWhenTakeAHeal(ref int healIncrement, ref Character? characterGiveHeal, ref PackageRefCard? refCard)
    {
        Character? characterHasEffect = this.getCharacterWhoHasEffect;
        if (characterHasEffect is null)
            return;
        int damage = this.damageMake * healIncrement;

        StaticEffectCard.doEffectCard( // do effect hitAround.
            effectCard: EffectCard.HitAround,
            characterLauncher: characterHasEffect,
            indexPosTarget: characterHasEffect.indexPosCel,
            effectValue: damage,
            refCard: refCard
        );
    }

}