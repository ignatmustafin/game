using System;
using System.Collections;
using Models;
using Newtonsoft.Json.Linq;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Scene = Services.Scene;

namespace Menu
{
    public class CreateLobby : MonoBehaviour
    {
        // [SerializeField] private GameObject createLobbyButton;
        // private readonly Http _http = new Http();
        //
        // private string joinLobbyUrl = "http://localhost:5157/lobby/join-lobby";
        // private string sceneLoadedUrl = "http://localhost:5157/lobby/scene-loaded";
        // private string endTurnUrl = "http://localhost:5157/lobby/end-turn";
        //
        // public async void OnCreateLobbyButtonClicked()
        // {
        //     string response = await _http.Get("/lobby/create-lobby");
        //     Lobby responseData = JsonUtility.FromJson<Lobby>(response);
        //     
        //     SetLobbyGuidToGameObject(responseData.Id);
        //     SocketConnection.Instance.AddEventListener("all_users_joined_lobby",
        //         (data) =>
        //         {
        //             Debug.Log("Lobby completed event");
        //
        //             void SwitchSceneAction()
        //             {
        //                 Scene.Instance.LoadScene(Scene.SceneName.MainScene);
        //             }
        //
        //             SocketConnection.Instance.CompleteTaskInMainStream(SwitchSceneAction);
        //         });
        // }
        //
        // private void SetLobbyGuidToGameObject(string lobbyId)
        // {
        //     GameObject lobbyIdInputField = GameObject.Find("lobbyIdInputField");
        //     lobbyIdInputField.GetComponent<TMP_InputField>().text = lobbyId;
        // }
        
        
        //
        // public void OnSceneLoadedClicked()
        // {
        //     string lobbyId = lobbyIdInputField.GetComponent<TMP_InputField>().text;
        //     // timerAction(55.ToString());
        // SocketConnection.Instance.AddEventListener("time_left",
        // (JToken[] data) =>
        // {
        //     Action timerAction = () =>
        //     {
        //         timer.GetComponent<TMP_Text>().SetText(data[0]["timer"].ToObject<int>().ToString());
        //     };
        //     SocketConnection.Instance.CompleteTaskInMainStream(timerAction);
        //     // Debug.Log($"TIMER: {data[0]["timer"].ToObject<int>()}");
        //     // SocketConnection.Instance.ChangeTimerValue(data[0]["timer"].ToObject<int>());
        // });
        //
        //     // SocketConnection.Instance.OnTimerLeft();
        //     SocketConnection.Instance.AddEventListener("start_battle",
        //         (data) => { Debug.Log($"BATTLE STARTED: {data}"); });
        //     StartCoroutine(SendLoadedSceneRequest(lobbyId));
        // }
        //
        // public void onTurnEnd()
        // {
        //     string lobbyId = lobbyIdInputField.GetComponent<TMP_InputField>().text;
        //     StartCoroutine(SendTurnEndedRequest(lobbyId));
        // }
        //
        // public void changeText(string timerLeft)
        // {
        //     Debug.Log($"Timer qwe");
        // }
        //
        // private IEnumerator SendCreateLobbyRequest()
        // {
        //     // Отправляем GET-запрос на сервер
        //     using (var request = UnityWebRequest.Get(_constants.GetFullUrl("/lobby/create-lobby")))
        //     {
        //         yield return request.SendWebRequest();
        //
        //         if (request.result != UnityWebRequest.Result.Success)
        //         {
        //             Debug.LogError("Error while sending request: " + request.error);
        //         }
        //         else
        //         {
        //             // Получаем ответ от сервера в виде строки и выводим его в консоль
        //             string response = request.downloadHandler.text;
        //             Debug.Log("LOBBY CREATED: " + response);
        //         }
        //     }
        // }
        //
        // private IEnumerator SendJoinLobbyRequest(string lobbyId)
        // {
        // LobbyIdData data = new LobbyIdData {LobbyId = lobbyId};
        //     // Debug.Log($"json data: {JsonUtility.ToJson(data)}");
        //
        //     var req = new UnityWebRequest(joinLobbyUrl, "POST");
        // string json = JsonUtility.ToJson(data);
        // byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        // req.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        // req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        // req.SetRequestHeader("Content-Type", "application/json");
        //
        //     yield return req.SendWebRequest();
        //
        //     if (req.result != UnityWebRequest.Result.Success)
        //     {
        //         Debug.LogError("Error while sending request: " + req.error);
        //     }
        //     else
        //     {
        //         string response = req.downloadHandler.text;
        //         Debug.Log("JOINED LOBBY: " + response);
        //     }
        //
        //     req.Dispose();
        // }
        //
        // private IEnumerator SendLoadedSceneRequest(string lobbyId)
        // {
        //     LobbyIdData data = new LobbyIdData {LobbyId = lobbyId};
        //     // Debug.Log($"json data: {JsonUtility.ToJson(data)}");
        //
        //     var req = new UnityWebRequest(sceneLoadedUrl, "POST");
        //     string json = JsonUtility.ToJson(data);
        //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //     req.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        //     req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        //     req.SetRequestHeader("Content-Type", "application/json");
        //
        //     yield return req.SendWebRequest();
        //
        //     if (req.result != UnityWebRequest.Result.Success)
        //     {
        //         Debug.LogError("Error while sending request: " + req.error);
        //     }
        //     else
        //     {
        //         string response = req.downloadHandler.text;
        //         Debug.Log("SCENE LOADED: " + response);
        //     }
        //
        //     req.Dispose();
        // }
        //
        // private IEnumerator SendTurnEndedRequest(string lobbyId)
        // {
        //     LobbyIdData data = new LobbyIdData {LobbyId = lobbyId};
        //
        //     var req = new UnityWebRequest(endTurnUrl, "POST");
        //     string json = JsonUtility.ToJson(data);
        //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //     req.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        //     req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        //     req.SetRequestHeader("Content-Type", "application/json");
        //
        //     yield return req.SendWebRequest();
        //
        //     if (req.result != UnityWebRequest.Result.Success)
        //     {
        //         Debug.LogError("Error while sending request: " + req.error);
        //     }
        //     else
        //     {
        //         string response = req.downloadHandler.text;
        //         // Debug.Log("Server response: " + response);
        //     }
        //
        //     req.Dispose();
        // }
        //
        // private void Awake()
        // {
        // }
        //
        // void Start()
        // {
        // }
    }

    public class LobbyIdData
    {
        public string LobbyId;
    }

}