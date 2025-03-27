using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    float x;
    float y;

    public Vector3 limltMax;
    public Vector3 limltMin;
    Vector3 temp;

    public GameObject prefaBullet;
    float time;
    public float speed;

    float fireDealay;
    Animator animator;
    bool onDead;

    private void Start()
    {
        time = 0;
        fireDealay = 0; 
        speed = 10.0f;

        animator = GetComponent<Animator>();    
        onDead = false;
    }
  
    // Update is called once per frame 
    void Update()
    {
        // Time.deltaTime // 쓰면 pc 성능에 상관없이 이동 이 동일 

        Move();
        FireBullet();
        onDeadCheck();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(new Vector3(x, y, 0));

        if (transform.position.x > limltMax.x)
        {
            temp.x = limltMax.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y > limltMax.y)
        {
            temp.y = limltMax.y;
            temp.x = transform.position.x;
            transform.position = temp;
        }
        if (transform.position.x < limltMin.x)
        {
            temp.x = limltMin.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y < limltMin.y)
        {
            temp.y = limltMin.y;
            temp.x = transform.position.x;
            transform.position = temp;
        }

    }

    public void FireBullet()
    {
        fireDealay += Time.deltaTime;
        Debug.Log("Fire" + fireDealay);
        if(fireDealay > 0.3f)
        {
            Instantiate(prefaBullet, transform.position, Quaternion.identity);
            fireDealay -= 0.3f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limltMin, new Vector2(limltMax.x, limltMin.y));
        Gizmos.DrawLine(limltMin, new Vector2(limltMin.x, limltMax.y));
        Gizmos.DrawLine(limltMax, new Vector2(limltMax.x, limltMin.y));
        Gizmos.DrawLine(limltMax, new Vector2(limltMin.x, limltMax.y));


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead = true;
        }
    }

    private void onDeadCheck()
    {
        if(onDead)
        {
            time += Time.deltaTime;
        }
        if(time > 0.6f)
        {
            Destroy(gameObject);
        }
    }
}
