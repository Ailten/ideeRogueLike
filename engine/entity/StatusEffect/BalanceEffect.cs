
public class BalanceEffect : StatusEffect
{
    private float duplicateRate;
    private float eraseRate;

    public BalanceEffect(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, float duplicateRate = 0.1f, float eraseRate = 0.1f) :
    base(SpriteType.StatusEffect_BalanceEffect, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.duplicateRate = duplicateRate;
        this.eraseRate = eraseRate;
    }


    private int getPurcent(bool isDuplicateRate)
    {
        float purcentSelected = (isDuplicateRate) ? this.duplicateRate : this.eraseRate;
        return (int)((purcentSelected) * 100);
    }


    public override string getDescription()
    {
        int purcentDuplicate = this.getPurcent(isDuplicateRate: true);
        int purcentErase = this.getPurcent(isDuplicateRate: false);
        return (
            $"- {this.getName()} :\n" +
            $"quand le joueur gagnie un combat, peu modifier les effets.\n" +
            $"{purcentDuplicate}% de chance de dupliquer un effet.\n" +
            $"{purcentErase}% de chance de suprimer un effet.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Balance effet";
    }
    public override bool isAMalus()
    {
        return this.duplicateRate > this.eraseRate;
    }


    public override void eventWhenPlayerWinFight()
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        int rngDuplicate = RandomManager.rng.Next(1000); // set rng.
        int rngErase = RandomManager.rng.Next(1000);
        int rangeForDuplicate = (int)Vector.lerpF(0, 999, this.duplicateRate);
        int rangeForErase = (int)Vector.lerpF(0, 999, this.eraseRate);
        bool isDuplicate = (rngDuplicate < rangeForDuplicate);
        bool isErase = (rngErase < rangeForErase);

        if (!isDuplicate && !isErase) // skip rng fail.
            return;
        if (isDuplicate && isErase) // assign only xor bool result.
        {
            isDuplicate = rngDuplicate < rngErase;
            isErase = !isDuplicate;
        }

        int randomIndexEffect = RandomManager.rng.Next(whoHas.statusEffects.Count);

        if (isDuplicate) // make duplicate effect.
        {
            StatusEffect se = whoHas.statusEffects[randomIndexEffect];
            StatusEffectType set = StaticStatusEffectType.getStatusEffectType(se);
            StatusEffect newSe = StaticStatusEffectType.getStatusEffect(
                set,
                this.getCharacterIdWhoHasEffect,
                this.getCharacterIdWhoApplyEffect,
                se.getTurnLife
            );

            whoHas.AddStatusEffect(newSe);

        }
        else
        {
            StatusEffect se = whoHas.statusEffects[randomIndexEffect];

            whoHas.dropAStatusEffectByIndex(randomIndexEffect);

            se.eventWhenStatusEffectDisapear(isDestroyByAction: true);
        }
    }
    
}