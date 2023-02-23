using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelScene
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public MainTextController mainTextController;

        [System.NonSerialized] public int lineNumber;

        private void Awake()
        {
            Instance = this;

            lineNumber = 0;
        }
    }
}
