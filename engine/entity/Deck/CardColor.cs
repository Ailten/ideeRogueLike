
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
    
    private static Random? _rand = null;
    public static Random rand
    {
        get
        {
            if (_rand == null)
                _rand = new Random(RunManager.seed);
            return _rand;
        }
    }

    public static CardColor getRandomColor(bool isIncludePolyChrome = false)
    {
        List<CardColor> rangeColor = new() { CardColor.Blue, CardColor.Red, CardColor.Green };
        if (isIncludePolyChrome){
            rangeColor.AddRange(new CardColor[] { CardColor.Blue, CardColor.Red, CardColor.Green });
            rangeColor.Add(CardColor.PolyChrome); // 1/7 rng.
        }

        int randomIndex = rand.Next(rangeColor.Count);
        return rangeColor[randomIndex];
    }

}