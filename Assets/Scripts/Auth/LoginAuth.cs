using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginAuth : MonoBehaviour
{
    private DatabaseReference mDatabaseRef;
    [SerializeField]
    private Button _loginButton;

    [SerializeField]
    private TMP_InputField _emailInputField;
    [SerializeField]
    private TMP_InputField _passwordInputField;

    private Coroutine _signupCoroutine;
    void Reset()
    {
        _loginButton = GetComponent<Button>();
        _emailInputField = GameObject.Find("EmailInputField").GetComponent<TMP_InputField>();
        _passwordInputField = GameObject.Find("PasswordInputField").GetComponent<TMP_InputField>();
    }
    void Start()
    {
        _loginButton.onClick.AddListener(HandleLoginButtonClicked);
    }

    private void HandleLoginButtonClicked()
    {
        var auth = FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(_emailInputField.text, _passwordInputField.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);
            SetUserStatus(result.User.UserId, "online");
            HandleUserDisconnection(result.User.UserId);
        });
    }

    private void SetUserStatus(string userId, string status)
    {
        mDatabaseRef.Child("users").Child(userId).Child("status").SetValueAsync(status);
    }

    private void HandleUserDisconnection(string userId)
    {
        DatabaseReference userStatusRef = mDatabaseRef.Child("users").Child(userId).Child("status");
        FirebaseDatabase.DefaultInstance.GetReference(".info/connected").ValueChanged += (s, e) =>
        {
            if (e.DatabaseError != null)
            {
                Debug.LogError(e.DatabaseError.Message);
                return;
            }

            if ((bool)e.Snapshot.Value)
            {
                userStatusRef.OnDisconnect().SetValue("offline");
                userStatusRef.SetValueAsync("online");
            }
        };
    }
}
