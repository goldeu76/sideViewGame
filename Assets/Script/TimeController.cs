using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true; // true: 카운트다운 / false: 카운트업
    public float gameTime = 0f; // 기준 시간 (에디터 입력값)
    public bool isTimeOver = false; // 시간 종료 여부
    public float displayTime = 0f; // UI 표시용 시간

    float times = 0f; // 실제 경과 시간

    void Start()
    {
        // 카운트다운일 경우 시작값을 전체 시간으로 설정
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    void Update()
    {
        // 시간이 끝나지 않았을 때만 동작
        if (isTimeOver == false)
        {
            times += Time.deltaTime; // 시간 누적

            if (isCountDown)
            {
                // 남은 시간 계산
                displayTime = gameTime - times;

                // 0 이하이면 종료 처리
                if (displayTime <= 0f)
                {
                    displayTime = 0f;
                    isTimeOver = true;
                }
            }
            else
            {
                // 카운트업 (경과 시간 표시)
                displayTime = times;

                // 목표 시간 도달 시 종료
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }

            Debug.Log("Times : " + displayTime); // 디버그 출력
        }
    }
}