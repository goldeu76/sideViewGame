using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;   // 카메라 좌측 제한
    public float rightLimit = 0.0f;  // 카메라 우측 제한
    public float topLimit = 0.0f;    // 카메라 상단 제한
    public float bottomLimit = 0.0f; // 카메라 하단 제한

    public bool isForceScrollX = false; // X축 강제 스크롤 여부
    public float forceSpeedX = 0.5f;    // X축 강제 이동 속도
    public bool isForceScrollY = false; // Y축 강제 스크롤 여부
    public float forceSpeedY = 0.5f;    // Y축 강제 이동 속도

    public GameObject subScreen; // 패럴랙스 배경

    void Start()
    {

    }

    void Update()
    {
        // Player 탐색 (현재 매 프레임 검색)
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z; // 카메라 z 유지

            // 강제 스크롤 (플레이어 무시하고 일정 속도로 이동)
            if (isForceScrollX)
            {
                x = transform.position.x + (forceSpeedX * Time.deltaTime);
            }

            // X축 이동 제한
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // 강제 스크롤 Y
            if (isForceScrollY)
            {
                y = transform.position.y + (forceSpeedY * Time.deltaTime);
            }

            // Y축 이동 제한
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }

            // 카메라 위치 적용
            transform.position = new Vector3(x, y, z);

            // ===== 패럴랙스 (Parallax) =====
            // 카메라보다 느리게 움직이는 배경을 만들어 원근감 표현
            if (subScreen != null)
            {
                y = subScreen.transform.position.y; // 기존 y 유지
                z = subScreen.transform.position.z; // 기존 z 유지

                // 카메라 x의 절반만 적용 → 배경이 더 느리게 이동
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            }
        }
    }
}