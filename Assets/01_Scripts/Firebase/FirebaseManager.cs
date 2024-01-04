using DG.Tweening;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    private DatabaseReference _databaseReference;

    public event Action OnUserNameExists;
    public event Action OnUserNameNotExists;
    public event Action OnLongUserName;

    public event Action<List<DataSnapshot>> OnLeaderboardSetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform);
        }
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        //AddScore("gang", 1000);

        //SetLeaderboard();
    }

    public void AddScore(string userName, int score)
    {
        _databaseReference.Child("leaderboard").Child(userName).SetValueAsync(score);
    }

    private void AddUser(string userName)
    {
        _databaseReference.Child("leaderboard").Child(userName).SetValueAsync(0);
    }

    public void CheckUser(string userName)
    {
        _databaseReference.Child("leaderboard").Child(userName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                // 에러
                Debug.LogError("Error checking user: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if(userName.Length >= 11 || userName.Length <= 2)
                {
                    Debug.Log("Too long username");
                    OnLongUserName.Invoke();
                }
                else if (snapshot.Exists)
                {
                    // 사용자가 존재함
                    Debug.Log("User exists");
                    OnUserNameExists.Invoke();
                }
                else
                {
                    // 사용자가 존재하지 않음
                    Debug.Log("User does not exist");
                    OnUserNameNotExists.Invoke();
                    AddUser(userName);
                }
            }
        });
    }

    public void SetLeaderboard()
    {
        _databaseReference.Child("leaderboard").OrderByValue().GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                // Handle the error...
                Debug.LogError("Error reading leaderboard: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                var childrenList = snapshot.Children.ToList();

                OnLeaderboardSetting.Invoke(childrenList);

                Debug.Log("Leaderboard read successful");
            }
        });
    }
}