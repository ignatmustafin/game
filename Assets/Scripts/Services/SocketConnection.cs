using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EngineIOSharp.Common.Enum;
using Newtonsoft.Json.Linq;
using SocketIOSharp.Client;
using UnityEngine;

namespace Services
{
    public class SocketConnection : MonoBehaviour
    {
        private MainThreadTaskScheduler _taskScheduler;
        private static SocketConnection _instance;
        private SocketIOClient _socket;
    
        public static SocketConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SocketConnection>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<SocketConnection>();
                    }
                }

                return _instance;
            }
        }

        public SocketIOClient Socket
        {
            get { return _socket; }
        }

        private void Awake()
        {
            _taskScheduler = new MainThreadTaskScheduler();
            TaskScheduler.UnobservedTaskException += (sender, args) => args.SetObserved();
            Task.Factory.StartNew(() => { }, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
        
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        public void ConnectToSocketServer(string url)
        {
            if (_socket == null)
            {
                _socket = new SocketIOClient(new SocketIOClientOption(EngineIOScheme.http, "127.0.0.1", 3000));
                _socket.On("connect", () => { Debug.Log("Connected to server!"); });
                _socket.Connect();
                Debug.Log($"Socket connected {_socket}");
            }
        }

        public void AddEventListener(JToken eventName, Action<JToken[]> callback)
        {
            _socket.On(eventName, callback);
        }

        public void RemoveEventListener(JToken eventName, Action callback)
        {
            _socket.Off(eventName, callback);
        }

        private void OnApplicationQuit()
        {
            _socket.Close();
        }
    
        public void CompleteTaskInMainStream(Action funcToComplete)
        {
            Task.Factory.StartNew(funcToComplete, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
        }


        // Start is called before the first frame update
        void Start()
        {
            ConnectToSocketServer("http://127.0.0.1:3000");
        }

        // Update is called once per frame
        void Update()
        {
        }
    }

    public class MainThreadTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            UnityEngine.Debug.Log("Task Queued");
            UnityMainThreadDispatcher.Instance.Enqueue(() => TryExecuteTask(task));
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            UnityEngine.Debug.Log("Task Executed Inline");
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
    }

    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        private static UnityMainThreadDispatcher _instance;
        private static readonly object LockObject = new object();
        private readonly Queue<Action> _actionQueue = new Queue<Action>();

        public static UnityMainThreadDispatcher Instance
        {
            get
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("UnityMainThreadDispatcher");
                        _instance = obj.AddComponent<UnityMainThreadDispatcher>();
                        DontDestroyOnLoad(obj);
                    }

                    return _instance;
                }
            }
        }
    
        private void Update()
        {
            lock (LockObject)
            {
                while (_actionQueue.Count > 0)
                {
                    _actionQueue.Dequeue()?.Invoke();
                }
            }
        }
    
        public void Enqueue(Action action)
        {
            lock (LockObject)
            {
                _actionQueue.Enqueue(action);
            }
        }
    }
}