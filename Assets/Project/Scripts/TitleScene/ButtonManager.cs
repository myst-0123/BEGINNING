using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private DataManager dataManager;

    [SerializeField] private Image resetNoticeImage;
    [SerializeField] private Image quitNoticeImage;

    private void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.Load();
    }
    
    public void StartButtonClick()
    {
        if (!dataManager.saveData.notPlayed)
        {
            resetNoticeImage.gameObject.SetActive(true);
        }
        else
        {
            dataManager.ResetSaveData();
        }
    }

    public void ContinueButtonClick()
    {
        dataManager.Load();
        dataManager.LoadNextScene();
    }

    public void QuitButtonClick()
    {
        quitNoticeImage.gameObject.SetActive(true);
    }

    public void YesButtonClick()
    {
        dataManager.ResetSaveData();
    }

    public void NoButtonClick()
    {
        resetNoticeImage.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NoButtonClick2()
    {
        quitNoticeImage.gameObject.SetActive(false);
    }
}
