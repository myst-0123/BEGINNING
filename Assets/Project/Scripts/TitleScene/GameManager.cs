using UnityEngine;
using UnityEngine.UI;

namespace TitleScene
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] Sprite _sprite2;
        [SerializeField] Image _background;

        private DataManager _dataManager;

        private void Start()
        {
            _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

            _dataManager.Load();
            if (_dataManager.saveData.reachedEnds[2])
            {
                _background.sprite = _sprite2;
            }
            else
            {
                _background.sprite = _sprite;
            }
        }
    }
}
