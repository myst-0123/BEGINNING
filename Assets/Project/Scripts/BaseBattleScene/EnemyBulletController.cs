using UnityEngine;

namespace BattleScene
{
    public class EnemyBulletController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] int attackDamage = 1;
        [SerializeField] GameObject hitFxPrefab;

        public int direction = 0;

        private Vector3 _velocity = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            float directionRad = direction * Mathf.Deg2Rad;
            _velocity = new Vector3(speed * Mathf.Cos(directionRad), speed * Mathf.Sin(directionRad), 0);

            transform.position += _velocity * Time.deltaTime;

            if (transform.position.x < -11)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Enemy")
            {
                Instantiate(hitFxPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                HPController hpController = collision.gameObject.GetComponent<HPController>();
                hpController.Attack(attackDamage);
            }
        }
    }
}
