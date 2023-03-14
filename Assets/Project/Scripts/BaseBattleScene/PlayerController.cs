using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene
{
    public class PlayerController : MonoBehaviour
    {
        private Animator anim = null;

        float time = 0;
        float skillTime;
        float skillCoolDown = 0;
        float skillTimeE;
        float skillCoolDownE = 0;


        [SerializeField] float shootInterval;
        [SerializeField] float skillCollTime;
        [SerializeField] float skillCollTimeE;
        [SerializeField] float skillContinuationTime;
        [SerializeField] float speed;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] GameObject manager;
        [SerializeField] Slider skillSlider;
        [SerializeField] Slider skillSliderE;

        private GameManager gameManager;

        void Start()
        {
            anim = GetComponent<Animator>();
            gameManager = manager.GetComponent<GameManager>();

            skillTime = skillContinuationTime;
            skillTimeE = skillContinuationTime;
        }

        void Update()
        {
            SkillTimeUpdate();

            if (transform.gameObject.GetComponent<HPController>().hp == 0)
            {
                gameManager.StartCoroutine("BattleLose");
                this.enabled = false;
            }

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
            if (Input.GetKey(KeyCode.Q))
            {
                Skill();
            }
            if (Input.GetKey(KeyCode.E))
            {
                SkillE();
            }
        }

        void Shoot()
        {
            time += Time.deltaTime;
            if (time > shootInterval)
            {
                if (skillTime < skillContinuationTime)
                {
                    time = 0;
                    for (int i = -18; i <= 18; i += 12)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, i);
                        Instantiate(bulletPrefab, new Vector3(transform.position.x + 1.4f, transform.position.y + 0.3f, 0), rotation);
                    }
                }
                else
                {
                    time = 0;
                    Instantiate(bulletPrefab, new Vector3(transform.position.x + 1.4f, transform.position.y + 0.3f, 0), transform.rotation);
                }
            }
        }

        void SkillTimeUpdate()
        {
            //Skill Q
            if (skillTime < skillContinuationTime)
            {
                skillTime += Time.deltaTime;
                if (skillTime > skillContinuationTime)
                {
                    shootInterval -= 0.3f; 
                    skillTime = skillContinuationTime;
                }
                skillSlider.value = skillTime / skillContinuationTime;
            }
            else
            {
                skillCoolDown -= Time.deltaTime;
                if (skillCoolDown < 0)
                {
                    skillCoolDown = 0;
                }
                skillSlider.value = skillCoolDown / skillCollTime;
            }

            //Skill E
            if (skillTimeE < skillContinuationTime)
            {
                skillTimeE += Time.deltaTime;
                if (skillTimeE > skillContinuationTime)
                {
                    speed /= 2;
                    skillTimeE = skillContinuationTime;
                }
                skillSliderE.value = skillTimeE / skillContinuationTime;
            }
            else
            {
                skillCoolDownE -= Time.deltaTime;
                if (skillCoolDownE < 0)
                {
                    skillCoolDownE = 0;
                }
                skillSliderE.value = skillCoolDownE / skillCollTimeE;
            }
        }

        void Skill()
        {
            if (skillCoolDown == 0)
            {
                skillTime = 0;
                shootInterval += 0.3f;
                skillCoolDown = skillCollTime;
            }
        }

        void SkillE()
        {
            if (skillCoolDownE == 0)
            {
                skillTimeE = 0;
                speed *= 2;
                skillCoolDownE = skillCollTimeE;
            }
        }
    }
}
