
public enum CardColor
{
    Blue,
    Red,
    Green,
    PolyChrome,

}

public static class StaticCardColor
{
    public static SpriteType getSpriteType(this CardColor cardColor)
    {
        switch (cardColor)
        {
            case (CardColor.Blue):
                return SpriteType.CardBG_Blue;
            case (CardColor.Red):
                return SpriteType.CardBG_Red;
            case (CardColor.Green):
                return SpriteType.CardBG_Green;
            case (CardColor.PolyChrome):
                return SpriteType.CardBG_PolyChrome;
            default:
                throw new Exception("CardColor.getSpriteType found no match !");
        }
    }

    public static bool isMatchingColor(this CardColor cardColor, CardColor cardColorAsk)
    {
        if (cardColor == CardColor.PolyChrome)
            return true;
        return cardColor == cardColorAsk;
    }

    public static CardColor getRandomColor(bool isIncludePolyChrome = false, int purcentChanceOfBeingPolyChrome = 12)
    {
        bool isPolyChrome = (!isIncludePolyChrome)? false: (RandomManager.rng.Next(100) < purcentChanceOfBeingPolyChrome);
        if (isPolyChrome)
            return CardColor.PolyChrome;
            
        int rng = RandomManager.rng.Next(3);
        return (
            (rng == 0)? CardColor.Blue:
            (rng == 1)? CardColor.Red:
            CardColor.Green
        );
    }

}