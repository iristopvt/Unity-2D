using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    GameObject player;
    Player playerController;
    // 체력바
    public float hp1; // 초록
    public float hp2; // 빨강
    //
    Animator animator;
    //
    bool onDead;
    bool isSpwan;
    // 점수
    int score;
    //
    float time;
    //
    Transform spwanMovePos;
    //
    float speed;

    // 총알 쏘는 위치
    public Transform LAttackPos;
    public Transform RAttackPos;
    //총알
    public GameObject bossBullet;
    //총알딜레이
    float fireDelay;

    // 애니메이션 상태 확인
    // -1 대기
    // 0  대기,이동
    // 1  L공격
    // 2  R공격
    // 3  Die

    int animNumber;

    // 피격관련
    public SpriteRenderer spriteRenderer;
    Color currentColor;

    private void Awake()
    {
        hp1 = 150.0f;
        hp2 = 150.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<Player>();
        spwanMovePos = GameObject.Find("BossSpwan").GetComponent<Transform>();

        animator = GetComponent<Animator>();

        onDead = false;
        isSpwan = true;

        score = 1000;

        speed = 10;

        animNumber = 0;

        currentColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpwan)
        {
            BossSpwan();
        }
        if(onDead)
        {
            time += Time.deltaTime;
        }
        if(time > 0.6f)
        {
            Destroy(gameObject);
        }

        if(player == null && GameManager.instance.lifeCount>=0)
        {
            PlayerFind();
        }

        FireBullet();
        AnimationSystem();
    }

    private void PlayerFind()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<Player>();
    }

    void FireBullet()
    {

        // 총알 발사 애니메이션

        if(hp1 > 0 && isSpwan == false)
        {
            fireDelay += Time.deltaTime;
            // 공격 딜레이가 2.0초 지나고 L공격 상태가 아니면
            if(fireDelay > 1.0f && animNumber != 1)
            {
                // L 공격
                animNumber = 1;
                fireDelay -= fireDelay;
            }
        }

        if(hp1 <= 0)
        {
            fireDelay += Time.deltaTime;
            // 공격 딜레이가 1.0초 지나고 R공격 상태가 아니면
            if(fireDelay > 1.0f && animNumber != 2)
            {
                // R공격
                animNumber = 2;
                fireDelay -= fireDelay;
            }
        }
    }

    void AnimationSystem()
    {

        // 대기 이름
        if(animNumber == 0)
        {
            StartCoroutine(Co_Idle());
        }
        if(animNumber == 1)
        {
            StartCoroutine(Co_LAttack());
        }
        if(animNumber == 2)
        {
            StartCoroutine (Co_RAttack());
        }
    }

    IEnumerator Co_Idle()
    {
        animNumber = -1;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator Co_LAttack()
    {
        animNumber = -1;
        animator.SetTrigger("LAttack");
        yield return new WaitForSeconds(0.6f);
        animNumber = 0;
    }

    IEnumerator Co_RAttack()
    {
        animNumber = -1;
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animNumber = 0;

    }

 

    void LAttack()
    {
        if (player == null)
            return;

        Instantiate(bossBullet, LAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }

    void RAttack()
    {
        if(player ==null) 
            return;

        Instantiate(bossBullet, RAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }
    void OnDead()
    {
        onDead = true;

        if(gameObject.tag != "Untaged")
        {
            UIManager.Instance.ScoreAdd(score);
            SoundManager.instance.enemyDeadSound.Play();
        }
        gameObject.tag = "Untagged";
    }

    private void BossSpwan()
    {
        transform.position = Vector3.MoveTowards(transform.position, spwanMovePos.position, Time.deltaTime * speed);
        if(transform.position ==spwanMovePos.position)
        {
            isSpwan = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if(hp1 > 0)
            {
                hp1 = hp1 - playerController.Damage;
            }
            else
            {
                hp2 = hp2 - playerController.Damage;
            }
            StartCoroutine(OnDamagedEffect());

        }

        if (collision.CompareTag("BoomMissile"))
        {
            if(hp1 > 0)
            {
                hp1 = hp1 - playerController.BoomDamage;
            }
            else
            {
                hp2 = hp2 - playerController.BoomDamage;
            }
            StartCoroutine(OnDamagedEffect());
        }
       
        if (hp2 <= 0)
        {
            animator.SetTrigger("Die");
            OnDead();
        }
    }

    IEnumerator OnDamagedEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = currentColor;
    }
}
