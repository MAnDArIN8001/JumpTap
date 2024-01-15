using TMPro;
using UnityEngine;

public class UIScoreMAnager : MonoBehaviour {
    [SerializeField] private string _dataKey, _maxScoreKey;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _maxScoreText;

    private void Start () {
        int result = PlayerPrefs.GetInt(_dataKey);
        int maxScore = PlayerPrefs.GetInt(_maxScoreKey);

        _scoreText.text = result.ToString();
        _maxScoreText.text = $"Record: {maxScore}";
    }
}
