using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour {
    [SerializeField] private int _scenToLoad;
    [SerializeField] private int _gameOverScenId;

    [SerializeField] private string _playerTag;

    [SerializeField] private AudioSource _touchSound;
    
    [SerializeField] private Animator _animator;

    private void Start() { 
        _animator = gameObject.GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag(_playerTag);
        
        if (player)
            player.GetComponent<PlayerController>().EOnPlayerDead += GameLost;
    }

    private void LoadScen() {
        SceneManager.LoadScene(_scenToLoad);
    }

    public void ChangeScen(int scenToLoad) {
        _scenToLoad = scenToLoad;

        StartFade();
        _touchSound.Play();
    }

    private void GameLost() {
        StartFade();
        _scenToLoad = _gameOverScenId;
    }

    private void StartFade() {
        _animator.SetTrigger("Fade");
    }

}
