using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; // 플레이어 감지 거리
    public bool isDelete = false; // 충돌 시 사라질지 여부

    bool isFall = false; // 사라지는 상태 여부
    float fadeTime = 0.5f; // 페이드 시간

    Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); // Rigidbody 캐싱
        rbody.bodyType = RigidbodyType2D.Static; // 시작은 고정 상태
    }

    void Update()
    {
        // 플레이어 탐색
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어와 거리 계산
            float d = Vector2.Distance(transform.position, player.transform.position);

            // 감지 범위 안에 들어오면
            if (length >= d)
            {
                // 아직 Static 상태일 때만
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; // 물리 활성화 (낙하 시작)
                }
            }
        }

        // 사라지는 처리
        if (isFall)
        {
            fadeTime -= Time.deltaTime; // 시간 감소

            // 알파값 감소 (투명 처리)
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime;
            GetComponent<SpriteRenderer>().color = col;

            // 완전히 사라지면 제거
            if (fadeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 삭제 옵션이 켜져 있으면 충돌 시 페이드 시작
        if (isDelete)
        {
            isFall = true;
        }
    }
}