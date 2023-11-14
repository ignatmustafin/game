using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class HttpService
{
    public string baseUrl = "http://localhost:5157"; // Replace with your base URL
    public string testUrl = "http://localhost:5073"; // Replace with your base URL

    private UnityWebRequest _webRequest;

    public async Task<string> Get(string path)
    {
        string url = baseUrl + path;
        _webRequest = UnityWebRequest.Get(url);

        var operation = _webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (_webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("GET Error: " + _webRequest.error);
            return null;
        }
        else
        {
            string responseText = _webRequest.downloadHandler.text;
            return responseText;
        }
    }
    
    public async Task<string> TestGet(string path)
    {
        string url = testUrl + path;
        _webRequest = UnityWebRequest.Get(url);

        var operation = _webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (_webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("GET Error: " + _webRequest.error);
            return null;
        }
        else
        {
            string responseText = _webRequest.downloadHandler.text;
            return responseText;
        }
    }

    public async Task<string> Post(string path, object requestData)
    {
        string url = baseUrl + path;
        string jsonData = JsonUtility.ToJson(requestData); // Сериализуем объект в JSON

        UnityWebRequest _webRequest = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        _webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        _webRequest.downloadHandler = new DownloadHandlerBuffer();
        _webRequest.SetRequestHeader("Content-Type", "application/json");

        var operation = _webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (_webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST Error: " + _webRequest.error);
            return null;
        }
        else
        {
            string responseText = _webRequest.downloadHandler.text;
            return responseText;
        }
    }

    public void CancelRequest()
    {
        if (_webRequest != null)
        {
            _webRequest.Abort();
        }
    }
}