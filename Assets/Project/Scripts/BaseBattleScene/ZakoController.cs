using BattleScene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoController : MonoBehaviour
{
    [SerializeField] private EnemiesController enemiesController;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject destroyPrefab;
    [SerializeField] private int attackDamage;
    [SerializeField] private int bulletDamage;

    private ZakoHpController zakoHpController;
    private GameObject playerObject;
    private bool inWall = false;

    void Start()
    {
        zakoHpController = gameObject.GetComponent<ZakoHpController>();
        playerObject = GameObject.Find("Player");
        StartCoroutine(Attack());
    }

    private void Update()
    {
       
        if (zakoHpController.hp == 0)
        {
            Instantiate(destroyPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (enemiesController.permittion)
        {
            float direction = GetAim() * Mathf.Deg2Rad;
            transform.position += new Vector3(speed * Mathf.Cos(direction), speed * Mathf.Sin(direction), 0) * Time.deltaTime;
        }
    }

    private float GetAim()
    {
        float dx = playerObject.transform.position.x - transform.position.x;
        float dy = playerObject.transform.position.y - transform.position.y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HPController hpController = other.gameObject.GetComponent<HPController>();
            hpController.Attack(attackDamage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            inWall = true;
        }
    }

    private IEnumerator Attack()
    {
        while(true)
        {
            if (inWall)
            {
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0, 0, GetAim());
                GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
                bullet.gameObject.GetComponent<EnemyBulletController>().SetBulletDamage(bulletDamage);
            }
            yield return new WaitForSeconds(4.0f);
        }
    }
}
