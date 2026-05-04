using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static string gameState = "playing"; // 현재 게임 상태

    Rigidbody2D rbody; // 물리 컴포넌트
    float axisH = 0.0f; // 좌우 입력값
    public float speed = 3.0f; // 이동 속도
    public float jump = 9.0f; // 점프 힘
    public LayerMask groundLayer; // 바닥 판정용 레이어
    bool goJump = false; // 점프 입력 여부 (Update → FixedUpdate 전달)
    bool onGround = false; // 바닥에 닿았는지 여부

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
        gameState = "playing"; // 시작 상태 설정
        animator = GetComponent<Animator>(); // Animator 캐싱
        nowAnime = stopAnime; // 초기 애니메이션
        oldAnime = stopAnime; // 이전 애니메이션 초기화
    }

    void Update()
    {
        if (gameState != "playing") // 플레이 중이 아니면 입력 무시
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal"); // 좌우 입력

        if (axisH > 0.0f) // 오른쪽 이동 시 방향 전환
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f) // 왼쪽 이동 시 방향 전환
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump")) // 점프 입력 감지
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing") // 플레이 중이 아니면 물리 처리 중단
        {
            return;
        }

        // 바닥 체크 (아래 방향으로 짧은 레이캐스트)
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0) // 이동 처리
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }

        if (onGround && goJump) // 점프 처리
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false; // 점프 플래그 초기화
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

        if (nowAnime != oldAnime) // 애니메이션 변경 시에만 실행
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
        if (collision.gameObject.tag == "Goal") // 목표 지점 도달
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead") // 사망 영역 충돌
        {
            GameOver();
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime); // 클리어 애니메이션 실행
        gameState = "gameclear"; // 상태 변경
        GameStop(); // 이동 정지
    }

    public void GameOver()
    {
        animator.Play(deadAnime); // 사망 애니메이션 실행

        gameState = "gameover"; // 상태 변경
        GameStop(); // 이동 정지

        GetComponent<CapsuleCollider2D>().enabled = false; // 충돌 비활성화
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); // 위로 튕김 효과
    }

    void GameStop()
    {
        rbody.velocity = new Vector2(0, 0); // 속도 초기화
    }
}

/* enum 기반으로 변경 시 사용

public class playerController : MonoBehaviour
{
    public enum Eanime // 애니메이션 상태
    {
        PlayerStop,
        PlayerMove,
        PlayerJump,
        PlayerGoal,
        PlayerOver
    }

    public enum state // 게임 상태
    {
        Playing,
        GameOver,
        gameClear
    }

    public static state gameState = state.Playing; // 현재 게임 상태

    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    Animator animator;
    Eanime stopAnime = Eanime.PlayerStop;
    Eanime moveAnime = Eanime.PlayerMove;
    Eanime jumpAnime = Eanime.PlayerJump;
    Eanime goalAnime = Eanime.PlayerGoal;
    Eanime deadAnime = Eanime.PlayerOver;
    Eanime nowAnime;
    Eanime oldAnime;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        gameState = state.Playing;
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
    }

    void Update()
    {
        if (gameState != state.Playing)
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (gameState != state.Playing)
        {
            return;
        }

        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }

        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

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

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime.ToString());
        }
    }

    public void Jump()
    {
        goJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime.ToString());
        gameState = state.gameClear;
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(deadAnime.ToString());
        gameState = state.GameOver;
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    void GameStop()
    {
        rbody.velocity = new Vector2(0, 0);
    }
}
*/