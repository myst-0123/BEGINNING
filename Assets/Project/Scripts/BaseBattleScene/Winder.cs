using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleScene
{
    public class Winder : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;

        private GameObject _playerObject; 
        void Start()
        {
            _playerObject = GameObject.Find("Player");
            StartCoroutine(MakeWinder());
        }

        float GetDirect()
        {
            float dx = _playerObject.transform.position.x - transform.position.x;
            float dy = _playerObject.transform.position.y - transform.position.y;
            return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        }

        IEnumerator MakeWinder()
        {
            while (true)
            {
                float dir = GetDirect();
                for (int j = 0; j < 60; j++)
                {
                    float dir2 = Mathf.Sin(2 * j * Mathf.Deg2Rad) * 15.0f;
                    for (int i = -2; i < 2; i++)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, dir + dir2 + i * 45);
                        GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                        bullet.GetComponent<EnemyBulletController>().SetBulletSpeed(8.0f);
                        bullet.GetComponent<EnemyBulletController>().SetBulletDamage(1);
                    }
                    yield return new WaitForSeconds(0.07f);
                }
            }
        }
    }
}
