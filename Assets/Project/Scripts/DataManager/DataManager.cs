using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;
    public SaveData saveData { get; private set; } = new SaveData();
    private string _filePath;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + ".savedata.json";

        if (instance && this != instance)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    private void FlagUpdate()
    {
        saveData.flags["thardOption"] = (saveData.reachedEnds[0] && saveData.reachedEnds[1]);
    }

    public void LoadNextScene()
    {
        saveData.notPlayed = false;
        SceneManager.LoadScene("LoadScene");
    }

    public void ResetSaveData()
    {
        saveData.currentProgress = "S0";
        saveData.reachedEnds = Enumerable.Repeat<bool>(false, 3).ToArray();
        saveData.flags["thardOption"] = false;
        saveData.notPlayed = true;

        Save();
        Load();
        LoadNextScene();
    }

    public void SetCurrentProgress(string str)
    {
        saveData.currentProgress = str;
    }

    public void ReachEnd()
    {
        int endNum = int.Parse(saveData.currentProgress[3].ToString());
        saveData.reachedEnds[endNum-1] = true;
        saveData.ending = true;
    }

    public void StaffRollEnd()
    {
        saveData.ending = false;
        saveData.currentProgress = "S0";
    }

    public void Save()
    {
        FlagUpdate();

        string json = JsonUtility.ToJson(saveData);
        StreamWriter streamWriter = new StreamWriter(_filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            StreamReader streamReader = new StreamReader(_filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            saveData = JsonUtility.FromJson<SaveData>(data);
        }
    }
}
