using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NovelScene
{
    public class SelectButtonManager : MonoBehaviour
    {
        [SerializeField] Button buttonPrefab;
        [SerializeField] GameObject selectButtons;

        public bool selectMode { get; private set; } = false;

        public void makeSelectButton(List<string> texts, List<string> labels)
        {
            RectTransform rectTransform;

            GameManager.Instance.imageManager.SetBackgroundA(0.7f);

            for (int i = 0; i < texts.Count; i++)
            {
                Button button = Instantiate(buttonPrefab, selectButtons.transform);
                button.name = "button" + i.ToString();
                button.GetComponentInChildren<TextMeshProUGUI>().text = texts[i];

                rectTransform = button.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(0, 200 - i*200, 0);
                
                SelectButtonScript buttonScript = button.GetComponent<SelectButtonScript>();
                buttonScript.setLabel(labels[i]);
            }
            
            selectMode = true;
        }

        public void deleteAllButton()
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }

            GameManager.Instance.imageManager.SetBackgroundA(1.0f);

            selectMode = false;
        }
    }
}
