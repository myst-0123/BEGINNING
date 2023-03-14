using UnityEngine;
using UnityEngine.UI;

public class ZakoHpController : MonoBehaviour
{
    [SerializeField] int maxHp;

    private Slider hpSlider;

    public int hp { get; private set; }

    void Start()
    {
        hp = maxHp;
    }

    public void Attack(int atk)
    {
        hp -= atk;
        if (hp < 0)
        {
            hp = 0;
        }
    }
}
