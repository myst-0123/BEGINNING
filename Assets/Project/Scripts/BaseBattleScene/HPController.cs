using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] int maxHp;
    [SerializeField] Slider hpSlider;

    public int hp { get; private set; }

    void Start()
    {
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
}
