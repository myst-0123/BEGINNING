using System;
using System.Collections.Generic;
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

        private void Awake()
        {
            Instance = this;

            lineNumber = 0;
        }
    }
}
