
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

    public static CardEdition getRandomEdition(bool isOnlySpecialEdition = false ,int purcentChanceOfBeingSpecialEdition = 12, Random? rng = null)
    {
        rng ??= RandomManager.rng;

        bool isSpecialEdition = (isOnlySpecialEdition)? true: (rng.Next(100) < purcentChanceOfBeingSpecialEdition);
        if (!isSpecialEdition)
            return CardEdition.Default;

        int rngEdition = rng.Next(2);
        return (
            (rngEdition == 0)? CardEdition.Cracked:
            CardEdition.Shinny
        );
    }
}