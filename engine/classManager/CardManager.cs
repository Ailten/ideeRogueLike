
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
        communCard.Add(new Card(
            cardIllu: SpriteType.CardImg_BatteBulle,
            cardColor: CardColor.Blue,
            cardEdition: CardEdition.Default,
            APCost: 1,
            distanceToUse: new(1, 1),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.Push, 0)
        ));
        communCard.Add(new Card(
            cardIllu: SpriteType.CardImg_BlacASiable,
            cardColor: CardColor.Blue,
            cardEdition: CardEdition.Default,
            APCost: 1,
            distanceToUse: new(1, 3),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.TrapMp, 0)
        ));
        SaveManager.getSave.succes.Where(s => !s.isRareCard()) // push card commun from succes into pool commun card.
            .Select(s => s.getCardUnlocked())
            .Where(c => c != null).Cast<Card>()
            .ToList().ForEach(c => communCard.Add(c));

        rareCard = new();
        rareCard.Add(new Card(
            cardIllu: SpriteType.CardImg_DarunyaNeko,
            cardColor: CardColor.Blue,
            cardEdition: CardEdition.Default,
            APCost: 2,
            distanceToUse: new(1, 1),
            effect: new KeyValuePair<EffectCard, int>(EffectCard.InvokeDarunyaNeko, 0)
        ));
        SaveManager.getSave.succes.Where(s => s.isRareCard()) // push card rare from succes into pool rare card.
            .Select(s => s.getCardUnlocked())
            .Where(c => c != null).Cast<Card>()
            .ToList().ForEach(c => rareCard.Add(c));

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