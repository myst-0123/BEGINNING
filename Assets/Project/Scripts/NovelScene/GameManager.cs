using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelScene
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
