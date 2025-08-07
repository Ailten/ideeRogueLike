
// eatch succes in game.
public enum Succes
{
    Kill_5_Slime,
    Kill_5_Flame,
    Kill_5_Rock,
    Take_10_Coin,
    Heal_10,
    Damage_10,
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

    public static bool isRareCard(this Succes succes)
    {
        switch (succes)
        {
            case (Succes.Take_10_Coin):
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
                return SaveManager.getSave.characterKilled["Slime"] >= 5;
            case (Succes.Kill_5_Flame):
                return SaveManager.getSave.characterKilled["Flame"] >= 5;
            case (Succes.Kill_5_Rock):
                return SaveManager.getSave.characterKilled["Rock"] >= 5;
            case (Succes.Take_10_Coin):
                return SaveManager.getSave.coinTaked >= 10;
            case (Succes.Heal_10):
                return SaveManager.getSave.healMaked >= 10;
            case (Succes.Damage_10):
                return SaveManager.getSave.damageMaked >= 10;

            default:
                throw new Exception("Succes has no way to be unlocked");
        }
    }
    
}