
public enum CardEdition
{
    Default,
    Cracked,
    Shinny

}

public static class StaticCardEdition
{
    public static SpriteType getSpriteType(this CardEdition cardEdition)
    {
        switch (cardEdition)
        {
            case (CardEdition.Default):
                return SpriteType.none;
            case (CardEdition.Cracked):
                return SpriteType.CardBG_Craced;
            case (CardEdition.Shinny):
                return SpriteType.CardBG_Shinny;
            default:
                throw new Exception("CardEdition.getSpriteType found no match !");
        }
    }

    public static CardEdition getRandomEdition()
    {
        List<CardEdition> rangeEdition = new() {
            CardEdition.Default,
            CardEdition.Default,
            CardEdition.Default,
            CardEdition.Default,
            CardEdition.Cracked,
            CardEdition.Shinny
        };

        int randomIndex = StaticCardColor.rand.Next(rangeEdition.Count);
        return rangeEdition[randomIndex];
    }
}