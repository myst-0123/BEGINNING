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
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            if (transform.position.x > 11)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                Instantiate(hitFxPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                HPController hpController = collision.gameObject.GetComponent<HPController>();
                hpController.Attack(1);
            }
        }
    }
}
