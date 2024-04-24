using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    [SerializeField]
    private string _sceneToLoad = "GameMenu";

    void Start()
{
    if (FirebaseAuth.DefaultInstance.CurrentUser != null)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
    else
    {
        // Handle the case where the user is not authenticated (e.g., show a login screen)
    }
}

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }

    void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;
    }
}