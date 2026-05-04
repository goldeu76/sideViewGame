using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static string gameState = "playing"; // 게임 상태

    Rigidbody2D rbody; // 물리 처리
    float axisH = 0.0f; // 좌우 입력값
    public float speed = 3.0f; // 이동 속도
    public float jump = 9.0f; // 점프 힘
    public LayerMask groundLayer; // 바닥 판정 레이어
    bool goJump = false; // 점프 요청 플래그
    bool onGround = false; // 바닥 여부

    Animator animator; // 애니메이터
    string stopAnime = "PlayerStop"; // 정지 애니메이션
    string moveAnime = "PlayerMove"; // 이동 애니메이션
    string jumpAnime = "PlayerJump"; // 점프 애니메이션
    string goalAnime = "PlayerGoal"; // 클리어 애니메이션
    string deadAnime = "PlayerOver"; // 사망 애니메이션
    string nowAnime; // 현재 애니메이션
    string oldAnime; // 이전 애니메이션

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); // Rigidbody 캐싱
        gameState = "playing"; // 시작 상태
        animator = GetComponent<Animator>(); // Animator 캐싱
        nowAnime = stopAnime;
        oldAnime = stopAnime;
    }

    void Update()
    {
        if (gameState != "playing") return; // 플레이 중이 아니면 입력 무시

        axisH = Input.GetAxisRaw("Horizontal"); // 좌우 입력

        if (axisH > 0.0f) // 오른쪽
            transform.localScale = new Vector2(1, 1);
        else if (axisH < 0.0f) // 왼쪽
            transform.localScale = new Vector2(-1, 1);

        if (Input.GetButtonDown("Jump")) // 점프 입력
            Jump();
    }

    private void FixedUpdate()
    {
        if (gameState != "playing") return; // 물리 처리 중단

        // 바닥 체크 (Linecast)
        onGround = Physics2D.Linecast(
            transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer
        );

        // 이동 처리
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }

        // 점프 처리
        if (onGround && goJump)
        {
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            goJump = false;
        }

        // 애니메이션 상태 결정
        if (onGround)
            nowAnime = (axisH == 0) ? stopAnime : moveAnime;
        else
            nowAnime = jumpAnime;

        // 변경 시에만 애니메이션 실행
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
        if (collision.gameObject.tag == "Goal")
            Goal();
        else if (collision.gameObject.tag == "Dead")
            GameOver();
    }

    public void Goal()
    {
        animator.Play(goalAnime); // 클리어 애니메이션
        gameState = "gameclear";
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(deadAnime); // 사망 애니메이션

        gameState = "gameover";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false; // 충돌 비활성화
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); // 튕김 효과
    }

    void GameStop()
    {
        rbody.velocity = new Vector2(0, 0); // 즉시 정지
    }
}

/* enum 기반 구조 (추후 최적화용)

public enum Eanime
{
    PlayerStop,
    PlayerMove,
    PlayerJump,
    PlayerGoal,
    PlayerOver
}

public enum state
{
    Playing,
    GameOver,
    gameClear
}

→ string 대신 enum 사용 시
- 오타 방지
- 성능 개선
- 코드 가독성 증가
*/