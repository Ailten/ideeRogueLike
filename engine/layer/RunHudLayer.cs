
public class RunHudLayer : Layer
{

    private static RunHudLayer _layer = new RunHudLayer(){ layerName="RunHudLayer" };
    public static RunHudLayer layer 
    {
        get { return _layer; }
    }


    public override void active()
    {
        //init all entities of layer. --->

        StatsCharacterUi statsCharacterUI = new StatsCharacterUi(idLayer); //sprite of HP and SP player.

        MiniMapUi miniMapUi = new MiniMapUi(idLayer); //mini map.
        miniMapUi.pos = new(1280, 0);

        
        base.active();
    }

    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        base.unActive();
    }

}