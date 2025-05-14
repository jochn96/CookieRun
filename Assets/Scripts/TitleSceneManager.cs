using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("testscene"); //게임화면으로 이동
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit(); //종료
    }
}
