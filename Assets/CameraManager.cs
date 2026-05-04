using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;   // 카메라 좌측 이동 제한
    public float rightLimit = 0.0f;  // 카메라 우측 이동 제한
    public float topLimit = 0.0f;    // 카메라 상단 이동 제한
    public float bottomLimit = 0.0f; // 카메라 하단 이동 제한

    public GameObject subScreen; // 패럴랙스(배경 원근감) 효과를 위한 오브젝트

    void Update()
    {
        // Player 오브젝트를 매 프레임 탐색
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z; // 카메라의 z값은 유지

            // X축 이동 범위 제한 (맵 밖으로 나가지 않도록)
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // Y축 이동 범위 제한
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }

            // 계산된 위치로 카메라 이동
            transform.position = new Vector3(x, y, z);

            // ===== 패럴랙스(Parallax) 효과 =====
            // 패럴랙스란:
            // 가까운 물체는 빠르게, 먼 배경은 느리게 움직이게 해서
            // 2D에서도 깊이감(원근감)을 느끼게 하는 기법

            if (subScreen != null)
            {
                // 배경의 기존 y, z는 유지 (세로 위치는 고정)
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;

                // 카메라 x 이동값의 절반만 적용
                // → 배경이 더 느리게 움직여 멀리 있는 것처럼 보임
                Vector3 v = new Vector3(x / 2.0f, y, z);

                subScreen.transform.position = v;
            }
        }
    }
}