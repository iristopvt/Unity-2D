using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public GameObject[] ui_Booms;

    // 점수
    public Text scoreText;
    public int score;
    //라이프
    public GameObject[] ui_Life;

   
    private void Awake()  // Awake는 start보다 먼저 실행 // 유니티이벤트함수 실행 치면 실행순서 나옴 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        score = 0;
    }

    // 폭탄 아이템 체크
    public void BoomCheck(int boomCount)
    {

        for (int i = 0; i < ui_Booms.Length; i++)
        {
            if(i+1 <= boomCount)
                ui_Booms[i].SetActive(true);
            else
                ui_Booms[i].SetActive(false);
        }
       

    }

    // 스코어 증가 
    public void ScoreAdd(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }

    // 라이프 체크
    public void LifeCheck(int lifeCount)
    {
        for(int i = 0; i < ui_Life.Length;i++)
        {
            if(i+1 <= lifeCount)
                ui_Life[i].SetActive(true);
            else
                ui_Life[i].SetActive(false);
        }
    }
}
