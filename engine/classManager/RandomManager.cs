
public static class RandomManager
{
    private static Random _rng = new Random(new DateTime().Millisecond);

    public static Random rng
    {
        get { return _rng; }
    }


}