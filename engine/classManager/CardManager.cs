
public static class CardManager
{
    private static List<Card> communCard = new();
    private static List<Card> rareCard = new();


    // call in start run for fill pool of card (depend on succes unlock).
    public static void initCards()
    {
        communCard = new();
        /*/ --- not include default wooden card ---
        communCard.Add(new Card(
            cardIllu: SpriteType.CardImg_WoodenSword,
            cardColor: CardColor.Red,
            cardEdition: CardEdition.Default,
            APCost: 1,
            distanceToUse: new(1, 1),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 2)
        ));
        communCard.Add(new Card(
            cardIllu: SpriteType.CardImg_WoodenShild,
            cardColor: CardColor.Red,
            cardEdition: CardEdition.Default,
            APCost: 1,
            distanceToUse: new(0, 0),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.Shild, 2)
        ));
        //*/
        communCard.Add(new Card(
            cardIllu: SpriteType.CardImg_Meat,
            cardColor: CardColor.Red,
            cardEdition: CardEdition.Default,
            APCost: 2,
            distanceToUse: new(0, 0),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.Heal, 6)
        ));
        if (SaveManager.isHasSucces(Succes.Kill_5_Slime))
        {
            communCard.Add(new Card( //condition when kill 5 slimes.
                cardIllu: SpriteType.CardImg_Splash,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(2, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 3)
            ));
        }
        if (SaveManager.isHasSucces(Succes.Kill_5_Rock))
        {
            communCard.Add(new Card( //condition when kill 5 rock.
                cardIllu: SpriteType.CardImg_Rock,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.MPHit, 1)
            ));
        }
        if (SaveManager.isHasSucces(Succes.Kill_5_Flame))
        {
            communCard.Add(new Card( //condition when kill 5 flame.
                cardIllu: SpriteType.CardImg_Flame,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Burn, 3)
            ));
        }

        rareCard = new();
        if (SaveManager.isHasSucces(Succes.Take_10_Coin)) {
            rareCard.Add(new Card(
                cardIllu: SpriteType.CardImg_AxeOLoot,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effects: new() {
                    new KeyValuePair<EffectCard, int>(EffectCard.MoneyLoot, 2),
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4)
                }
            ));
        }
    }

    public static Card generateARandomCard()
    {
        bool isARareCard = (rareCard.Count == 0)? false: RandomManager.rng.Next(1000) < 120;
        int indexCardPick = RandomManager.rng.Next(
            (isARareCard)? rareCard.Count: communCard.Count
        );
        
        Card output = (isARareCard)? rareCard[indexCardPick]: communCard[indexCardPick];

        output.cardColor = StaticCardColor.getRandomColor(true);
        output.cardEdition = StaticCardEdition.getRandomEdition(false);

        return output;
    }

}