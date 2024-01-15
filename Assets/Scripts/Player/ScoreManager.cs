using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour {
    [SerializeField] private int _score;

    [SerializeField] private string _dataKey, _maxScoreKey, _nameKey;

    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private DataBase _db;

    private void Start () {
        _scoreText.text = _score.ToString();

        gameObject.GetComponent<PlayerController>().EOnPlayerDead += SaveScore;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.TryGetComponent<Platform>(out Platform platform)) {
            _score++;
            _scoreText.text = _score.ToString();
        }
    }

    private void SaveScore() {
        if(CheckIsHighestScore()) {
            PlayerPrefs.SetInt(_maxScoreKey, _score);
            SaveScoreToDataBase();

            Debug.Log("Highest");
        }

        PlayerPrefs.SetInt(_dataKey, _score);
    }

    private bool CheckIsHighestScore() {
        int maxScore = PlayerPrefs.GetInt(_maxScoreKey);

        return maxScore < _score;
    }

    private void SaveScoreToDataBase() {
        string playerName = PlayerPrefs.GetString(_nameKey);

        PlayerScoreDTO playerScore = new PlayerScoreDTO(playerName, _score);

        _db.UpdateScore(playerScore);
    }
}
