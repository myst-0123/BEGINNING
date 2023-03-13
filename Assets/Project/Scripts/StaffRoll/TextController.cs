using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _textScrollSpeed;
    [SerializeField] private float _limitPosition;

    private RectTransform _rectTransform;
    private DataManager _dataManager;

    void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        _text.text = _textAsset.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _dataManager.StaffRollEnd();
            _dataManager.Save();
            SceneManager.LoadScene("TitleScene");
        }
        else
        {
            if (_rectTransform.localPosition.y <= _limitPosition)
            {
                _rectTransform.localPosition += new Vector3(0, _textScrollSpeed, 0) * Time.deltaTime;
            }
        }
    }
}
