using UnityEngine;

namespace NovelScene
{
    public class SelectButtonScript : MonoBehaviour
    {
        string label = "None";

        public void setLabel(string l)
        {
            label = l;
        }

        public void OnClick()
        {
            GameManager.Instance.userScriptManager.GoToLine(label);
            GameManager.Instance.selectButtonManager.deleteAllButton();
        }
    }
}
