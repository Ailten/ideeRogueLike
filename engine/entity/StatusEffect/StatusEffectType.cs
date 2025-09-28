
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
    PropagatePoison,
    ShildMultWhenFirst,
    ConvertPurcentHealInShild,
    FauxEffect,

}


public static class StaticStatusEffectType
{
    public static StatusEffect getStatusEffect(this StatusEffectType statusEffectType, int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, Random? rng = null)
    {
        rng ??= RandomManager.rng;

        switch (statusEffectType)
        {
            case (StatusEffectType.Burn):
                return new Burn(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damage: 1
                );
            case (StatusEffectType.MoneyLoot):
                return new MoneyLoot(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    money: 2
                );

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
                    damageBoost: rng.Next(1, 5)
                );
            case (StatusEffectType.DamageAddBoostShiny):
                return new DamageAddBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageBoost: rng.Next(3, 8)
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
                    damageMult: rng.Next(15, 23) / 10f
                );
            case (StatusEffectType.DamageMultBoostShiny):
                return new DamageMultBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageMult: rng.Next(18, 26) / 100f
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
                    shildBoost: rng.Next(3, 6)
                );
            case (StatusEffectType.ShildAddBoostShiny):
                return new ShildAddBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildBoost: rng.Next(5, 11)
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
                    shildMult: rng.Next(60, 86) / 100f
                );
            case (StatusEffectType.ShildMultBoostShiny):
                return new ShildMultBoostShiny(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildMult: rng.Next(40, 71) / 100f
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
                    HPUp: 1
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
                    chooseBoost: 1
                );
            case (StatusEffectType.BoostPickCard):
                return new BoostPickCard(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    cardPickBoost: 1
                );
            case (StatusEffectType.MoneyMultiplyDamage):
                return new MoneyMultiplyDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentDamageByCoint: 1
                );
            case (StatusEffectType.YingYangShinyCracked):
                return new YingYangShinyCracked(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentCastShiny: 0.15f,
                    purcentCastCracked: 0.15f
                );
            case (StatusEffectType.DuplicateCracked):
                return new DuplicateCracked(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentSave: 0.99f
                );
            case (StatusEffectType.CrackedAddDamage):
                return new CrackedAddDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageByCracked: 5
                );
            case (StatusEffectType.BrokeCardGainShild):
                return new BrokeCardGainShild(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildByCrack: 5
                );
            case (StatusEffectType.BalanceEffect):
                return new BalanceEffect(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    duplicateRate: 0.05f,
                    eraseRate: 0.05f
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
                    damageByMPDecrease: 4
                );
            case (StatusEffectType.PushWallMakeSelfHeal):
                return new PushWallMakeSelfHeal(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    heal: 4
                );
            case (StatusEffectType.PushWallMakeRallMP):
                return new PushWallMakeRallMP(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    rallMPByCase: 1
                );
            case (StatusEffectType.AddIndirectDamage):
                return new AddIndirectDamage(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageBoost: 3
                );
            case (StatusEffectType.TakeHealMakeHitAround):
                return new TakeHealMakeHitAround(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damageMake: 2
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
            case (StatusEffectType.PropagatePoison):
                return new PropagatePoison(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    damage: 2
                );
            case (StatusEffectType.ShildMultWhenFirst):
                return new ShildMultWhenFirst(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    shildMult: rng.Next(20, 31)/10f
                );
            case (StatusEffectType.ConvertPurcentHealInShild):
                return new ConvertPurcentHealInShild(
                    characterIdWhoHasEffect: characterIdWhoHasEffect,
                    characterIdWhoApplyEffect: characterIdWhoApplyEffect,
                    turnLife: turnLife,
                    purcentConvert: rng.Next(5, 11)/10f
                );
            case (StatusEffectType.FauxEffect):
                return new FauxEffect(
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
        if (statusEffect.GetType() == typeof(Burn))
            return StatusEffectType.Burn;
        if (statusEffect.GetType() == typeof(MoneyLoot))
            return StatusEffectType.MoneyLoot;
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
        if (statusEffect.GetType() == typeof(PropagatePoison))
            return StatusEffectType.PropagatePoison;
        if (statusEffect.GetType() == typeof(ShildMultWhenFirst))
            return StatusEffectType.ShildMultWhenFirst;
        if (statusEffect.GetType() == typeof(ConvertPurcentHealInShild))
            return StatusEffectType.ConvertPurcentHealInShild;
        if (statusEffect.GetType() == typeof(FauxEffect))
            return StatusEffectType.FauxEffect;

        throw new Exception("getStatusEffectType has no impementation for this !");
    }
}