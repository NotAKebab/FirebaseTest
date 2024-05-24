using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendRequest : MonoBehaviour
{
    public TMP_Text _usernameText;
    public Button _acceptButton;
    public Button _rejectButton;

    private string _requesterId;
    private string _username;
    private DatabaseReference mDatabaseRef;
    private FirebaseAuth auth;

    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        _acceptButton.onClick.AddListener(AcceptFriendRequest);
        _rejectButton.onClick.AddListener(RejectFriendRequest);
    }

    public void SetRequestData(string requesterId, string username)
    {
        _requesterId = requesterId;
        _username = username;
        _usernameText.text = username;
    }

    private void AcceptFriendRequest()
    {
        string currentUserId = auth.CurrentUser.UserId;
        DatabaseReference userFriendsRef = mDatabaseRef.Child("friends").Child(currentUserId).Child(_requesterId);
        DatabaseReference requesterFriendsRef = mDatabaseRef.Child("friends").Child(_requesterId).Child(currentUserId);

        userFriendsRef.SetValueAsync(true).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                requesterFriendsRef.SetValueAsync(true).ContinueWithOnMainThread(task2 =>
                {
                    if (task2.IsCompleted)
                    {
                        mDatabaseRef.Child("friend_requests").Child(currentUserId).Child(_requesterId).RemoveValueAsync();
                        Destroy(gameObject); // Eliminar el objeto de la lista de solicitudes
                    }
                });
            }
        });
    }

    private void RejectFriendRequest()
    {
        string currentUserId = auth.CurrentUser.UserId;
        mDatabaseRef.Child("friend_requests").Child(currentUserId).Child(_requesterId).RemoveValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Destroy(gameObject); // Eliminar el objeto de la lista de solicitudes
            }
            else
            {
                Debug.LogError("RejectFriendRequest encountered an error: " + task.Exception);
            }
        });
    }
}
