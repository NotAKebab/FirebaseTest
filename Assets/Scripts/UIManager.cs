using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void OpenLoginPanel()
    {
        SceneManager.LoadScene("Login");

    }

    public void OpenRegistrationPanel()
    {
        SceneManager.LoadScene("Register");

    }
    public void OpenMainPanel()
    {
        SceneManager.LoadScene("Menu");

    }
    public void OpenRecoverPanel()
    {
        SceneManager.LoadScene("Recover");

    }
    public void OpenFriends()
    {
        SceneManager.LoadScene("Friends");

    }
    public void OpenGame()
    {
        SceneManager.LoadScene("coso");

    }
}
