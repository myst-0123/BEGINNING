using UnityEngine;

namespace BattleScene
{
    public class EnemyBulletController : MonoBehaviour
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

            if (transform.position.x < -11)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Instantiate(hitFxPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                HPController hpController = collision.gameObject.GetComponent<HPController>();
                hpController.Attack(attackDamage);
            }
        }
    }
}
