using UnityEngine;

public class FxController : MonoBehaviour
{
    float time = 0;
    [SerializeField] private float _destroyTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > _destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
