using UnityEngine;

namespace BattleScene
{
    public class PlayerController : MonoBehaviour
    {
        private Animator anim = null;

        float time = 0;

        [SerializeField] float shootInterval;
        [SerializeField] float speed;
        [SerializeField] GameObject bulletPrefab;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Run", true);
                if (transform.position.x < 3)
                {
                    transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Run", true);
                if (transform.position.x > -7.8)
                {
                    transform.position += new Vector3(-1 * speed, 0, 0) * Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("Run", true);
                if (transform.position.y < 3.7)
                {
                    transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("Run", true);
                if (transform.position.y > -3.7)
                {
                    transform.position += new Vector3(0, -1 * speed, 0) * Time.deltaTime;
                }
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Run", false);
            }
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            time += Time.deltaTime;
            if (time > shootInterval)
            {
                time = 0;
                GameObject bullet = Instantiate(bulletPrefab);

                bullet.transform.position = new Vector3(transform.position.x + 1.4f, transform.position.y + 0.3f, 0);
            }
        }
    }
}
