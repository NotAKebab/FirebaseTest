using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecoverAcc : MonoBehaviour
{
    [SerializeField]
    private Button recoverButton;


    [SerializeField]
    private TMP_InputField _email;

    private DatabaseReference mDatabaseRef;

    void Reset()
    {
        recoverButton = GetComponent<Button>();
        _email = GameObject.Find("EmailInputField").GetComponent<TMP_InputField>();

    }
    private void Start()
    {
        recoverButton.onClick.AddListener(HandleResetButtonClicked);
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void HandleResetButtonClicked()
    {
        Debug.Log("HandleResetButtonClicked");
        string email = _email.text;

        StartCoroutine(ResetPassword(email));
    }

    private IEnumerator ResetPassword(string email)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var resetTask = auth.SendPasswordResetEmailAsync(email);

        yield return new WaitUntil(() => resetTask.IsCompleted);

        if (resetTask.IsCanceled)
        {
            Debug.LogError("SendPasswordResetEmailAsync was canceled");
        }
        else if (resetTask.IsFaulted)
        {
            Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + resetTask.Exception);
        }
        else
        {
            Debug.Log("Correo de restablecimiento de contraseña enviado correctamente a: " + email);

            FirebaseUser user = auth.CurrentUser;
            if (user != null)
            {
                auth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                        return;
                    }

                    Debug.Log("Password reset email sent successfully.");
                });
            }
        }
    }
}