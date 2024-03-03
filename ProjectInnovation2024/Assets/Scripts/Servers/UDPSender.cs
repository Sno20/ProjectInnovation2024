using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSender : MonoBehaviour

// PC
{
    private const int port = 7087;

    void Update()
    {
        SendBroadcast("Windows: Look how small you are!");
    }

    public static void SendBroadcast(string message)
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        // Define the message to send for discovery
        string discoveryMessage = message;

        // Convert message string to bytes
        byte[] bytes = Encoding.ASCII.GetBytes(discoveryMessage);

        // Broadcast the discovery message to the local network
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port);
        udpClient.Send(bytes, bytes.Length, endPoint);
        udpClient.Close();
    }
}