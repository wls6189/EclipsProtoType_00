using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //플레이어의 포지션
    Vector2 playerInitPosition;

   
    void Start()
    {
        playerInitPosition = FindObjectOfType<PlayerController>().transform.position; //실행되면 플레이어의 포지션을 저장한다

    }

    
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //포지션을 초기화한다
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
