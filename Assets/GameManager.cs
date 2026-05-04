using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainImage;      // 중앙 결과 이미지
    [SerializeField] Sprite gameOverSpr;        // 게임오버 이미지
    [SerializeField] Sprite gameClearSpr;       // 게임클리어 이미지
    [SerializeField] GameObject panel;          // 결과 UI 패널
    [SerializeField] GameObject restartButton;  // 재시작 버튼
    [SerializeField] GameObject nextButton;     // 다음 스테이지 버튼

    void Start()
    {
        Invoke("InactiveImage", 1f); // 1초 딜레이
        panel.SetActive(false);      // 패널 비활성화
    }

    void Update()
    {
        // 게임 클리어 처리
        if (playerController.gameState == "gameclear")
        {
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false; // 재시작 버튼 비활성화

            playerController.gameState = "gameend"; // 중복 실행 방지
        }
        // 게임 오버 처리
        else if (playerController.gameState == "gameover")
        {
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false; // 다음 버튼 비활성화

            playerController.gameState = "gameend";
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false); // 시작 이미지 숨김
    }
}