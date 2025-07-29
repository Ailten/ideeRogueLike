
[Serializable()] 
public class Save
{
    public TimeSpan timePlayed { get; set; } //amount of time played.
    private int runCount { get; set; } //amount of run start.
    public ulong damageMaked { get; set; } //amount of damage maked.
    public uint healMaked { get; set; } //amount of heal maked.
    public uint shildMaked { get; set; } //amount of shild maked.
    public ulong damageTaked { get; set; } //amount of damage taked.
    public uint coinTaked { get; set; } //amount of coin taked.
    public uint cardPlayed { get; set; } //amount of card played.
    public List<Succes> succes { get; set; } //stock eatch succes already unlocked.
    public Dictionary<string, int> characterKilled { get; set; } //amount of kill on eatch characters type.


    public Save()
    {
        this.succes = new();
        this.characterKilled = new();
    }


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
    public int increaseRunCount()
    {
        return ++runCount;
    }

    // increase kill count character.
    public int increaseKillCount(string nameMobKilled)
    {
        if (characterKilled.ContainsKey(nameMobKilled))
            return ++characterKilled[nameMobKilled];

        characterKilled.Add(nameMobKilled, 1);
        return 1;
    }

    // increase stats count maked.
    public ulong increaseDamageMaked(int amountIncrease)
    {
        damageMaked += (ulong)amountIncrease;
        return damageMaked;
    }
    public uint increaseHealMaked(int amountIncrease)
    {
        healMaked += (uint)amountIncrease;
        return healMaked;
    }
    public uint increaseShildMaked(int amountIncrease)
    {
        shildMaked += (uint)amountIncrease;
        return shildMaked;
    }
    public ulong increaseDamageTaked(int amountIncrease)
    {
        damageTaked += (ulong)amountIncrease;
        return damageTaked;
    }
    public uint increaseCoinTaked(int amountIncrease)
    {
        coinTaked += (uint)amountIncrease;
        return coinTaked;
    }
    public uint increaseCardPlayed()
    {
        return ++cardPlayed;
    }

}