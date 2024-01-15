using UnityEngine;

public class Guide : MonoBehaviour {
    [SerializeField] private int _authorizationScenId;

    [SerializeField] private ScenManager _scenManager;

    private void Start() {
        string name = PlayerPrefs.GetString("Name");

        if (string.IsNullOrEmpty(name))
            _scenManager.ChangeScen(_authorizationScenId);

    }
}
