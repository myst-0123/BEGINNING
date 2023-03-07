using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NovelScene;
using System;

namespace NovelScene
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;
        [SerializeField] GameObject _nextPageIcon;

        int _dispalyedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;

        // Start is called before the first frame update
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;

            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatemant(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
                GoToTheNextLine();
            }
            DisplayText();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.selectButtonManager.selectMode)
            {
                _mainTextObject.maxVisibleCharacters = _sentenceLength;
            }

            _time += Time.deltaTime;

            if (_time >= _feedTime)
            {
                _time -= _feedTime;
                if (!CanGoToTheNextLine())
                {
                    _dispalyedSentenceLength++;
                    _mainTextObject.maxVisibleCharacters = _dispalyedSentenceLength;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (CanGoToTheNextLine())
                {
                    GoToTheNextLine();
                    DisplayText();
                }
                else
                {
                    _dispalyedSentenceLength = _sentenceLength;
                }
            }

            _nextPageIcon.SetActive(CanGoToTheNextLine() || GameManager.Instance.selectButtonManager.selectMode);
        }

        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _sentenceLength = sentence.Length;
            if (_dispalyedSentenceLength <= _sentenceLength)
                return false;
            if (GameManager.Instance.selectButtonManager.selectMode)
                return false;

            return true;
        }

        public void GoToTheNextLine()
        {
            _dispalyedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatemant(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
                if (!GameManager.Instance.selectButtonManager.selectMode)
                    GoToTheNextLine();
                else
                    GameManager.Instance.lineNumber--;
            }
        }

        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _mainTextObject.text = sentence;
        }
    }

}
