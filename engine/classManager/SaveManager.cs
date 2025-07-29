
using System.Text.Json;

public static class SaveManager
{
    private static string currentNameFile = "01";
    private static Save currentSave = new();


    // remap function of save.
    public static bool tryAddSucces(Succes succesToAdd) => currentSave.tryAddSucces(succesToAdd);
    public static bool isHasSucces(Succes succesToAsk) => currentSave.isHasSucces(succesToAsk);
    public static void increaseRunCount() => currentSave.increaseRunCount();
    public static int increaseKillCount(Type typeMobKilled) => currentSave.increaseKillCount(typeMobKilled);
    public static ulong increaseDamageMaked(uint statsIncrease) => currentSave.increaseDamageMaked(statsIncrease);
    public static uint increaseHealMaked(uint statsIncrease) => currentSave.increaseHealMaked(statsIncrease);
    public static uint increaseShildMaked(uint statsIncrease) => currentSave.increaseShildMaked(statsIncrease);
    


    // load and save one file.
    public static void loadFileSave(string nameFileSave = "01")
    {
        currentNameFile = nameFileSave;

        string path = $"assets/save/{nameFileSave}.json";

        string jsonStr = ""; // read from file.
        using (StreamReader sr = File.OpenText(path))
        {
            string? s;
            while ((s = sr.ReadLine()) != null)
                jsonStr += s;
        }

        currentSave = JsonSerializer.Deserialize<Save>(jsonStr) ?? throw new Exception("SaveManager fail to load json !");
    }
    public static void saveFileSave(string? nameFileSave = null)
    {
        nameFileSave ??= currentNameFile;

        currentSave.timePlayed += getTimePlayed(); // edit time played.
        timePlayStart();

        string path = $"assets/save/{nameFileSave}.json"; // set param for write.
        string jsonStr = JsonSerializer.Serialize(currentSave);

        using (var sw = new StreamWriter(path)) // write on file.
        {
            sw.WriteLine(jsonStr);
        }
    }


    // time played.
    private static DateTime timeStartWindow;
    public static void timePlayStart()
    {
        timeStartWindow = DateTime.Now;
    }
    private static TimeSpan getTimePlayed()
    {
        TimeSpan timePlayed = DateTime.Now - timeStartWindow;
        return timePlayed;
    }
}