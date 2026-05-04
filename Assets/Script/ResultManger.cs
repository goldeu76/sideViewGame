using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManger : MonoBehaviour
{
    public GameObject scoreText; // 결과 화면 점수 UI

    void Start()
    {
        // GameManager에 저장된 총 점수를 결과 화면에 출력
        scoreText.GetComponent<Text>().text = GameManager.totalScore.ToString();
    }

    void Update()
    {
        // 사용 안함
    }
}