using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] float speed = 0.05f;
        [SerializeField] GameObject hitFxPrefab;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(speed, 0, 0);
            if (transform.position.x > 11)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Instantiate(hitFxPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            HPController hpController = collision.gameObject.GetComponent<HPController>();
            hpController.Attack(1);
        }
    }
}
