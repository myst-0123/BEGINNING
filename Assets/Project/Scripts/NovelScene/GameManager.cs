using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NovelScene
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public MainTextController mainTextController;
        public NameTextController nameTextController;
        public ImageManager imageManager;
        public SelectButtonManager selectButtonManager;

        public int lineNumber;

        [SerializeField] Image _fadePanel;

        private float _fadeTime = 1.0f;
        private int _loopCount = 100;
        private float _waitTime;
        private float _interval;

        private void Awake()
        {
            Instance = this;

            _waitTime = _fadeTime / _loopCount;
            _interval = 255.0f / _loopCount;

            lineNumber = 0;

            StartCoroutine("FadeIn");
        }

        private IEnumerator FadeIn()
        {
            _fadePanel.color = new Color(0, 0, 0, 1);

            for (float a = 255.0f; a >= 0.0f; a -= _interval)
            {
                yield return new WaitForSeconds(_waitTime);

                Color newColor = _fadePanel.color;
                newColor.a = a / 255.0f;
                _fadePanel.color = newColor;
            }
        }
    }
}
