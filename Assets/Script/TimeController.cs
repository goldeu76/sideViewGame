using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true; // true: 카운트다운 / false: 카운트업
    public float gameTime = 0f;     // 기준 시간 (초)
    public bool isTimeOver = false; // 시간 종료 여부
    public float displayTime = 0f;  // UI 표시용 시간

    float times = 0f; // 경과 시간

    void Start()
    {
        if (isCountDown)
        {
            displayTime = gameTime; // 시작 시 남은 시간 초기화
        }
    }

    void Update()
    {
        if (isTimeOver == false) // 시간이 끝나지 않았을 때만 진행
        {
            times += Time.deltaTime; // 경과 시간 누적

            if (isCountDown)
            {
                // 남은 시간 계산
                displayTime = gameTime - times;

                if (displayTime <= 0f) // 시간 종료
                {
                    displayTime = 0f;
                    isTimeOver = true;
                }
            }
            else
            {
                // 누적 시간 표시 (카운트업)
                displayTime = times;

                if (displayTime >= gameTime) // 목표 시간 도달
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }

            Debug.Log("Times : " + displayTime); // 디버그 출력
        }
    }
}