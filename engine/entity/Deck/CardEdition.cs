
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

    public static CardEdition getRandomEdition(bool isOnlySpecialEdition = false ,int purcentChanceOfBeingSpecialEdition = 12)
    {
        bool isSpecialEdition = (isOnlySpecialEdition)? true: (RandomManager.rng.Next(100) < purcentChanceOfBeingSpecialEdition);
        if (!isSpecialEdition)
            return CardEdition.Default;

        int rng = RandomManager.rng.Next(2);
        return (
            (rng == 0)? CardEdition.Cracked:
            CardEdition.Shinny
        );
    }
}