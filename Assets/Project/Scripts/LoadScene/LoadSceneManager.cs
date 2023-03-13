using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private DataManager _dataManager;
    private AsyncOperation _loadScene;

    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    IEnumerator LoadScene()
    {
        switch(_dataManager.saveData.currentProgress[0])
        {
            case 'S':
                _loadScene = SceneManager.LoadSceneAsync("NovelScene");
                break;
            case 'B':
                _loadScene = SceneManager.LoadSceneAsync("BattleScene");
                break;
        }

        while (!_loadScene.isDone)
        {
            _text.text = "NowLoading.";
            yield return new WaitForSeconds(0.6f);
            _text.text = "NowLoading..";
            yield return new WaitForSeconds(0.6f);
            _text.text = "NowLoading...";
            yield return new WaitForSeconds(0.6f);
        }
    }

}
