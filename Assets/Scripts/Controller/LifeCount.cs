using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;
    public Image darkeningImage;
    public float fadeDuration = 1.0f; // Adjust the duration as needed.


    private float targetAlpha = 0f;


    //4 lives - 4 imgaes (0,1,2,3)
    //3 lives - 3 images (0,1,2,[3])
    //2 lives - 2 images (0,1,[2],[3])
    //1 life - 1 image (0,[1],[2],[3])
    //0 lives - 0 images ([0,1,2,3]) LOSE
    private void Start()
    {
        darkeningImage.color = new Color(0, 0, 0, 0); // Initialize as fully transparent.
    }

    public void GetLife()
    {

        if (livesRemaining <= 3)
        {
            livesRemaining++;
            lives[livesRemaining].enabled = true;
        }


    }
    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //Decrease the value of livesRemaining
        livesRemaining--;
        //Hide one of the life images
        lives[livesRemaining].enabled = false;

        //화면 어두워지게 만드는 코드 타격을 입었을 
        targetAlpha += 0.33f; // Increase the alpha value for each health box lost.
        targetAlpha = Mathf.Clamp01(targetAlpha); // Ensure it stays between 0 and 1.
        StartCoroutine(FadeScreen());

        //If we run out of lives we lose the game
        //if (livesRemaining == 0)
        //{
        //    FindObjectOfType<LevelManager >().Restart();
        //}
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //    LoseLife();
    }
    private IEnumerator FadeScreen()
    {
        Color startColor = darkeningImage.color;
        Color endColor = new Color(0, 0, 0, targetAlpha);
        Debug.Log("coler setting");
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            darkeningImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        darkeningImage.color = endColor;
    }
}


