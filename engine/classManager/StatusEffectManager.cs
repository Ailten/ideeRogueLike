
public static class StatusEffectManager
{
    private static List<StatusEffectType> communEffect = new();
    private static List<StatusEffectType> rareEffect = new();


    // call in start run for fill pool of status effect (depend on succes unlock).
    public static void initStatusEffects()
    {

        //FIX ME:
        //How to manage a pool of status effect ?
        // - store a list of instanced object (but is store by instance, so we can't pull twice the same)
        // - store a type of status effect child (but, some effect has a colorCard type in parameters, spliting by 4 the result of same class)
        // - do a switch case (but how manage success modification)
        // - do an enum for eatch status effect. [V]


        communEffect = new();
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Red);
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Blue);
        communEffect.Add(StatusEffectType.DamageAddBoostColor_Green);
        communEffect.Add(StatusEffectType.DamageAddBoostShiny);

        rareEffect = new();
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Red);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Blue);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Green);
        rareEffect.Add(StatusEffectType.DamageMultBoostShiny);
        rareEffect.Add(StatusEffectType.APBoost);
        rareEffect.Add(StatusEffectType.MPBoost);
    }


    // generate a random status effect.
    public static StatusEffect generateARandomEffect(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1)
    {
        bool isRare = (rareEffect.Count == 0)? false: RandomManager.rng.Next(1000) < 120;
        int indexPick = RandomManager.rng.Next(
            (isRare)? rareEffect.Count: communEffect.Count
        );
        
        StatusEffectType typePick = (isRare)? rareEffect[indexPick]: communEffect[indexPick];

        switch (typePick)
        {
            case (StatusEffectType.DamageAddBoostColor_Red):
            case (StatusEffectType.DamageAddBoostColor_Blue):
            case (StatusEffectType.DamageAddBoostColor_Green):
                return new DamageAddBoostColor(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    color: (
                        (typePick == StatusEffectType.DamageAddBoostColor_Red)? CardColor.Red:
                        (typePick == StatusEffectType.DamageAddBoostColor_Blue)? CardColor.Blue:
                        CardColor.Green
                    ),
                    damageBoost: RandomManager.rng.Next(1, 4)
                );
            case (StatusEffectType.DamageAddBoostShiny):
                return new DamageAddBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageBoost: RandomManager.rng.Next(1, 4)
                );

            case (StatusEffectType.DamageMultBoostColor_Red):
            case (StatusEffectType.DamageMultBoostColor_Blue):
            case (StatusEffectType.DamageMultBoostColor_Green):
                return new DamageMultBoostColor(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    color: (
                        (typePick == StatusEffectType.DamageMultBoostColor_Red)? CardColor.Red:
                        (typePick == StatusEffectType.DamageMultBoostColor_Blue)? CardColor.Blue:
                        CardColor.Green
                    ),
                    damageMult: RandomManager.rng.Next(110, 221) / 100f
                );
            case (StatusEffectType.DamageMultBoostShiny):
                return new DamageMultBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageMult: RandomManager.rng.Next(110, 221) / 100f
                );
            case (StatusEffectType.APBoost):
                return new APBoost(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    APUp: 1
                );
            case (StatusEffectType.MPBoost):
                return new MPBoost(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    MPUp: 1
                );

            default:
                throw new Exception("StatusEffectType pick has no instanciation expected !");
        }
    }

}