
// eatch succes in game.
public enum Succes
{
    Kill_5_Slime,
    Kill_5_Flame,
    Kill_5_Rock,
    Take_10_Coin,
    Take_100_Coin,
    Heal_10,
    Heal_20,
    Heal_30,
    Damage_10,
    Damage_100,
    Damage_200,
    Played_1_Shiny,
    Played_5_Shiny,
    Played_15_Shiny,
    Take_20_Damage,
    Take_40_Damage,
    Take_60_Damage,
    Take_100_Damage,
    Played_1_Cracked,
    Played_5_Cracked,
    Played_10_Cracked,
    Played_15_Cracked,
    RunPlayed_5,
    RunPlayed_10,
    RunPlayed_15,

    UseACard_DarunyaNeko,
    UseACard_AxeOLoot,
    UseACard_BlacASiable,
    UseACard_Barbak,
    UseACard_DarunyaNeko_5,
    UseACard_BlacASiable_5,
    UseACard_BatteBulle_5,
}


public static class StaticSucces
{
    public static Card? getCardUnlocked(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.Kill_5_Slime):
                return new Card(
                    cardIllu: SpriteType.CardImg_Splash,
                    cardColor: CardColor.Blue,
                    cardEdition: CardEdition.Default,
                    APCost: 2,
                    distanceToUse: new(2, 2),
                    effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 3)
                );
            case (Succes.Kill_5_Flame):
                return new Card(
                    cardIllu: SpriteType.CardImg_Flame,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 1,
                    distanceToUse: new(1, 1),
                    effect: new KeyValuePair<EffectCard, int>(EffectCard.Burn, 3)
                );
            case (Succes.Kill_5_Rock):
                return new Card(
                    cardIllu: SpriteType.CardImg_Rock,
                    cardColor: CardColor.Green,
                    cardEdition: CardEdition.Default,
                    APCost: 1,
                    distanceToUse: new(1, 1),
                    effect: new KeyValuePair<EffectCard, int>(EffectCard.MPHit, 1)
                );

            case (Succes.Take_10_Coin):
                return new Card(
                    cardIllu: SpriteType.CardImg_AxeOLoot,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 2,
                    distanceToUse: new(1, 1),
                    effects: new() {
                        new KeyValuePair<EffectCard, int>(EffectCard.MoneyLoot, 2),
                        new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4)
                    }
                );

            case (Succes.Heal_10):
                return new Card(
                    cardIllu: SpriteType.CardImg_LuneAllier,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 2,
                    distanceToUse: new(1, 2),
                    effects: new() {
                        new KeyValuePair<EffectCard, int>(EffectCard.InvokeLuneAllier, 2)
                    }
                );

            case (Succes.Damage_10):
                return new Card(
                    cardIllu: SpriteType.CardImg_Explsur,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 2,
                    distanceToUse: new(0, 0),
                    effects: new() {
                        new KeyValuePair<EffectCard, int>(EffectCard.Hit, 1),
                        new KeyValuePair<EffectCard, int>(EffectCard.HitAround, 3)
                    }
                );

            default:
                return null;
        }
    }

    public static bool isRare(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.Take_10_Coin):
            case (Succes.Take_100_Coin):
            case (Succes.Heal_30):
            case (Succes.Played_5_Shiny):
            case (Succes.Played_15_Shiny):
            case (Succes.Take_100_Damage):
            case (Succes.Played_10_Cracked):
            case (Succes.RunPlayed_15):
            case (Succes.Damage_100):

            case (Succes.UseACard_DarunyaNeko_5):
                return true;

            default:
                return false;
        }
    }

    public static bool isUnlocked(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.Kill_5_Slime):
                return SaveManager.getAmountKillCount(typeof(CharacterSlime)) >= 5;
            case (Succes.Kill_5_Flame):
                return SaveManager.getAmountKillCount(typeof(CharacterFlame)) >= 5;
            case (Succes.Kill_5_Rock):
                return SaveManager.getAmountKillCount(typeof(CharacterRock)) >= 5;
            case (Succes.Take_10_Coin):
                return SaveManager.getSave.coinTaked >= 10;
            case (Succes.Take_100_Coin):
                return SaveManager.getSave.coinTaked >= 100;
            case (Succes.Heal_10):
                return SaveManager.getSave.healMaked >= 10;
            case (Succes.Heal_20):
                return SaveManager.getSave.healMaked >= 20;
            case (Succes.Heal_30):
                return SaveManager.getSave.healMaked >= 30;
            case (Succes.Damage_10):
                return SaveManager.getSave.damageMaked >= 10;
            case (Succes.Damage_100):
                return SaveManager.getSave.damageMaked >= 100;
            case (Succes.Damage_200):
                return SaveManager.getSave.damageMaked >= 200;
            case (Succes.Played_1_Shiny):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Shinny) >= 1;
            case (Succes.Played_5_Shiny):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Shinny) >= 5;
            case (Succes.Played_15_Shiny):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Shinny) >= 25;
            case (Succes.Take_20_Damage):
                return SaveManager.getSave.damageTaked >= 20;
            case (Succes.Take_40_Damage):
                return SaveManager.getSave.damageTaked >= 40;
            case (Succes.Take_60_Damage):
                return SaveManager.getSave.damageTaked >= 40;
            case (Succes.Take_100_Damage):
                return SaveManager.getSave.damageTaked >= 100;
            case (Succes.Played_1_Cracked):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Cracked) >= 1;
            case (Succes.Played_5_Cracked):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Cracked) >= 5;
            case (Succes.Played_10_Cracked):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Cracked) >= 10;
            case (Succes.Played_15_Cracked):
                return SaveManager.getAmountCardEditionPlayed(CardEdition.Cracked) >= 15;
            case (Succes.RunPlayed_5):
                return SaveManager.getSave.runCount >= 5;
            case (Succes.RunPlayed_10):
                return SaveManager.getSave.runCount >= 10;
            case (Succes.RunPlayed_15):
                return SaveManager.getSave.runCount >= 15;


            case (Succes.UseACard_DarunyaNeko):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_DarunyaNeko) >= 1;
            case (Succes.UseACard_AxeOLoot):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_AxeOLoot) >= 1;
            case (Succes.UseACard_BlacASiable):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_BlacASiable) >= 1;
            case (Succes.UseACard_Barbak):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_Barbak) >= 1;
            case (Succes.UseACard_DarunyaNeko_5):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_DarunyaNeko) >= 5;
            case (Succes.UseACard_BlacASiable_5):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_BlacASiable) >= 5;
            case (Succes.UseACard_BatteBulle_5):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_BatteBulle) >= 5;

            default:
                throw new Exception("Succes has no way to be unlocked");
        }
    }

    // get a spriteType from an enum succes (for set list character in main menu start run).
    public static SpriteType? getCharacterUnlocked(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.UseACard_DarunyaNeko):
                return SpriteType.Character_DarumaNico;
            case (Succes.UseACard_AxeOLoot):
                return SpriteType.Character_Axolootl;
            case (Succes.UseACard_BlacASiable):
                return SpriteType.Character_Blacacia;
            case (Succes.UseACard_Barbak):
                return SpriteType.Character_Barbak;

            default:
                return null;
        }
    }

    // get StatusEffectType by succes.
    public static StatusEffectType? getStatusEffectUnlocked(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.Heal_20):
                return StatusEffectType.PushWallMakeSelfHeal;
            case (Succes.Heal_30):
                return StatusEffectType.TakeHealMakeHitAround;
            case (Succes.Damage_100):
                return StatusEffectType.SideEyes;
            case (Succes.Damage_200):
                return StatusEffectType.AddIndirectDamage;
            case (Succes.Take_100_Coin):
                return StatusEffectType.MoneyMultiplyDamage;
            case (Succes.Played_1_Shiny):
                return StatusEffectType.DamageAddBoostShiny;
            case (Succes.Played_5_Shiny):
                return StatusEffectType.DamageMultBoostShiny;
            case (Succes.Played_15_Shiny):
                return StatusEffectType.ShinyGainAP;
            case (Succes.Take_20_Damage):
                return StatusEffectType.ShildAddBoostColor_Red;
            case (Succes.Take_40_Damage):
                return StatusEffectType.ShildMultBoostColor_Red;
            case (Succes.Take_60_Damage):
                return StatusEffectType.MultDamageByHPLeft;
            case (Succes.Take_100_Damage):
                return StatusEffectType.APWhenHit;
            case (Succes.Played_1_Cracked):
                return StatusEffectType.CrackedAddDamage;
            case (Succes.Played_5_Cracked):
                return StatusEffectType.BrokeCardGainShild;
            case (Succes.Played_10_Cracked):
                return StatusEffectType.DuplicateCracked;
            case (Succes.Played_15_Cracked):
                return StatusEffectType.YingYangShinyCracked;
            case (Succes.RunPlayed_5):
                return StatusEffectType.BoostChooseSpecialRoom;
            case (Succes.RunPlayed_10):
                return StatusEffectType.BoostPickCard;
            case (Succes.RunPlayed_15):
                return StatusEffectType.BalanceEffect;

            case (Succes.UseACard_DarunyaNeko_5):
                return StatusEffectType.BoostIntoInvoke;
            case (Succes.UseACard_BlacASiable_5):
                return StatusEffectType.RallMpMakeDamage;
            case (Succes.UseACard_BatteBulle_5):
                return StatusEffectType.PushWallMakeRallMP;

            default:
                return null;
        }
    }
    
}