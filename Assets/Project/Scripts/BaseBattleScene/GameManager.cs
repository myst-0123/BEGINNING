using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BattleScene
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject _enemyObject;
        [SerializeField] private Canvas _gameoverWindow;
        [SerializeField] private Image _image;
        [SerializeField] private Image _readyImage;
        [SerializeField] private Image _fightImage;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _loopCount;
        
        private float _velocity;
        private HPController _playerHpController;

        private void Start()
        {
            StartCoroutine("FadeIn");
            _playerHpController = _playerObject.GetComponent<HPController>();
        }

        private void Update()
        {
            if (_playerHpController.hp == 0)
            {
                _playerObject.GetComponent<PlayerController>().enabled = false;
                _gameoverWindow.gameObject.SetActive(true);
            }
        }
        public void Retry()
        {
            Debug.Log("clicked!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToTitle()
        {

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
            float waitTime = _fadeTime / _loopCount;
            float interval = 255.0f / _loopCount;
            float _velocity = 8.0f / _fadeTime;
            float velocityReductionRate = _velocity / (_fadeTime * _loopCount);
            _image.color = new Color(0, 0, 0, 1);

            for (float a = 255.0f; a >= 0; a -= interval)
            {
                yield return new WaitForSeconds(waitTime);

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
            _enemyObject.GetComponent<EnemyController>().enabled = true;
        }

        IEnumerator FadeOut()
        {
            float waitTime = _fadeTime / _loopCount;
            float interval = 255.0f / _loopCount;
            _image.color = new Color(0, 0, 0, 0);

            for (float a = 0.0f; a <= 255.0; a += interval)
            {
                yield return new WaitForSeconds(waitTime);

                Color newColor = _image.color;
                newColor.a = a / 255.0f;
                _image.color = newColor;
            }
        }
    }
}
