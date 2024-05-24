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
    [SerializeField]
    private TMP_Text _notificationText;

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
    private void ShowNotification(string status)
    {
        string message = $"{_username} is now {status}";
        _notificationText.text = message;
        StartCoroutine(ClearNotificationAfterDelay(3f)); // Borrar notificación después de 3 segundos
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
                ShowNotification(status);
            }
            else
            {
                _statusText.text = "offline";
                ShowNotification("offline");
            }
        };
    }
    private IEnumerator ClearNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _notificationText.text = "";
    }
}
