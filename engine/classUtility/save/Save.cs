
public class Save
{
    public TimeSpan timePlayed; //amount of time played.
    public int runCount = 0; //amount of run start.
    public ulong damageMaked = 0; //amount of damage maked.
    public uint healMaked = 0; //amount of heal maked.
    public uint shildMaked = 0; //amount of shild maked.
    private List<Succes> succes = new();
    private Dictionary<Type, int> CharacterKilled = new();


    // try to add a succes to succesSave, return if the add work.
    public bool tryAddSucces(Succes succesToAdd)
    {
        if (isHasSucces(succesToAdd))
            return false;
        succes.Add(succesToAdd);
        return true;
    }
    // ask if the succes send is already on list succesSave.
    public bool isHasSucces(Succes succesToAsk)
    {
        return succes.Contains(succesToAsk);
    }

    // increase run count.
    public void increaseRunCount()
    {
        runCount++;
    }

    // increase kill count character.
    public int increaseKillCount(Type typeMobKilled)
    {
        if (CharacterKilled.ContainsKey(typeMobKilled))
            return ++CharacterKilled[typeMobKilled];

        CharacterKilled.Add(typeMobKilled, 1);
        return 1;
    }

    // increase stats count maked.
    public ulong increaseDamageMaked(uint amountIncrease)
    {
        damageMaked += (ulong)amountIncrease;
        return damageMaked;
    }
    public uint increaseHealMaked(uint amountIncrease)
    {
        healMaked += amountIncrease;
        return healMaked;
    }
    public uint increaseShildMaked(uint amountIncrease)
    {
        shildMaked += amountIncrease;
        return shildMaked;
    }

}