using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UserSearch : MonoBehaviour
{

    public TMP_InputField _searchInputField;

    public Button _searchButton;

    public GameObject _searchResultsPanel;

    public GameObject _userResultPrefab; // Prefab para mostrar un usuario en los resultados

    private DatabaseReference mDatabaseRef;
    private FirebaseAuth auth;

    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        _searchButton.onClick.AddListener(HandleSearchButtonClicked);
    }

    private void HandleSearchButtonClicked()
    {
        string searchQuery = _searchInputField.text;
        SearchUsers(searchQuery);
    }

    private void SearchUsers(string username)
    {
        mDatabaseRef.Child("users").OrderByChild("username").EqualTo(username).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.Exists)
                {
                    foreach (DataSnapshot snapshot in task.Result.Children)
                    {
                        string userId = snapshot.Key;
                        string userUsername = snapshot.Child("username").Value.ToString();

                        GameObject userResult = Instantiate(_userResultPrefab, _searchResultsPanel.transform);
                        userResult.GetComponent<UserResult>().SetUserData(userId, userUsername);
                    }
                }
                else
                {
                    Debug.Log("No users found with username: " + username);
                }
            }
            else
            {
                Debug.LogError("SearchUsers encountered an error: " + task.Exception);
            }
        });
    }
}