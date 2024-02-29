using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class UDPSender : MonoBehaviour
{
    //      MOBILE


    private const int port = 8089;

    public string messageToSend = null;

    void Update()
    {
        //SendBroadcast("Mobile: I see you big boy!");
        //SendInt(69);

        if (messageToSend != null)
        {
            SendBroadcast(messageToSend);
            messageToSend = null;
        }

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

    public static void SendInt(int number)
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        // Convert integer value to a fixed-size byte array (4 bytes)
        byte[] bytes = BitConverter.GetBytes(number);

        // Broadcast the message to the local network
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port);
        udpClient.Send(bytes, bytes.Length, endPoint);
        udpClient.Close();
    }
}