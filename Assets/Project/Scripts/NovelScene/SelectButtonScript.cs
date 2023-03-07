using UnityEngine;

namespace NovelScene
{
    public class SelectButtonScript : MonoBehaviour
    {
        int lineNum = 2;

        public void setLineNum(int num)
        {
            lineNum = num;
        }

        public void OnClick()
        {
            GameManager.Instance.mainTextController.GoToLine(lineNum);
            GameManager.Instance.selectButtonManager.deleteAllButton();
        }
    }
}
