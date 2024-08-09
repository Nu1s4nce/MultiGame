using UnityEngine;
using System.Net;
using Zenject;


public class HostHandler : MonoBehaviour
{
    private const int PORT = 5000;
    //private List<string> activeConnections = new List<string>();
    //private Dictionary<int, string> idIpDictionary = new Dictionary<int, string>();

    //private int _currentId = 1;
    //private bool _isServer => _server != null;

    public MenuInput joinGameInput;

    private IPAddress ip;

    private Server _server;
    private Client _client;

    [Inject]
    private void Init(Server server, Client client)
    {
        _server = server;
        _client = client;
    }
    public string GetIpFromInput()
    {
        return joinGameInput.GetIpFromForm();
    }

    public void RunHostServer()
    {
        _server.StartServer(PORT);

        Debug.Log("Server has been started");
        //server.SendMessageToSocket("New connection!!!");
    }

    public void JoinServer()
    {
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse(GetIpFromInput()), PORT);
        _client.ConnectTo(endPoint);
        Debug.Log("Подключение к серваку установлено");
        _client.SendMessageToSocket("First message");
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        if (_client != null)
        {
            _client.ShutDownSocket();
        }
        else
        {
            _server.ShutDownSocket();
        }
    }
}