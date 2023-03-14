using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BattleScene
{
    public class GameManager : MonoBehaviour
    {
        private enum Stage
        {
            one,
            twoOne,
            twoTwo,
            threeOne,
            threeTwo,
            threeThree
        }

        [SerializeField] private GameObject[] _enemyPrefabs = new GameObject[6];
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Canvas _gameoverWindow;
        [SerializeField] private Canvas _tutorialWindow;
        [SerializeField] private Image _image;
        [SerializeField] private Image _readyImage;
        [SerializeField] private Image _fightImage;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _loopCount;
        
        private GameObject _enemies;

        private DataManager _dataManager;
        private Stage _stage;
        private float _waitTime;
        private float _interval;

        private void Start()
        {
            _waitTime = _fadeTime / _loopCount;
            _interval = 255.0f / _loopCount;
            _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

            GetStage();
            InstantiateEnemy(_stage);
            StartCoroutine(FadeIn());
        }

        private void GetStage()
        {
            switch(_dataManager.saveData.currentProgress)
            {
                case "B1":
                    _stage = Stage.one;
                    break;
                case "B2-1":
                case "B2-2":
                    _stage = Stage.twoOne;
                    break;
                case "B2-3":
                    _stage = Stage.twoTwo;
                    break;
                case "B3-1":
                    _stage = Stage.threeOne;
                    break;
                case "B3-2":
                    _stage = Stage.threeTwo;
                    break;
                case "B3-3":
                    _stage = Stage.threeThree;
                    break;
            }
        }

        private void InstantiateEnemy(Stage stage)
        {
            _enemies = Instantiate(_enemyPrefabs[(int)stage]);            
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToTitle()
        {
            _dataManager.Save();
            SceneManager.LoadScene("TitleScene");
        }

        IEnumerator BattleLose()
        {
            _gameoverWindow.gameObject.SetActive(true);

            float interval = 130.0f / _loopCount;

            _image.color = new Color(0, 0, 0, 0);

            for (float a = 0; a <= 130.0f; a += interval)
            {
                yield return new WaitForSeconds(_waitTime);

                Color newColor = _image.color;
                newColor.a = a / 255.0f;
                _image.color = newColor;
            }
        }

        IEnumerator BattleWin()
        {
            IEnumerator enumerator = FadeOut();
            Coroutine coroutine = StartCoroutine(enumerator);
            yield return coroutine;

            string newProgress = _dataManager.saveData.currentProgress;
            newProgress = newProgress.Replace("B", "S");
            _dataManager.SetCurrentProgress(newProgress);

            SceneManager.LoadScene("LoadScene");
        }

        IEnumerator FadeIn()
        {
            float _velocity = 8.0f / _fadeTime;
            float velocityReductionRate = _velocity / (_fadeTime * _loopCount);

            _image.color = new Color(0, 0, 0, 1);

            for (float a = 255.0f; a >= 0; a -= _interval)
            {
                yield return new WaitForSeconds(_waitTime);

                _playerObject.transform.position += new Vector3(_velocity, 0, 0) * Time.deltaTime;

                _velocity -= velocityReductionRate;

                Color newColor = _image.color;
                newColor.a = a / 255.0f;
                _image.color = newColor;
            }

            yield return new WaitForSeconds(0.2f);

            _readyImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.0f);

            _readyImage.gameObject.SetActive(false);
            _fightImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.7f);

            _fightImage.gameObject.SetActive(false);

            if (_dataManager.saveData.notPlayed)
            {
                _dataManager.Played();
                _tutorialWindow.gameObject.SetActive(true);
                yield return StartCoroutine(_tutorialWindow.gameObject.GetComponent<Tutorial>().WaitClick());
                _tutorialWindow.gameObject.SetActive(false);
            }

            _playerObject.GetComponent<PlayerController>().enabled = true;
            _enemies.GetComponent<EnemiesController>().SetPermittionTrue();
        }

        IEnumerator FadeOut()
        {
            _image.color = new Color(0, 0, 0, 0);

            for (float a = 0.0f; a <= 255.0; a += _interval)
            {
                yield return new WaitForSeconds(_waitTime);

                Color newColor = _image.color;
                newColor.a = a / 255.0f;
                _image.color = newColor;
            }
        }
    }
}
