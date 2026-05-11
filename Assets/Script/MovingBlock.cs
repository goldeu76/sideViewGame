using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{

    public float moveX = 0f; // 시작 위치 기준으로 한 방향(+ 또는 -)으로 이동하는 "끝 지점 거리"
    public float moveY = 0f; // 시작 위치 기준으로 한 방향(+ 또는 -)으로 이동하는 "끝 지점 거리"
    public float times = 0f; // 한 방향 이동에 걸리는 시간 (왕복 아님)
    public float wait = 0f; // 방향 전환 후 대기 시간

    public bool isMoveWhenOn = false; // 플레이어가 올라타야만 움직일지 여부

    public bool isCanMove = true; // 블록 이동 가능 여부 (true면 이동 시작)

    float perDx; // 1프레임당 X 이동량 (FixedUpdate 기준)
    float perDy; // 1프레임당 Y 이동량 (FixedUpdate 기준)

    Vector3 defPos; // 시작 위치 (기준점)
    bool isReverse = false; // 이동 방향 반전 여부

    void Start()
    {
        // 시작 위치 저장 (이동 기준점)
        defPos = transform.position;

        // FixedUpdate 기준 시간 간격
        float timestep = Time.fixedDeltaTime;

        // 한 방향 이동 거리를 프레임 단위로 나눠서 이동 속도 계산
        perDx = moveX / ((1.0f / timestep) * times);
        perDy = moveY / ((1.0f / timestep) * times);

        // 플레이어가 올라타야만 움직이는 블록이면 처음에는 정지 상태
        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    private void FixedUpdate()
    {
        // 블록이 움직일 수 있는 상태일 때만 실행
        if (isCanMove)
        {
            float x = transform.position.x; // 현재 X 위치
            float y = transform.position.y; // 현재 Y 위치

            bool endX = false; // X 목표 도달 여부
            bool endY = false; // Y 목표 도달 여부
            //이동순서 : 정방향(else) > 역방향(if) > 정방향
            // ===== 역방향 이동 =====
            if (isReverse)
            {
                // 시작 위치 방향으로 되돌아가는 이동
                transform.Translate(new Vector3(-perDx, -perDy, defPos.z));

                // X: 시작 위치까지 돌아왔는지 체크
                if ((perDx >= 0f && x <= defPos.x) || (perDx < 0f && x >= defPos.x))
                {
                    endX = true;
                }

                // Y: 시작 위치까지 돌아왔는지 체크
                if ((perDy >= 0f && y <= defPos.y) || (perDy < 0f && y >= defPos.y))
                {
                    endY = true;
                }
            }
            // ===== 정방향 이동 =====
            else
            {
                Vector3 v = new Vector3(perDx, perDy, defPos.z);
                transform.Translate(v);

                // X: 목표 지점(moveX)까지 이동했는지 체크
                if ((perDx >= 0f && x >= defPos.x + moveX) || (perDx < 0f && x <= defPos.x + moveX))
                {
                    endX = true;
                }

                // Y: 목표 지점(moveY)까지 이동했는지 체크
                if ((perDy >= 0f && y >= defPos.y + moveY) || (perDy < 0f && y <= defPos.y + moveY))
                {
                    endY = true;
                }
            }

            // X, Y 둘 다 목표 지점 도달 시 방향 전환
            if (endX && endY)
            {
                // 역방향에서 시작 위치로 정확히 보정
                if (isReverse)
                {
                    transform.position = defPos;
                }

                // 이동 방향 반전
                isReverse = !isReverse;

                // 이동 잠시 정지
                isCanMove = false;

                // 플레이어 탑승형이 아니면 대기 후 자동 재시작
                if (isMoveWhenOn == false)
                {
                    Invoke("Move", wait);
                }
            }
        }
    }

    // 이동 재개
    public void Move()
    {
        isCanMove = true;
    }

    // 이동 정지
    public void Stop()
    {
        isCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어를 블록의 자식으로 설정 → 같이 이동하게 됨
            collision.transform.SetParent(transform);

            // 탑승 시 이동 시작 옵션이면 즉시 움직임 시작
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 블록에서 떨어지면 부모 해제 → 독립 이동 복구
            collision.transform.SetParent(null);
        }
    }
}