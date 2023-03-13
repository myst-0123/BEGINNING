using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public SaveData saveData { get; private set; }
    private string _filePath;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + ".savedata.json";
        DontDestroyOnLoad(this);
    }

    public void ResetSaveData()
    {
        saveData.currentProgress = "1";
        saveData.reachedEnds = Enumerable.Repeat<bool>(false, 3).ToArray();
    }

    public void Save()
    {
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
