using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class Scene: MonoBehaviour
    {
        public static Scene Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        public enum SceneName
        {
            MainScene,
            TestServer
        }
    
        public void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }

    
}