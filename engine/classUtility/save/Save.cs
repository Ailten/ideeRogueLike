
public class Save
{
    public TimeSpan timePlayed { get; set; } //amount of time played. [V]
    public int runCount { get; set; } //amount of run start. [V]
    public ulong damageMaked { get; set; } //amount of damage maked. [V]
    public uint healMaked { get; set; } //amount of heal maked. [V]
    public uint shildMaked { get; set; } //amount of shild maked. [V]
    public uint cardPlayed { get; set; } //amount of card played.
    public List<Succes> succes { get; set; } //stock eatch succes already unlocked.
    public Dictionary<Type, int> CharacterKilled { get; set; } //amount of kill on eatch characters type. [V]


    public Save()
    {
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
    public uint increaseCardPlayed()
    {
        return ++cardPlayed;
    }

}