
public static class StatusEffectManager
{
    private static List<StatusEffectType> communEffect = new();
    private static List<StatusEffectType> rareEffect = new();


    // call in start run for fill pool of status effect (depend on succes unlock).
    public static void initStatusEffects()
    {
        communEffect = new();
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Red);
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Blue);
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Green);
        if (SaveManager.getSave.succes.Contains(Succes.Take_20_Damage))
        {
            communEffect.Add(StatusEffectType.ShildAddBoostColor_Blue);
            communEffect.Add(StatusEffectType.ShildAddBoostColor_Green);
        }
        if (SaveManager.getSave.succes.Contains(Succes.Take_40_Damage))
        {
            communEffect.Add(StatusEffectType.ShildMultBoostColor_Blue);
            communEffect.Add(StatusEffectType.ShildMultBoostColor_Green);
        }
        communEffect.Add(StatusEffectType.HPBoost);
        communEffect.AddRange( // push status effect type into pool.
            SaveManager.getSave.succes.Where(s => !s.isRare())
                .Select(s => s.getStatusEffectUnlocked())
                .Where(se => se != null).Cast<StatusEffectType>()
        );

        rareEffect = new();
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Red);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Blue);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Green);
        rareEffect.Add(StatusEffectType.APBoost);
        rareEffect.Add(StatusEffectType.MPBoost);
        rareEffect.AddRange( // push status effect type into pool.
            SaveManager.getSave.succes.Where(s => s.isRare())
                .Select(s => s.getStatusEffectUnlocked())
                .Where(se => se != null).Cast<StatusEffectType>()
        );
        
    }


    // generate a random status effect.
    public static StatusEffect generateARandomEffect(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, Random? rng = null)
    {
        rng ??= RandomManager.rng;

        bool isRare = (rareEffect.Count == 0) ? false : rng.Next(1000) < 120;
        int indexPick = rng.Next(
            (isRare) ? rareEffect.Count : communEffect.Count
        );

        StatusEffectType typePick = (isRare) ? rareEffect[indexPick] : communEffect[indexPick];

        return StaticStatusEffectType.GetStatusEffect(typePick, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife);
    }

}