using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserLabel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    void Reset()
    {
        _label = GetComponent<TMP_Text>();
    }
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    // Update is called once per frame
    void HandleAuthChange(object sender, EventArgs e)
    {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

        if (currentUser != null)
        {
            SetLabelUsername(currentUser.UserId);
        }
    }

    private void SetLabelUsername(string userId)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference($"users/{userId}/username")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    Debug.Log(snapshot);
                    _label.text = (string)snapshot.Value;
                }
            });

    }
}
