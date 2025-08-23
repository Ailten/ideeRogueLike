
public enum StatusEffectType
{
    Burn,
    MoneyLoot,
    DamageAddBoostColor_Red,
    DamageAddBoostColor_Blue,
    DamageAddBoostColor_Green,
    DamageAddBoostShiny,
    DamageMultBoostColor_Red,
    DamageMultBoostColor_Blue,
    DamageMultBoostColor_Green,
    DamageMultBoostShiny,
    ShildAddBoostColor_Red,
    ShildAddBoostColor_Blue,
    ShildAddBoostColor_Green,
    ShildAddBoostShiny,
    ShildMultBoostColor_Red,
    ShildMultBoostColor_Blue,
    ShildMultBoostColor_Green,
    ShildMultBoostShiny,
    APBoost,
    MPBoost,
    APWhenHit,
    HPBoost,
    BoostIntoInvoke,
    BoostChooseSpecialRoom,
    BoostPickCard,
    MoneyMultiplyDamage,
    YingYangShinyCracked,
    DuplicateCracked,
    CrackedAddDamage,
    BrokeCardGainShild,
    BalanceEffect,
    ShinyGainAP,
    SideEyes,
    RallMpMakeDamage,
    PushWallMakeSelfHeal,
    PushWallMakeRallMP,
    AddIndirectDamage,
    TakeHealMakeHitAround,
    MultDamageByHPLeft,
    TakeHealAddDamage,

}


public static class StaticStatusEffectType
{
    public static StatusEffect getStatusEffect(this StatusEffectType statusEffectType, int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1)
    {
        switch (statusEffectType)
        {
            case (StatusEffectType.DamageAddBoostColor_Red):
            case (StatusEffectType.DamageAddBoostColor_Blue):
            case (StatusEffectType.DamageAddBoostColor_Green):
                return new DamageAddBoostColor(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    color: (
                        (statusEffectType == StatusEffectType.DamageAddBoostColor_Red) ? CardColor.Red :
                        (statusEffectType == StatusEffectType.DamageAddBoostColor_Blue) ? CardColor.Blue :
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
                        (statusEffectType == StatusEffectType.DamageMultBoostColor_Red) ? CardColor.Red :
                        (statusEffectType == StatusEffectType.DamageMultBoostColor_Blue) ? CardColor.Blue :
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
                        (statusEffectType == StatusEffectType.ShildAddBoostColor_Red) ? CardColor.Red :
                        (statusEffectType == StatusEffectType.ShildAddBoostColor_Blue) ? CardColor.Blue :
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
                        (statusEffectType == StatusEffectType.ShildMultBoostColor_Red) ? CardColor.Red :
                        (statusEffectType == StatusEffectType.ShildMultBoostColor_Blue) ? CardColor.Blue :
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
            case (StatusEffectType.HPBoost):
                return new HPBoost(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    HPUp: 10
                );
            case (StatusEffectType.BoostIntoInvoke):
                return new BoostIntoInvoke(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife
                );
            case (StatusEffectType.BoostChooseSpecialRoom):
                return new BoostChooseSpecialRoom(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    chooseBoost: Math.Clamp(RandomManager.rng.Next(0, 3), 1, 2)
                );
            case (StatusEffectType.BoostPickCard):
                return new BoostPickCard(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    cardPickBoost: Math.Clamp(RandomManager.rng.Next(0, 3), 1, 2)
                );
            case (StatusEffectType.MoneyMultiplyDamage):
                return new MoneyMultiplyDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentDamageByCoint: Math.Clamp(RandomManager.rng.Next(0, 3), 1, 2)
                );
            case (StatusEffectType.YingYangShinyCracked):
                return new YingYangShinyCracked(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentCastShiny: ((float)RandomManager.rng.Next(0, 16) / 100),
                    purcentCastCracked: ((float)RandomManager.rng.Next(0, 16) / 100)
                );
            case (StatusEffectType.DuplicateCracked):
                return new DuplicateCracked(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentSave: ((float)RandomManager.rng.Next(30, 51) / 100)
                );
            case (StatusEffectType.CrackedAddDamage):
                return new CrackedAddDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageByCracked: RandomManager.rng.Next(1, 3)
                );
            case (StatusEffectType.BrokeCardGainShild):
                return new BrokeCardGainShild(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildByCrack: RandomManager.rng.Next(1, 4)
                );
            case (StatusEffectType.BalanceEffect):
                return new BalanceEffect(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    duplicateRate: ((float)RandomManager.rng.Next(0, 16) / 100),
                    eraseRate: ((float)RandomManager.rng.Next(0, 16) / 100)
                );
            case (StatusEffectType.ShinyGainAP):
                return new ShinyGainAP(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife
                );
            case (StatusEffectType.SideEyes):
                return new SideEyes(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife
                );
            case (StatusEffectType.RallMpMakeDamage):
                return new RallMpMakeDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageByMPDecrease: RandomManager.rng.Next(1, 4)
                );
            case (StatusEffectType.PushWallMakeSelfHeal):
                return new PushWallMakeSelfHeal(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    heal: RandomManager.rng.Next(1, 3)
                );
            case (StatusEffectType.PushWallMakeRallMP):
                return new PushWallMakeRallMP(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    rallMPByCase: Math.Clamp(RandomManager.rng.Next(0, 3), 1, 2)
                );
            case (StatusEffectType.AddIndirectDamage):
                return new AddIndirectDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageBoost: RandomManager.rng.Next(1, 4)
                );
            case (StatusEffectType.TakeHealMakeHitAround):
                return new TakeHealMakeHitAround(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageMake: RandomManager.rng.Next(1, 3)
                );
            case (StatusEffectType.MultDamageByHPLeft):
                return new MultDamageByHPLeft(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife
                );
            case (StatusEffectType.TakeHealAddDamage):
                return new TakeHealAddDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife
                );




            default:
                throw new Exception("StatusEffectType pick has no instanciation expected !");
        }
    }

    public static StatusEffectType getStatusEffectType(StatusEffect statusEffect)
    {
        if (statusEffect.GetType() == typeof(DamageAddBoostColor))
        {
            DamageAddBoostColor statusEffectCast = (DamageAddBoostColor)statusEffect;
            return (
                (statusEffectCast.color == CardColor.Red) ? StatusEffectType.DamageAddBoostColor_Red :
                (statusEffectCast.color == CardColor.Blue) ? StatusEffectType.DamageAddBoostColor_Blue :
                StatusEffectType.DamageAddBoostColor_Green
            );
        }
        if (statusEffect.GetType() == typeof(DamageAddBoostShiny))
            return StatusEffectType.DamageAddBoostShiny;
        if (statusEffect.GetType() == typeof(DamageMultBoostColor))
        {
            DamageMultBoostColor statusEffectCast = (DamageMultBoostColor)statusEffect;
            return (
                (statusEffectCast.color == CardColor.Red) ? StatusEffectType.DamageMultBoostColor_Red :
                (statusEffectCast.color == CardColor.Blue) ? StatusEffectType.DamageMultBoostColor_Blue :
                StatusEffectType.DamageMultBoostColor_Green
            );
        }
        if (statusEffect.GetType() == typeof(DamageMultBoostShiny))
            return StatusEffectType.DamageMultBoostShiny;
        if (statusEffect.GetType() == typeof(ShildAddBoostColor))
        {
            ShildAddBoostColor statusEffectCast = (ShildAddBoostColor)statusEffect;
            return (
                (statusEffectCast.color == CardColor.Red) ? StatusEffectType.ShildAddBoostColor_Red :
                (statusEffectCast.color == CardColor.Blue) ? StatusEffectType.ShildAddBoostColor_Blue :
                StatusEffectType.ShildAddBoostColor_Green
            );
        }
        if (statusEffect.GetType() == typeof(ShildAddBoostShiny))
            return StatusEffectType.ShildAddBoostShiny;
        if (statusEffect.GetType() == typeof(ShildMultBoostColor))
        {
            ShildMultBoostColor statusEffectCast = (ShildMultBoostColor)statusEffect;
            return (
                (statusEffectCast.color == CardColor.Red) ? StatusEffectType.ShildMultBoostColor_Red :
                (statusEffectCast.color == CardColor.Blue) ? StatusEffectType.ShildMultBoostColor_Blue :
                StatusEffectType.ShildMultBoostColor_Green
            );
        }
        if (statusEffect.GetType() == typeof(ShildMultBoostShiny))
            return StatusEffectType.ShildMultBoostShiny;

        if (statusEffect.GetType() == typeof(APBoost))
            return StatusEffectType.APBoost;
        if (statusEffect.GetType() == typeof(MPBoost))
            return StatusEffectType.MPBoost;
        if (statusEffect.GetType() == typeof(APWhenHit))
            return StatusEffectType.APWhenHit;
        if (statusEffect.GetType() == typeof(HPBoost))
            return StatusEffectType.HPBoost;
        if (statusEffect.GetType() == typeof(BoostIntoInvoke))
            return StatusEffectType.BoostIntoInvoke;
        if (statusEffect.GetType() == typeof(BoostChooseSpecialRoom))
            return StatusEffectType.BoostChooseSpecialRoom;
        if (statusEffect.GetType() == typeof(BoostPickCard))
            return StatusEffectType.BoostPickCard;
        if (statusEffect.GetType() == typeof(MoneyMultiplyDamage))
            return StatusEffectType.MoneyMultiplyDamage;
        if (statusEffect.GetType() == typeof(YingYangShinyCracked))
            return StatusEffectType.YingYangShinyCracked;
        if (statusEffect.GetType() == typeof(DuplicateCracked))
            return StatusEffectType.DuplicateCracked;
        if (statusEffect.GetType() == typeof(CrackedAddDamage))
            return StatusEffectType.CrackedAddDamage;
        if (statusEffect.GetType() == typeof(BrokeCardGainShild))
            return StatusEffectType.BrokeCardGainShild;
        if (statusEffect.GetType() == typeof(BalanceEffect))
            return StatusEffectType.BalanceEffect;
        if (statusEffect.GetType() == typeof(ShinyGainAP))
            return StatusEffectType.ShinyGainAP;
        if (statusEffect.GetType() == typeof(SideEyes))
            return StatusEffectType.SideEyes;
        if (statusEffect.GetType() == typeof(RallMpMakeDamage))
            return StatusEffectType.RallMpMakeDamage;
        if (statusEffect.GetType() == typeof(PushWallMakeSelfHeal))
            return StatusEffectType.PushWallMakeSelfHeal;
        if (statusEffect.GetType() == typeof(PushWallMakeRallMP))
            return StatusEffectType.PushWallMakeRallMP;
        if (statusEffect.GetType() == typeof(AddIndirectDamage))
            return StatusEffectType.AddIndirectDamage;
        if (statusEffect.GetType() == typeof(TakeHealMakeHitAround))
            return StatusEffectType.TakeHealMakeHitAround;
        if (statusEffect.GetType() == typeof(MultDamageByHPLeft))
            return StatusEffectType.MultDamageByHPLeft;
        if (statusEffect.GetType() == typeof(TakeHealAddDamage))
            return StatusEffectType.TakeHealAddDamage;
            




        throw new Exception("getStatusEffectType has no impementation for this !");
    }
}