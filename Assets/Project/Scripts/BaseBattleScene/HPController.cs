using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] int maxHp;
    [SerializeField] Slider hpSlider;

    int hp;

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
