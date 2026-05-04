using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static string gameState = "playing"; // 게임 상태 (playing / gameclear / gameover)
    Rigidbody2D rbody;

    float axisH = 0.0f; // 좌우 입력값
    public float speed = 3.0f; // 이동 속도
    public float jump = 9.0f; // 점프 힘
    public LayerMask groundLayer; // 바닥 판정 레이어

    bool goJump = false; // 점프 입력 상태
    bool onGround = false; // 바닥 접촉 여부

    Animator animator;

    string stopAnime = "PlayerStop";
    string moveAnime = "PlayerMove";
    string jumpAnime = "PlayerJump";
    string goalAnime = "PlayerGoal";
    string deadAnime = "PlayerOver";

    string nowAnime;
    string oldAnime;

    public int score = 0; // 아이템 점수

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";
        animator = GetComponent<Animator>();

        nowAnime = stopAnime;
        oldAnime = stopAnime;
    }

    void Update()
    {
        if (gameState != "playing")
        {
            return; // 게임 종료 시 입력 차단
        }

        axisH = Input.GetAxisRaw("Horizontal"); // 좌우 입력

        // 방향 전환
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // 점프 입력
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        // 바닥 체크 (라인캐스트)
        onGround = Physics2D.Linecast(
            transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer
        );

        // 이동
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }

        // 점프 처리
        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        // 애니메이션 상태 결정
        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }

        // 애니메이션 변경 시에만 실행
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    public void Jump()
    {
        goJump = true; // 점프 요청
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 목표 도착
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        // 사망 처리
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        // 아이템 획득
        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();

            score = item.value; // 점수 획득
            Destroy(collision.gameObject); // 아이템 삭제
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false; // 충돌 비활성화

        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); // 튕김 효과
    }

    void GameStop()
    {
        rbody.velocity = Vector2.zero; // 이동 정지
    }
}