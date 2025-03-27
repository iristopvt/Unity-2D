using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBullet;
    GameObject player;
    float fireDelay;

    Animator animator;
    bool onDead;
    float time;
    //이동
    float moveSpeed;
    Rigidbody2D rg2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        onDead = false;
        time = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");

        //이동
        rg2D = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(5.0f, 7.0f);
        fireDelay = 2.5f;
        Move();

    }

    public void FireBullet()
    {
        if (player == null)
            return;

        fireDelay += Time.deltaTime;
        if(fireDelay > 3f)
        {
            Instantiate(enemyBullet,transform.position,Quaternion.identity);
            fireDelay -= 3f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(onDead)
        {
            time += Time.deltaTime;
        }
        if(time > 0.6f)
        {
            Destroy(gameObject);
        }
        FireBullet();
    }

    private void Move()
    {
        if(player == null)
            return;

        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.velocity = dir * moveSpeed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            animator.SetInteger("State", 1);
            OnDead();
           

        }
    }

    private void OnDead()
    {
        onDead = true;
        // 스코어 증가 코드 작성 예정
    }


}
