using System;
using Models;
using Newtonsoft.Json.Linq;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class JoinLobby : MonoBehaviour
    {
        // [SerializeField] private GameObject lobbyIdInputField;
        // private readonly Http _http = new Http();
        //
        //
        // public async void OnJoinLobbyClicked()
        // {
        //     string lobbyId = lobbyIdInputField.GetComponent<TMP_InputField>().text;
        //
        //     SocketConnection.Instance.AddEventListener("all_users_joined_lobby",
        //         (data) =>
        //         {
        //             Debug.Log("Lobby completed event");
        //
        //             void SwitchSceneAction()
        //             {
        //                 SwitchScene("MainScene");
        //             }
        //
        //             SocketConnection.Instance.CompleteTaskInMainStream(SwitchSceneAction);
        //         });
        //     Lobby data = new Lobby {Id = lobbyId};
        //     
        //     string response = await _http.Post("/lobby/join-lobby", data);
        //     Debug.Log(response);
        // }
        //
        // private void SwitchScene(string sceneName)
        // {
        //     SceneManager.LoadScene(sceneName);
        // }
    }
}