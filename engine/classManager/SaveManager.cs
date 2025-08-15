
using System.Text.Json;

public static class SaveManager
{
    private static string currentNameFile = "01";
    private static Save currentSave = new();


    // remap function of save.
    public static Save getSave
    {
        get { return currentSave; }
    }
    public static bool tryAddSucces(Succes succesToAdd) => currentSave.tryAddSucces(succesToAdd);
    public static bool isHasSucces(Succes succesToAsk) => currentSave.isHasSucces(succesToAsk);
    public static int increaseRunCount() => currentSave.increaseRunCount();
    public static int increaseKillCount(Type typeMob) => currentSave.increaseKillCount(typeMob);
    public static int getAmountKillCount(Type typeMob) => currentSave.getAmountKillCount(typeMob);
    public static ulong increaseDamageMaked(int statsIncrease) => currentSave.increaseDamageMaked(statsIncrease);
    public static uint increaseHealMaked(int statsIncrease) => currentSave.increaseHealMaked(statsIncrease);
    public static uint increaseShildMaked(int statsIncrease) => currentSave.increaseShildMaked(statsIncrease);
    public static ulong increaseDamageTaked(int statsIncrease) => currentSave.increaseDamageTaked(statsIncrease);
    public static ulong increaseCoinTaked(int statsIncrease) => currentSave.increaseCoinTaked(statsIncrease);
    public static int increaseCardPlayed(SpriteType cardIllu) => currentSave.increaseCardPlayed(cardIllu);
    public static int getAmountCardPlayed(SpriteType cardIllu) => currentSave.getAmountCardPlayed(cardIllu);

    // add many succes to the list save.
    public static void addSucces(List<Succes> succesToAdd) => currentSave.succes.AddRange(succesToAdd);


    // load and save one file (return true if it's work).
    private static JsonSerializerOptions jsonOption = new JsonSerializerOptions { WriteIndented = true };
    public static bool loadFileSave(string nameFileSave = "01")
    {
        currentNameFile = nameFileSave;

        string path = $"assets/save/{nameFileSave}.json";

        string jsonStr = ""; // read from file.
        try
        {
            if (!File.Exists(path)) // if file not exist, create it.
                File.Create(path);

            using (StreamReader sr = File.OpenText(path))
            {
                string? s;
                while ((s = sr.ReadLine()) != null)
                    jsonStr += s;
            }

            if (jsonStr == "")
                currentSave = new();
            else
                currentSave = JsonSerializer.Deserialize<Save>(jsonStr, jsonOption) ?? throw new Exception("SaveManager fail to load json !");

        }
        catch (JsonException e) // file contend is not matching canvas of json.
        {
            Console.WriteLine($"error during load save : {e.Message}");
            return false;
        }
        catch (Exception e) // unexcepted reason exception.
        {
            Console.WriteLine($"error during load save : {e.Message}");
            return false;
        }

        return true;
    }
    public static void saveFileSave(string? nameFileSave = null)
    {
        nameFileSave ??= currentNameFile;

        currentSave.timePlayed += getTimePlayed(); // edit time played.
        timePlayStart();

        string path = $"assets/save/{nameFileSave}.json"; // set param for write.
        string jsonStr = JsonSerializer.Serialize<Save>(currentSave, jsonOption);

        if (!File.Exists(path)) // create file if not exist.
            File.Create(path);

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


    // get purcent of completion.
    public static float getPurcentCompletion()
    {
        return (float) currentSave.succes.Count / Enum.GetValues<Succes>().Length;
    }
}