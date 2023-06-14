using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RegisterForm : MonoBehaviour
{
    public TextMeshProUGUI nameInputText;
    public TextMeshProUGUI emailInputText;
    public TextMeshProUGUI passwordInputText;


    public void Submit()
    {
        var cb = new Action<string>((result) =>
        {
            SceneManager.LoadScene("LoginScene");
        });
        

        string emailValue = emailInputText.text.Trim('\u200B');
        string passwordValue = passwordInputText.text.Trim('\u200B');
        string naemValue = nameInputText.text.Trim('\u200B');

        var formData = new UserRegisterCreds{email=emailValue, password=passwordValue, name=naemValue};

        
        StartCoroutine(Services.Http.Post(Services.Constant.SignUpUrl, cb, formData));
    }
}

class UserRegisterCreds
{
    public string email;
    public string password;
    public string name;
}
