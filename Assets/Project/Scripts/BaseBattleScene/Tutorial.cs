using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator WaitClick()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
                break;
            yield return null;
        }
    }
}
