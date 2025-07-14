
public enum FxType
{
    FxTurnOn,
    FxTextHit,

    // fx during battle.
    FxStarHit,
    FxShildBuf,
    FxHeartHeal
}

public static class StaticFxType
{
    public static Fx initAnFxOnQueue(this FxType fxType, Vector pos)
    {
        switch (fxType)
        {
            case (FxType.FxStarHit):
                return new FxStarHit(pos, true);
            case (FxType.FxShildBuf):
                return new FxShildBuf(pos, true);
            case(FxType.FxHeartHeal):
                return new FxHeartHeal(pos, true);

            default:
                throw new Exception("initAnFx FxType not instanciable !");
        }
    }
}