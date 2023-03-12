using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NovelScene
{
    public class NameTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _nameTextObject;

        public void SetName(string name)
        {
            _nameTextObject.text = name;
        }
    }
}
