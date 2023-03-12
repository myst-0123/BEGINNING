using BattleScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{    
    private GameManager _gameManager;

    public bool permittion { get; private set; }

    void Start()
    {
        permittion = false;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    public void SetPermittionTrue()
    {
        permittion = true;
    }
}
