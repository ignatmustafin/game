using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginFormScript : MonoBehaviour
{
    public TextMeshProUGUI emailInputText;
    public TextMeshProUGUI passwordInputText;
    
    
    private void Submit()
    {
        if (string.IsNullOrEmpty(emailInputText.text) || string.IsNullOrEmpty(passwordInputText.text))
        {
            return;
        }
    
        var cb = new Action<string>((result) =>
        {
            //Player
            Debug.Log(result);
            SceneManager.LoadScene("MainScene");
        });

        string emailValue = emailInputText.text.Trim('\u200B');
        string passwordValue = passwordInputText.text.Trim('\u200B');

        var formData = new UserLoginCreds{email=emailValue, password=passwordValue};

        StartCoroutine(Services.Http.Post(Services.Constant.SignInUrl, cb, formData));

    }
   
}

class UserLoginCreds
{
    public string email;
    public string password;
}
