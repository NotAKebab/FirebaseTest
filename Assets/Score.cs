using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _currentScore;
    [SerializeField] public TextMeshProUGUI _highestScore;
    public int _score;

    public void Start()
    {
        _currentScore.text = _score.ToString();
        _highestScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            PlayerPrefs.Save();
            _highestScore.text = _score.ToString();
        }
    }
    public void UpdateScore()
    {
        _score++;
        _currentScore.text = _score.ToString();
        UpdateHighScore();
    }
}
