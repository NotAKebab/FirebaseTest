using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UserResult : MonoBehaviour
{
    public TMP_Text _usernameText;
    public Button _sendRequestButton;

    private string _userId;
    private string _username;
    private DatabaseReference mDatabaseRef;
    private FirebaseAuth auth;

    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        _sendRequestButton.onClick.AddListener(SendFriendRequest);
    }

    public void SetUserData(string userId, string username)
    {
        _userId = userId;
        _username = username;
        _usernameText.text = username;
    }

    private void SendFriendRequest()
    {
        string currentUserId = auth.CurrentUser.UserId;
        mDatabaseRef.Child("friend_requests").Child(_userId).Child(currentUserId).SetValueAsync(true).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Friend request sent to: " + _username);
            }
            else
            {
                Debug.LogError("SendFriendRequest encountered an error: " + task.Exception);
            }
        });
    }
}
