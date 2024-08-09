using UnityEngine;
using Zenject;

public class ButtonClicker : MonoBehaviour
{
    public delegate void ButtonClicked();
    public event ButtonClicked Notify;
    
    private Server _server;
    private Client _client;
    private PointsManager _pointsManager;

    [Inject]
    private void Init(Server server, Client client, PointsManager pointsManager)
    {
        _server = server;
        _client = client;
        _pointsManager = pointsManager;
    }
    public void OnButtonClick()
    {
        if (_client.IsConnected())
        {
            Debug.Log("Client");
            Notify?.Invoke();
            _client.SendMessageToSocket("click"); 
        }
        else
        {
            Debug.Log("Server");
            _pointsManager.HandleClick();
            _server.SendMessageToAllSockets("clickFromServer"); 
        }
        
    }
}
