using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName; // 이동할 씬 이름 (Inspector에서 설정)

    void Start()
    {
        // 초기 설정 없음
    }

    void Update()
    {
        // 매 프레임 동작 없음
    }

    public void Load()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }
}