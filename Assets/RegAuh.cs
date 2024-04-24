using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class RegAuh : MonoBehaviour
{
    [SerializeField]
    private Button _registrationButton;

    private Coroutine _signupCoroutine;

    [SerializeField]
    private TMP_InputField _usernameInputField;

    private DatabaseReference mDatabaseRef;

    void Reset()
    {
        _registrationButton = GetComponent<Button>();
        _usernameInputField = GameObject.Find("UserInputField").GetComponent<TMP_InputField>();
    }
    private void Start()
    {
        _registrationButton.onClick.AddListener(HandleRegisterButtonClicked);
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void HandleRegisterButtonClicked()
    {
        Debug.Log("HandleRegisterButtonClicked");
        string email = GameObject.Find("EmailInputField").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("PasswordInputField").GetComponent<TMP_InputField>().text;
        _signupCoroutine = StartCoroutine(RegisterUser(email, password));


    }
    private IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.IsCanceled)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync fue cancelado");
        }
        else if (registerTask.IsFaulted)
        {
            Debug.LogError("Hubo un error con CreateUserWithEmailAndPasswordAsync " + registerTask.Exception);
        }
        else
        {
            AuthResult result = registerTask.Result;
            Debug.LogFormat("Firebase user creado correctamente: ´{0} ({1})", result.User.DisplayName, result.User.UserId);

            mDatabaseRef.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(_usernameInputField.text);
        }

    }
}
