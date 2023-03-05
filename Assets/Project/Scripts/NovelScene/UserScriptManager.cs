using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NovelScene
{
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile;

        List<string> _sentences = new List<string>();

        private void Awake()
        {
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        public bool IsStatemant(string sentence)
        {
            if (sentence[0] == '&' || sentence[0] == '!')
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
            switch(words[0])
            {
                case "&background":
                    GameManager.Instance.imageManager.SetBackgroundImage(words[1]);
                    break;
                case "&character":
                    GameManager.Instance.imageManager.SetCharacterImage(int.Parse(words[1]), words[2]);
                    break;
                case "&goto":
                    GameManager.Instance.mainTextController.GoToLine(int.Parse(words[1]));
                    break;
                case "!":
                    GameManager.Instance.nameTextController.SetName(words[1]);
                    break;
            }
        }
    }

}
