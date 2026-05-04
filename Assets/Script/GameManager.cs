using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainImage;      // 결과 이미지 (클리어/오버)
    [SerializeField] Sprite gameOverSpr;        // 게임오버 이미지
    [SerializeField] Sprite gameClearSpr;       // 게임클리어 이미지
    [SerializeField] GameObject panel;          // 결과 UI 패널
    [SerializeField] GameObject restartButton;  // 재시작 버튼
    [SerializeField] GameObject nextButton;     // 다음 버튼

    public GameObject timeBar;  // 타이머 UI 바
    public GameObject timeText; // 타이머 텍스트

    TimeController timeCnt; // 시간 관리 스크립트 참조
    Image titleImage;

    void Start()
    {
        timeCnt = GetComponent<TimeController>(); // TimeController 가져오기

        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0f) // 타이머 사용 안 할 경우
            {
                timeBar.SetActive(false); // UI 비활성화
            }
        }

        Invoke("InactiveImage", 1f); // 시작 이미지 잠깐 후 숨김
        panel.SetActive(false);      // 결과 패널 비활성화
    }

    void Update()
    {
        // ===== 게임 클리어 =====
        if (playerController.gameState == "gameclear")
        {
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; // 타이머 정지
            }

            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false; // 재시작 버튼 비활성화

            playerController.gameState = "gameend"; // 중복 실행 방지
        }
        // ===== 게임 오버 =====
        else if (playerController.gameState == "gameover")
        {
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; // 타이머 정지
            }

            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false; // 다음 버튼 비활성화

            playerController.gameState = "gameend"; // 중복 실행 방지
        }
        // ===== 플레이 중 =====
        else if (playerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerController playerCnt = player.GetComponent<playerController>();

            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0f) // 타이머 사용하는 경우
                {
                    int time = (int)timeCnt.displayTime; // 표시용 시간

                    timeText.GetComponent<Text>().text = time.ToString(); // UI 업데이트

                    if (time == 0) // 시간 종료
                    {
                        playerCnt.GameOver(); // 강제 게임오버
                    }
                }
            }
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false); // 시작 이미지 숨김
    }
}