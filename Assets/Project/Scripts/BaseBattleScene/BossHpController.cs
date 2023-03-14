using UnityEngine;
using UnityEngine.UI;

public class BossHpController : MonoBehaviour
{
    [SerializeField] int maxHp;
    
    private Slider hpSlider;

    public int hp { get; private set; }

    void Start()
    {
        hpSlider = GameObject.Find("EnemyHPBar").GetComponent<Slider>();
        hpSlider.value = 1;
        hp = maxHp;
    }

    public void Attack(int atk)
    {
        hp -= atk;
        if (hp < 0)
        {
            hp = 0;
        }
        hpSlider.value = (float)hp / (float)maxHp;
    }

    public float HpPercent()
    {
        return 100 * hp / maxHp;
    }
}
