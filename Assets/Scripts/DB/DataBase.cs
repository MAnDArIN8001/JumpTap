using UnityEngine;
using Firebase.Database;
using System;
using System.Threading.Tasks;

public class DataBase : MonoBehaviour {
    [SerializeField] private DatabaseReference _dbRef;

    public void Awake() {
        _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void UpdateScore(PlayerScoreDTO playerScore) {
        try {
            await _dbRef.Child($"Users/{playerScore.name}").SetValueAsync(playerScore.score);

            Debug.Log("Ok");
        } catch(Exception err) {
            Debug.LogError(err);
        }
    }

    public void RegisterUser(string name) { 
        try {
            _dbRef.Child($"Users/{name}").SetValueAsync(0);
        } catch(Exception err) { 
            Debug.LogError(err);
        }
    }

    public async Task<DataSnapshot> FetchUsers() {
        try {
            var response = await _dbRef.Child("Users").GetValueAsync();

            if (response == null)
                throw new Exception("response is null");

            return response;
        } catch (Exception err) { 
            Debug.LogError(err.Message);
        }

        return null;
    }
}
