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
    }

}
