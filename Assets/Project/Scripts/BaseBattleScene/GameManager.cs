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
            two,
            threeOne,
            threeTwo,
            threeThree
        }

        [SerializeField] private GameObject[] _enemyPrefabs = new GameObject[5];
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Canvas _gameoverWindow;
        [SerializeField] private Image _image;
        [SerializeField] private Image _readyImage;
        [SerializeField] private Image _fightImage;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _loopCount;
        
        private GameObject _enemies;
        private HPController _playerHpController;

        private float _waitTime;
        private float _interval;

        private void Start()
        {
            _waitTime = _fadeTime / _loopCount;
            _interval = 255.0f / _loopCount;

            InstantiateEnemy(Stage.threeThree);
            StartCoroutine("FadeIn");
        }

        private void InstantiateEnemy(Stage stage)
        {
            _enemies = Instantiate(_enemyPrefabs[(int)stage]);            
        }

        public void Retry()
        {
            Debug.Log("clicked!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToTitle()
        {

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
            UnityEditor.EditorApplication.isPlaying = false;
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
