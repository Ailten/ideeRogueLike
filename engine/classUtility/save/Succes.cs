using System.Text.RegularExpressions;

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
    Heal_40,
    Damage_10,
    Damage_100,
    Damage_200,
    Played_1_Shiny,
    Played_5_Shiny,
    Played_15_Shiny,
    Take_20_Damage,
    Take_40_Damage,
    Take_60_Damage,
    Take_80_Damage,
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
    UseACard_Flame,
    UseACard_5_DarunyaNeko,
    UseACard_5_BlacASiable,
    UseACard_5_BatteBulle,
    UseACard_20_WoodenShild,
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

            case (Succes.UseACard_Flame):
                return new Card(
                    cardIllu: SpriteType.CardImg_OsuAime,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 1,
                    distanceToUse: new(1, 3),
                    effects: new() {
                        new KeyValuePair<EffectCard, int>(EffectCard.PropagatePoison, 2)
                    }
                );

            case (Succes.Take_80_Damage):
                return new Card(
                    cardIllu: SpriteType.CardImg_Dashilios,
                    cardColor: CardColor.Red,
                    cardEdition: CardEdition.Default,
                    APCost: 1,
                    distanceToUse: new(1, 3),
                    effects: new() {
                        new KeyValuePair<EffectCard, int>(EffectCard.TeleportSwitch, 0)
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
            case (Succes.UseACard_Flame):
            case (Succes.UseACard_20_WoodenShild):
            case (Succes.Heal_40):

            case (Succes.UseACard_5_DarunyaNeko):
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
            case (Succes.Heal_40):
                return SaveManager.getSave.healMaked >= 40;
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
                return SaveManager.getSave.damageTaked >= 60;
            case (Succes.Take_80_Damage):
                return SaveManager.getSave.damageTaked >= 80;
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
            case (Succes.UseACard_Flame):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_Flame) >= 1;
            case (Succes.UseACard_5_DarunyaNeko):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_DarunyaNeko) >= 5;
            case (Succes.UseACard_5_BlacASiable):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_BlacASiable) >= 5;
            case (Succes.UseACard_5_BatteBulle):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_BatteBulle) >= 5;
            case (Succes.UseACard_20_WoodenShild):
                return SaveManager.getAmountCardPlayed(SpriteType.CardImg_WoodenShild) >= 20;
                

            default:
                throw new Exception("Succes has no way to be unlocked");
        }
    }

    // get description of what do to unlock success.
    public static string getConditionToUnlock(this Succes succes)
    {
        string succesStr = succes.ToString();
        Match getNumber = new Regex("[0-9]{1,}").Match(succesStr);
        Match getFirstPart = new Regex("^[a-zA-Z]{1,}").Match(succesStr);
        Match getLastPart = new Regex("[a-zA-Z]{1,}$").Match(succesStr);
        int amountNeed = (
            (getNumber.Length > 0)? Int32.Parse(getNumber.Groups[0].Value):
            1
        );
        string firstPart = (
            (getFirstPart.Length > 0)? getFirstPart.Groups[0].Value:
            ""
        );
        string lastPart = (
            (getLastPart.Length > 0)? getLastPart.Groups[0].Value:
            ""
        );

        if (firstPart == "Kill") // ex: Kill_5_Slime.
            return $"Tuer {amountNeed} {lastPart}";
        if (firstPart == "Take" && lastPart == "Coin") // ex: Take_10_Coin.
            return $"Colecter {amountNeed} pieces d'or";
        if (firstPart == "Take" && lastPart == "Damage") // ex: Take_20_Damage.
            return $"Encaisser {amountNeed} points de degats";
        if (firstPart == "Heal") // ex: Heal_10.
            return $"Soigner {amountNeed} points de vie";
        if (firstPart == "Damage") // ex: Damage_10.
            return $"Infliger {amountNeed} points de degats";
        if (firstPart == "Played" && lastPart == "Shiny") // ex: Played_1_Shiny.
            return $"Jouer {amountNeed} cartes brillantes";
        if (firstPart == "Played" && lastPart == "Cracked") // ex: Played_1_Cracked.
            return $"Jouer {amountNeed} cartes fissurees";
        if (firstPart == "RunPlayed") // ex: RunPlayed_5.
            return $"Jouer {amountNeed} parties";
        if (firstPart == "UseACard") // ex: UseACard_DarunyaNeko || UseACard_5_DarunyaNeko.
            return $"Jouer {amountNeed} fois la carte {lastPart}";


        throw new Exception("Can't getConditionToUnlock !");
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
            case (Succes.Heal_40):
                return StatusEffectType.ConvertPurcentHealInShild;
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

            case (Succes.UseACard_5_DarunyaNeko):
                return StatusEffectType.BoostIntoInvoke;
            case (Succes.UseACard_5_BlacASiable):
                return StatusEffectType.RallMpMakeDamage;
            case (Succes.UseACard_5_BatteBulle):
                return StatusEffectType.PushWallMakeRallMP;
            case (Succes.UseACard_20_WoodenShild):
                return StatusEffectType.ShildMultWhenFirst;

            default:
                return null;
        }
    }
    
}