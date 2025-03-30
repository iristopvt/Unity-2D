using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMissileController : MonoBehaviour
{
    public float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        speed = 35.0f;
        time = 0;

    }

    // Update is called once per frame
    void Update()
    {
        MoveBoom();
        DestroyBoom();

    }

    private void MoveBoom()
    {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
    }

    private void DestroyBoom()
    {
        time += Time.deltaTime;

        if(time > 3.0f)
        {
            Destroy(gameObject);
        }
    }
}
