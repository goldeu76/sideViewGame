using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName; // 인스펙터에서 로드할 씬 이름 설정

 
    public void Load()
    {
        SceneManager.LoadScene(sceneName); // 해당 이름의 씬으로 전환
    }
}
