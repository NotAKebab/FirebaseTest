using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Friend : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _usernameText;
    [SerializeField]
    private TMP_Text _statusText;

    private string _friendId;
    private string _username;
    private DatabaseReference mDatabaseRef;

    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        ObserveFriendStatus();
    }

    public void SetFriendData(string friendId, string username)
    {
        _friendId = friendId;
        _username = username;
        _usernameText.text = username;
    }

    private void ObserveFriendStatus()
    {
        mDatabaseRef.Child("users").Child(_friendId).Child("status").ValueChanged += (s, e) =>
        {
            if (e.DatabaseError != null)
            {
                Debug.LogError(e.DatabaseError.Message);
                return;
            }

            if (e.Snapshot.Exists)
            {
                string status = e.Snapshot.Value.ToString();
                _statusText.text = status;
            }
            else
            {
                _statusText.text = "offline";
            }
        };
    }
}
