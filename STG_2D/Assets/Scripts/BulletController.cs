using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        speed = 30.0f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
        DestroyBullet();
    }

    void FireBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        time += Time.deltaTime;
        if(time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemy"))
        {
            Destroy(gameObject);

        }

        if (collision.CompareTag("ItemDropEnemy"))
        {
            Destroy(gameObject);

        }
    }
}
