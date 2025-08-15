
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
        communEffect.Add(StatusEffectType.DamageAddBoostShiny);
        communEffect.Add(StatusEffectType.ShildAddBoostColor_Red);
        communEffect.Add(StatusEffectType.ShildAddBoostColor_Blue);
        communEffect.Add(StatusEffectType.ShildAddBoostColor_Green);
        communEffect.Add(StatusEffectType.ShildAddBoostShiny);
        communEffect.Add(StatusEffectType.ShildMultBoostColor_Red);
        communEffect.Add(StatusEffectType.ShildMultBoostColor_Blue);
        communEffect.Add(StatusEffectType.ShildMultBoostColor_Green);
        communEffect.Add(StatusEffectType.ShildMultBoostShiny);

        rareEffect = new();
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Red);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Blue);
        rareEffect.Add(StatusEffectType.DamageMultBoostColor_Green);
        rareEffect.Add(StatusEffectType.DamageMultBoostShiny);
        rareEffect.Add(StatusEffectType.APBoost);
        rareEffect.Add(StatusEffectType.MPBoost);
        rareEffect.Add(StatusEffectType.APWhenHit);
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

            case (StatusEffectType.ShildAddBoostColor_Red):
            case (StatusEffectType.ShildAddBoostColor_Blue):
            case (StatusEffectType.ShildAddBoostColor_Green):
                return new ShildAddBoostColor(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    color: (
                        (typePick == StatusEffectType.ShildAddBoostColor_Red)? CardColor.Red:
                        (typePick == StatusEffectType.ShildAddBoostColor_Blue)? CardColor.Blue:
                        CardColor.Green
                    ),
                    shildBoost: RandomManager.rng.Next(1, 4)
                );
            case (StatusEffectType.ShildAddBoostShiny):
                return new ShildAddBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildBoost: RandomManager.rng.Next(1, 4)
                );

            case (StatusEffectType.ShildMultBoostColor_Red):
            case (StatusEffectType.ShildMultBoostColor_Blue):
            case (StatusEffectType.ShildMultBoostColor_Green):
                return new ShildMultBoostColor(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    color: (
                        (typePick == StatusEffectType.ShildMultBoostColor_Red)? CardColor.Red:
                        (typePick == StatusEffectType.ShildMultBoostColor_Blue)? CardColor.Blue:
                        CardColor.Green
                    ),
                    shildMult: RandomManager.rng.Next(80, 95) / 100f
                );
            case (StatusEffectType.ShildMultBoostShiny):
                return new ShildMultBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildMult: RandomManager.rng.Next(80, 95) / 100f
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
            case (StatusEffectType.APWhenHit):
                return new APWhenHit(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    APUp: 1
                );

            default:
                throw new Exception("StatusEffectType pick has no instanciation expected !");
        }
    }

}