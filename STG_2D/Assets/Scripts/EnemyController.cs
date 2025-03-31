using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBullet;
    GameObject player;
    Player playerController;
    float fireDelay;

    Animator animator;
    bool onDead;
    float time;
    //이동
    float moveSpeed;
    Rigidbody2D rg2D;

    // 아이템
    public GameObject[] item;

    //hp
    int hp;

    // 태그 임시저장
    public string tagName;

    // 점수
    int score;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        onDead = false;
        time = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<Player>();
        //이동
        rg2D = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(5.0f, 7.0f);
        fireDelay = 2.5f;

        if (gameObject.CompareTag("ItemDropEnemy"))
            hp = 3;
        else
            hp = 1;

        tagName = gameObject.tag;

        score = 10;

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
            if (tagName =="ItemDropEnemy")
            {
                int temp = Random.Range(0, 2);
                Instantiate(item[temp], transform.position,Quaternion.identity);    
            }
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
            hp = hp - playerController.Damage;
        }

        if (collision.CompareTag("BoomMissile")) 
        {
            hp = hp - playerController.BoomDamage;
        }
        if (collision.CompareTag("BlockCollider"))
        {
            OnDisapper();
        }
        if (hp <= 0)
        {
            animator.SetInteger("State", 1);
            OnDead();
        }
    }

    private void OnDead()
    {
        onDead = true;
        if(gameObject.tag != "Untagged")
        {
            UIManager.Instance.ScoreAdd(score);
            SoundManager.instance.enemyDeadSound.Play();
        }
        gameObject.tag = "Untagged";

        // 스코어 증가 코드 작성 예정
    }

    private void OnDisapper()
    {
        Destroy(gameObject);
    }

}
