using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public GameObject[] ui_Booms;

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

    public void BoomCheck(int boomCount)
    {

        for (int i = 0; i < ui_Booms.Length; i++)
        {
            if(i+1 <= boomCount)
                ui_Booms[i].SetActive(true);
            else
                ui_Booms[i].SetActive(false);
        }
        //if(boomCount == 0)
        //{
        //    ui_Booms[0].SetActive(false);
        //    ui_Booms[1].SetActive(false);
        //    ui_Booms[2].SetActive(false);
        //}
        //if (boomCount == 1)
        //{
        //    ui_Booms[0].SetActive(true);
        //    ui_Booms[1].SetActive(false);
        //    ui_Booms[2].SetActive(false);
        //}
        //if (boomCount == 2)
        //{
        //    ui_Booms[0].SetActive(true);
        //    ui_Booms[1].SetActive(true);
        //    ui_Booms[2].SetActive(false);
        //}
        //if (boomCount == 3)
        //{
        //    ui_Booms[0].SetActive(true);
        //    ui_Booms[1].SetActive(true);
        //    ui_Booms[2].SetActive(true);
        //}

    }
}
