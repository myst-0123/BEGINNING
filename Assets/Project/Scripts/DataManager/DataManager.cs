using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private SaveData _saveData;
    private string _filePath;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + ".savedata.json";
        DontDestroyOnLoad(this);
    }

    public void ResetSaveData()
    {
        _saveData.currentProgress = "1";
        _saveData.reachedEnds = Enumerable.Repeat<bool>(false, 3).ToArray();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_saveData);
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
            _saveData = JsonUtility.FromJson<SaveData>(data);
        }
    }
}
