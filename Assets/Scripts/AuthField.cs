using Firebase.Database;
using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AuthField : MonoBehaviour {
    [SerializeField] private int _minNameLength;

    [SerializeField] private string _nameKey;

    [SerializeField] private TMP_Text _inputText;

    [SerializeField] private DataBase _db;

    public async void RegisterUser() {
        string name = _inputText.text;

        try {
            if (!CheckIsValidName(name))
                throw new Exception("Invalid name");

            if (!await CheckIsNameUnique(name))
                throw new Exception("Theres user with the same name");

            _db.RegisterUser(name);
            PlayerPrefs.SetString(_nameKey, name);
        } catch(Exception err) {
            Debug.Log(err);
        }
    }

    private bool CheckIsValidName(string name) {
        return name.Length >= _minNameLength && !String.IsNullOrEmpty(name.Trim());
    }

    private async Task<bool> CheckIsNameUnique(string name) {
        var data = await _db.FetchUsers();

        foreach(var user in data.Children) {
            if (user.Key == name)
                return false;
        }

        return true;
    }

}
