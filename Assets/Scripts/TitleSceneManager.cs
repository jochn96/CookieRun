using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("testscene"); //����ȭ������ �̵�
    }

    public void ExitGame()
    {
        Debug.Log("���� ����");
        Application.Quit(); //����
    }
}
