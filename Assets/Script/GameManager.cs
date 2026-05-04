using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainImage;      // 결과 이미지 (게임오버 / 클리어)
    [SerializeField] Sprite gameOverSpr;        // 게임오버 스프라이트
    [SerializeField] Sprite gameClearSpr;       // 클리어 스프라이트
    [SerializeField] GameObject panel;          // 결과 패널
    [SerializeField] GameObject restartButton;  // 재시작 버튼
    [SerializeField] GameObject nextButton;     // 다음 스테이지 버튼

    public GameObject timeBar;   // 타이머 UI 바
    public GameObject timeText;  // 타이머 텍스트
    TimeController timeCnt;     // 타이머 컨트롤러

    Image titleImage;

    public GameObject scoreText;     // 점수 텍스트
    public static int totalScore;    // 전체 누적 점수
    public int stageScore = 0;       // 현재 스테이지 점수

    void Start()
    {
        timeCnt = GetComponent<TimeController>(); // 타이머 연결

        // 타이머 사용 안 할 경우 UI 비활성화
        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0f)
            {
                timeBar.SetActive(false);
            }
        }

        Invoke("InactiveImage", 1f); // 시작 이미지 숨김
        panel.SetActive(false);      // 결과 UI 비활성화

        UpdateScore(); // 점수 초기화
    }

    void Update()
    {
        // ===== 게임 클리어 =====
        if (playerController.gameState == "gameclear")
        {
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;

                // 남은 시간 보너스 점수
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }

            // UI 처리
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            // 재시작 버튼 비활성화
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;

            playerController.gameState = "gameend";

            // 스테이지 점수 반영
            totalScore += stageScore;
            stageScore = 0;

            UpdateScore();
        }

        // ===== 게임 오버 =====
        else if (playerController.gameState == "gameover")
        {
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }

            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            // 다음 버튼 비활성화
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;

            playerController.gameState = "gameend";
        }

        // ===== 플레이 중 =====
        else if (playerController.gameState == "playing")
        {
            // Player 탐색
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerController playerCnt = player.GetComponent<playerController>();

            // ----- 타이머 처리 -----
            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0f)
                {
                    int time = (int)timeCnt.displayTime;

                    // UI 업데이트
                    timeText.GetComponent<Text>().text = time.ToString();

                    // 시간 종료 → 게임오버
                    if (time == 0)
                    {
                        playerCnt.GameOver();
                    }
                }
            }

            // ----- 점수 처리 -----
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score; // 스테이지 점수 누적
                playerCnt.score = 0;           // 중복 방지

                UpdateScore();
            }
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false); // 시작 이미지 비활성화
    }

    void UpdateScore()
    {
        int score = stageScore + totalScore; // 현재 총 점수
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}