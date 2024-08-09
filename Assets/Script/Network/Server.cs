using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Zenject;

public class Server
{
    private Socket _server;
    private byte[] _buffer = new byte[512];
    private IPEndPoint _localEp;

    private bool _canConnect = true;
    private Socket _handler;

    private Dictionary<int, Socket> _allConnections = new();
    private int _id;

    private PointsManager _pointsManager;
    
    [Inject]
    private void Init(PointsManager pointsManager)
    {
        _pointsManager = pointsManager;
    }
    
    public void StartServer(int port)
    {
        _localEp = new IPEndPoint(GetLocalIPAddress(), port);
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        _server.Bind(_localEp);
        _server.Listen(100);

        HandleClients(_server);
        
    }
    public Socket GetSocket()
    {
        return _server;
    }
    public void SetConnect(bool b)
    {
        _canConnect = b;
    }
    public async void SendMessageToSocket(string msg)
    {
        Debug.Log(msg);
        byte[] smth = Encoding.ASCII.GetBytes(msg);
        //smth = AddByteToArray(smth, 0);
        await _handler.SendAsync(smth, SocketFlags.None);
    }
    public async void SendMessageToSocket(string msg, Socket handler)
    {
        byte[] smth = Encoding.ASCII.GetBytes(msg);
        //smth = AddByteToArray(smth, 0);
        await handler.SendAsync(smth, SocketFlags.None);
    }
    public void SendMessageToAllSockets(string msg)
    {
        foreach (KeyValuePair<int, Socket> connection in _allConnections)
        {
            SendMessageToSocket(msg, connection.Value);
        }
        Debug.Log(msg);
    }
    
    private void HandleResponse(string response)
    {
        switch (response)
        {
            case "click":
                _pointsManager.HandleClick();
                SendMessageToAllSockets("clickApproved");
                break;
        }
    }

    private IPAddress GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip;
            }
        }

        Debug.Log("No network adapters with an IPv4 address in the system!");
        return null;
    }

    private void AddConnectionToTheList()
    {
        _id += 1;
        _allConnections.Add(_id, _handler);
    }
    private async void HandleClients(Socket server)
    {
        while (_canConnect)
        {
            _handler = await server.AcceptAsync();
            StartReceivingMessages(_handler);
            AddConnectionToTheList();
            Debug.Log("Адрес подключенного клиента:" + _handler.RemoteEndPoint);
        }
    }

    private async void StartReceivingMessages(Socket socket)
    {
        int bytes;
        do
        {
            bytes = await socket.ReceiveAsync(_buffer, SocketFlags.None);
            string response = Encoding.ASCII.GetString(_buffer, 0, bytes);
            Debug.Log(response);
            HandleResponse(response);
        } while (bytes > 0);
    }
    public void ShutDownSocket()
    {
        _server.Shutdown(SocketShutdown.Both);
        _server.Close();
    }
    
}