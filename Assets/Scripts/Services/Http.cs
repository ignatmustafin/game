using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public static class Http
    {
        public static IEnumerator Post(string uri, Action<string>  onSuccessAction, object payload)
        {
            var webRequest = new UnityWebRequest(uri, "POST");
            
            string json = JsonUtility.ToJson(payload);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

            
            webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            
            webRequest.SetRequestHeader("Content-Type", "application/json; charset=utf-8");

            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.InProgress:
                    Debug.Log("Pending...");
                    break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    onSuccessAction(webRequest.downloadHandler.text);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            webRequest.Dispose();
        }
    }
}