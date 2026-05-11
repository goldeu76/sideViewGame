using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName; //불러올 씬 이름

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
