
public class Fx : Entity
{

    public Fx(SpriteType spriteType) : base(RunLayer.layer.idLayer, spriteType)
    {
        timeStartAnime = UpdateManager.timeSpeedForAnime(RunLayer.layer.milisecInLevel);

        this.size = new(0, 0);
        this.zIndex = 1400; //character 1200. UI 2000. (1400 base)

        EntityManager.sortAllEntities();
    }


    private int timeStartAnime;
    private int timeAnimeDelay;

    protected void setTimeAnimeDelay(float timeAnimeDelayFloat)
    {
        timeAnimeDelay = (int)(timeAnimeDelayFloat * 1000);
    }


    // call in first of drawAfter for get the I of delay anime (and can destroy object).
    protected float getTimeI()
    {
        int timeAnimeSpeeded = UpdateManager.timeSpeedForAnime(RunLayer.layer.milisecInLevel);
        float i = (float)(timeAnimeSpeeded - timeStartAnime) / timeAnimeDelay;
        if(i < 0f || i > 1f)
            EntityManager.removeOneEntity(this);
        return i;
    }

}