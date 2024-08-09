using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Zenject;

public class Client
{
    private Socket _client;
    private byte[] _buffer = new byte[512];
    
    private PointsManager _pointsManager;
    
    [Inject]
    private void Init(PointsManager pointsManager)
    {
        _pointsManager = pointsManager;
    }
    
    public Client()
    {
        _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true );
        StartReceivingMessages();
    }

    public Socket GetSocket()
    {
        return _client;
    }

    public bool IsConnected()
    {
        return _client.Connected;
    }
    
    private void HandleResponse(string response)
    {
        switch (response)
        {
            case "clickApproved":
                _pointsManager.HandleClick();
                break;
            case "clickFromServer":
                _pointsManager.HandleClick();
                break;
        }
    }
    
    private async void StartReceivingMessages()
    {
        int bytes;
        do
        {
            bytes = await _client.ReceiveAsync(_buffer, SocketFlags.None);
            string response = Encoding.ASCII.GetString(_buffer, 0, bytes);
            Debug.Log(response);
            HandleResponse(response);
        } while (bytes > 0);
    }

    public void ConnectTo(EndPoint endPoint)
    {
        try
        {
            _client.ConnectAsync(endPoint);
        }
        catch(SocketException ex)
        {
            Debug.Log(ex.Message);
            _client.Close();
        }
        StartReceivingMessages();
    }
    
    public async void SendMessageToSocket(string msg)
    {
        byte[] smth = Encoding.ASCII.GetBytes(msg);
        await _client.SendAsync(smth, SocketFlags.None);
        Debug.Log(msg);
    }

    public void ShutDownSocket()
    {
        _client.Shutdown(SocketShutdown.Both);
        _client.Close();
    }
}
