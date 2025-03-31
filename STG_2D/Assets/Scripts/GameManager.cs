using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Player playerController;
    public Vector3 playerPos;
    public int lifeCount;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeCount = 2;
        UIManager.Instance.LifeCheck(lifeCount);
        CreatePlayer();
    }

    // 플레이어 생성
    public void CreatePlayer()
    {
        if(lifeCount >=0)
        {
            GameObject player = Instantiate(playerPrefab);
            float x = Random.Range(-9.0f, 9.0f);
            float y = -18.0f;
            playerPos = new Vector3(x, y, 0);
            player.transform.position = playerPos;
            playerController = player.GetComponent<Player>();
            UIManager.Instance.BoomCheck(playerController.Boom);

        }    
    }

    // 플레이어 라이프 감서
    public void PlayerLifeRemove()
    {
        lifeCount--;
    }
  
    // 플레이어 라이프에 따른 게임오버 확인

    public void GameOverCheck()
    {
        if(lifeCount < 0)
        {
            UIManager.Instance.GameOver();
        }
    }
}
