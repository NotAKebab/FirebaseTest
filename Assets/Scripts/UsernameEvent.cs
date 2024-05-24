using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameEvent : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _label;
    void Reset()
    {
        _label = GetComponent<TMP_Text>();
    }
    void Start()
    {
        FirebaseDatabase.DefaultInstance
       .GetReference($"users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/username")
       .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        _label.text = (string)args.Snapshot.Value;
    }
}
