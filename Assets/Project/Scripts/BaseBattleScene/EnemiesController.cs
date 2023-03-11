using BattleScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private GameObject _manager;
    
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = _manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            _gameManager.StartCoroutine("BattleWin");
            this.enabled = false;
        }
    }
}
