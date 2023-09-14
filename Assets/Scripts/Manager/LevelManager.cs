using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //�÷��̾��� ������
    Vector2 playerInitPosition;

   
    void Start()
    {
        playerInitPosition = FindObjectOfType<PlayerController>().transform.position; //����Ǹ� �÷��̾��� �������� �����Ѵ�

    }

    
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //�������� �ʱ�ȭ�Ѵ�
        //FindObjectOfType<PlayerController>().ResetPlayer();
        //FindObjectOfType<PlayerController>().transform.position = playerInitPosition;
        if(FindObjectOfType<PlayerController>().playerCurHp <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log(currentSceneName);
            SceneManager.LoadScene(currentSceneName);
        }

    }
}
