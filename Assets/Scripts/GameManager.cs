using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image fadeImage;

   
    
    public Blade blade;
    public Spawner spawner;

    private int score = 0;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

        public void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach(Fruit fruit in fruits)
        {
            Destroy(fruit);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb);
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text=score.ToString();

    }

    public void Explode()
    {
        blade.enabled= false;
        spawner.enabled=false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t=Mathf.Clamp01(elapsed/duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale= 1f-t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1);

        NewGame();

        elapsed= 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }


    }
}
