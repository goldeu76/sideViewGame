using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName; // 이동할 씬 이름

    public void Load()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }
}