using UnityEngine;

namespace BattleScene
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] int attackDamage = 1;
        [SerializeField] GameObject hitFxPrefab;

        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            float directionRad = transform.eulerAngles.z * Mathf.Deg2Rad;
            _velocity = new Vector3(speed * Mathf.Cos(directionRad), speed * Mathf.Sin(directionRad), 0);
        }
        // Update is called once per frame
        void Update()
        {
            transform.position += _velocity * Time.deltaTime;

            if (transform.position.x > 11)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch(collision.gameObject.tag)
            {
                case "Boss":
                    Instantiate(hitFxPrefab, transform.position, transform.rotation);
                    Destroy(gameObject);
                    BossHpController hpController = collision.gameObject.GetComponent<BossHpController>();
                    hpController.Attack(attackDamage);
                    break;
            }
        }
    }
}
