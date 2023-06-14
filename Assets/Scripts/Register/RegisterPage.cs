using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterPage : MonoBehaviour
{
    public void NavigateToLoginPage()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
