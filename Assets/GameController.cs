using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Score score;
    public GameObject gameOverCanva;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void MenuGame()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void GameOver()
    {
        gameOverCanva.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("oaisfhnaihuynf");
            score.UpdateScore();
        }
    }
}
