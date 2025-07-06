
public static class FxManager
{
    private static Queue<KeyValuePair<FxType, Vector>> queueFxWaiting = new();

    // init an fx by sending his type (on queue, for waiting his turn).
    public static void initOnQueue(FxType fxTypeAsk, Vector pos)
    {
        KeyValuePair<FxType, Vector> param = new(fxTypeAsk, pos);
        queueFxWaiting.Enqueue(param);
        if (queueFxWaiting.Count > 1)
            return;
        fxTypeAsk.initAnFxOnQueue(pos);
    }

    public static void endFxOnqueue()
    {
        if (queueFxWaiting.Count > 0) //dequeue last.
            queueFxWaiting.Dequeue();
            
        if (queueFxWaiting.Count > 0) //start next fx on queue.
        {
            KeyValuePair<FxType, Vector> param = queueFxWaiting.ElementAt(0);
            param.Key.initAnFxOnQueue(param.Value);
        }
    }
}