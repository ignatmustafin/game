using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public static class Http
    {
        public static IEnumerator Post(string uri, Action<string>  onSuccessAction, Dictionary<string, string> formData)
        {
            var webRequest = UnityWebRequest.Post(uri , formData);

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
        }
    }
}