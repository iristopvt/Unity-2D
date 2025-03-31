using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnConrtoller : MonoBehaviour
{
    // 생성위치
    public Transform[] enemySpawn;

    // 적 프리팹
    public GameObject[] enemyGameObjects;
    // 시간 재는 변수
    float time;
    // 적 생성 시간
    float respawnTime;
    //적 생성 숫자
    int enemyCount;
    //랜덤 숫자 변수를 저정하는 배열
    int[] randomCount;
    //웨이브 >> 추후사용
    int wave;
    // 플레이어 변수
    //GameObject player;
    // 보스 관련
    bool bossCreate;
    public GameObject bossGameObject;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;
        bossCreate = false; 
      //  player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        // 보스생성
        if (wave >= 5 && bossCreate == false)
            BossCreate();
    }

    void Timer()
    {
        time += Time.deltaTime;
        if(time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= time;
        }
    }

    void RandomPos()
    {

        // 랜덤 위치를 위한 숫자
        for(int i = 0; i <enemyCount; i++)
        {
            randomCount[i] = Random.Range(0, 9);
        }
    }

    void EnemyCreate()
    {
        //if (player == null)
        //    return;

        if(GameManager.instance.lifeCount < 0)
        {
            return;
        }
        for(int i = 0; i <enemyCount; i++)
        {
            //랜덤 적 선택
            int tmpCnt = Random.Range(0,enemyGameObjects.Length);

            // 생성
            GameObject tmp = GameObject.Instantiate(enemyGameObjects[tmpCnt]);

            // 위치
            tmp.transform.position = enemySpawn[randomCount[i]].position;

            // 동일 위치를 방지하기 위한 조금의 위치 값 수정
            float tmpX = tmp.transform.position.x;
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result,tmp.transform.position.y,transform.position.z);
        }
    }

    void BossCreate()
    {
        bossCreate = true;
        GameObject tmp = GameObject.Instantiate(bossGameObject);
        int randomCount = Random.Range(0, 9);
        tmp.transform.position = enemySpawn[randomCount].position;
        BossController bossController = tmp.GetComponent<BossController>(); 

        UIManager.Instance.isBossSpwan = true;
        UIManager.Instance.MaxHp1 = bossController.hp1;
        UIManager.Instance.MaxHp2 = bossController.hp2;
        UIManager.Instance.bossController = bossController;
    }
}
