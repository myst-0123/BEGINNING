using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NovelScene
{
    public class UserScriptManager : MonoBehaviour
    {
        private TextAsset _textFile;
        private DataManager _dataManager;

        List<string> _sentences = new List<string>();

        private void Start()
        {
            _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
            _textFile = (TextAsset)Resources.Load("/texts/" + _dataManager.saveData.currentProgress);
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                if (line != "")
                    _sentences.Add(line);
            }
        }

        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        public bool IsStatemant(string sentence)
        {
            if (sentence[0] == '&' || sentence[0] == '!' || sentence[0] == '#')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(' ');

            var texts = new List<string>();
            var labels = new List<string>();

            switch(words[0])
            {
                case "&background":
                    GameManager.Instance.imageManager.SetBackgroundImage(words[1]);
                    break;
                case "&character":
                    GameManager.Instance.imageManager.SetCharacterImage(int.Parse(words[1]), words[2]);
                    break;
                case "&goto":
                    GoToLine(words[1]);
                    break;
                case "&select":
                    foreach (var i in words.Skip(1))
                    {
                        if (i[0] == '#')
                        {
                            labels.Add(i);
                        }
                        else
                        {
                            texts.Add(i);
                        }
                    }
                    GameManager.Instance.selectButtonManager.makeSelectButton(texts, labels);
                    break;
                case "&scene":
                    _dataManager.SetCurrentProgress(words[1]);
                    SceneManager.LoadScene("LoadScene");
                    break;
                case "&if":
                    if (_dataManager.saveData.flags[words[1]])
                    {
                        ;
                    }
                    else
                    {
                        GoToLine(words[2]);
                    }
                    break;
                case "!":
                    if (words.Length == 1)
                    {
                        GameManager.Instance.nameTextController.SetName("");
                    }
                    else
                    {
                        GameManager.Instance.nameTextController.SetName(words[1]);
                    }
                    break;
                default:
                    Debug.LogError("Invalid Command");
                    break;
            }
        }

        public void GoToLine(string label)
        {
            int index = _sentences.FindIndex(n => n == label);
            GameManager.Instance.lineNumber = index;
        }
    }

}
