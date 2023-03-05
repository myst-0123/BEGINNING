using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Run : MonoBehaviour
{
    private Animator anim = null;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Run", true);
            transform.Translate(0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1,1, 1);
            anim.SetBool("Run", true);
            transform.Translate(-0.01f, 0, 0);
        }
        else
        {
            anim.SetBool("Run", false);
        }

    }
}
