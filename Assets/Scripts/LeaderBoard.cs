using UnityEngine;
using TMPro;
using Firebase.Database;
using System;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LeaderBoard : MonoBehaviour {
    [SerializeField] private bool _isActive;

    [SerializeField] private Animator _animator;

    [SerializeField] private TMP_Text _scoresText;

    [SerializeField] private DataBase _db;

    private void Start() {
        _db = gameObject.GetComponent<DataBase>();
        _animator = gameObject.GetComponent<Animator>();

        UpdateScore();

        Debug.Log(PlayerPrefs.GetString("Name"));
    }

    public void OpenMenu() {
        _isActive = true;
        _animator.SetBool("IsActive", _isActive);
    }

    public void CloseMenu() {
        _isActive = false;
        _animator.SetBool("IsActive", _isActive);
    }

    private async void UpdateScore() {
        try { 
            DataSnapshot data = await _db.FetchUsers();

            if (data == null)
                throw new Exception("no data");

            var list = data.Children.ToList();
            list.Sort((x, y) => {
                int xValue = int.Parse(x.Value.ToString());
                int yValue = int.Parse(y.Value.ToString());
                
                return yValue.CompareTo(xValue);
            });

            for (int i = 0; i < list.Count; i++) {
                var user = list[i];

                _scoresText.text += $"{i+1}.{user.Key}: {user.Value}\n";
            }

        } catch(Exception err) { 
            Debug.LogException(err);
        }
    }
}
