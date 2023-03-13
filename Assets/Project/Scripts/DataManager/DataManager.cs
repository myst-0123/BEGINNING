using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public SaveData saveData { get; private set; }
    private string _filePath;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + ".savedata.json";
        DontDestroyOnLoad(this);
    }

    private void FlagUpdate()
    {
        saveData.flags["thardOption"] = (saveData.reachedEnds[0] && saveData.reachedEnds[1]);
    }

    public void ResetSaveData()
    {
        saveData.currentProgress = "S1";
        saveData.reachedEnds = Enumerable.Repeat<bool>(false, 3).ToArray();
        saveData.flags["thardOption"] = false;
        saveData.notPlayed = true;

        Save();
        Load();
    }

    public void SetCurrentProgress(string str)
    {
        saveData.currentProgress = str;
    }

    public void ReachEnd()
    {
        int endNum = int.Parse(saveData.currentProgress[3].ToString());
        saveData.reachedEnds[endNum-1] = true;
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
        saveData.notPlayed = false;

        if (File.Exists(_filePath))
        {
            StreamReader streamReader = new StreamReader(_filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            saveData = JsonUtility.FromJson<SaveData>(data);
        }

        SceneManager.LoadScene("LoadScene");
    }
}
