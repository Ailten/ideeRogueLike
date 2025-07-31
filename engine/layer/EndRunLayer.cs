
public class EndRunLayer : Layer
{
    private static EndRunLayer _layer = new EndRunLayer() { layerName = "EndRunLayer" };
    public static EndRunLayer layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->


        // print an UI screen WIN or LOOSE.
        // with button back to main menu.


        base.active();
    }


    // params.
    private bool isRunWin;
    private int seedRunEnd;
    private List<Succes> succesUnlockDuringTheRun = new();
    private TimeSpan timeInRun;

    // set params for print end run layer.
    public void setIsRunWin(bool isRunWin) => this.isRunWin = isRunWin;
    public void setSeedRunEnd(int seedRunEnd) => this.seedRunEnd = seedRunEnd;
    public void setSuccesUnlockDuringTheRun(List<Succes> succesUnlockDuringTheRun) => this.succesUnlockDuringTheRun = succesUnlockDuringTheRun;
    public void setTimeInRun(TimeSpan timeInRun) => this.timeInRun = timeInRun;


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->
        isRunWin = false;
        seedRunEnd = 0;
        succesUnlockDuringTheRun = new();
        timeInRun = 0;

        base.unActive();
    }

}