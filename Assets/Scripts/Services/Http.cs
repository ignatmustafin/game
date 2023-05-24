using System;
using System.Collections;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class Http
    {
        private readonly Constant _constants = new Constant();


        public async Task<string> Get(string path)
        {
            UnityWebRequest req = new UnityWebRequest(_constants.GetFullUrl(path), "GET");
            req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            req.SendWebRequest();

            while (!req.isDone)
            {
                await Task.Yield();
            }

            
            if (req.result != UnityWebRequest.Result.Success)
            {
                throw new Exception("Error while sending request: " + req.error);
            }

            Debug.Log($"RESPONSE {req.downloadHandler}");
            string response = req.downloadHandler.text;
            return response;
        }
        
        public async Task<string> Post(string path, object data)
        {
            UnityWebRequest req = new UnityWebRequest(_constants.GetFullUrl(path), "POST");
            string json = JsonUtility.ToJson(data);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            req.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            req.SendWebRequest();

            while (!req.isDone)
            {
                await Task.Yield();
            }

            
            if (req.result != UnityWebRequest.Result.Success)
            {
                throw new Exception("Error while sending request: " + req.error);
            }

            string response = req.downloadHandler.text;
            return response;
        }
    }
}