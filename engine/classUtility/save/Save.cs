
public class Save
{
    public TimeSpan timePlayed { get; set; } //amount of time played.
    public int runCount { get; set; } //amount of run start.
    public ulong damageMaked { get; set; } //amount of damage maked.
    public uint healMaked { get; set; } //amount of heal maked.
    public uint shildMaked { get; set; } //amount of shild maked.
    public List<Succes> succes { get; set; }
    public Dictionary<Type, int> CharacterKilled { get; set; }


    public Save()
    {
        this.timePlayed = new();
        this.runCount = 0;
        this.damageMaked = 0;
        this.healMaked = 0;
        this.shildMaked = 0;
        this.succes = new();
        this.CharacterKilled = new();
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