using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class UDPListener : MonoBehaviour
{

    // PC

    public string messageReceived;

    private const int port = 8089;
    private UdpClient udpClient;
    int testNum = 0;

    void Start()
    {
        udpClient = new UdpClient(port);
        udpClient.BeginReceive(ReceiveData, null);
        udpClient.BeginReceive(ReceiveNum, null);
    }

    void ReceiveData(IAsyncResult result)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.EndReceive(result, ref endPoint);
        messageReceived = Encoding.ASCII.GetString(receivedBytes);

        // Handle received message
        //if (!receivedMessage.Contains("Mobile")) Debug.Log(receivedMessage);
        //Debug.Log("Received message: " + receivedMessage);

        // Continue listening for messages
        udpClient.BeginReceive(ReceiveData, null);
    }

    private void Decode(string message)
    {
        if (message.Contains("gyro")){

            DecodeGyro(message);
        }
    }

    private Vector3 DecodeGyro(string gyroMessage)
    {
        Vector3 gyroData;

        string[] gyroDataParts = messageReceived.Split(',');    Debug.Log(gyroDataParts);


        if (gyroDataParts.Length >= 3)
        {
            gyroData.x = float.Parse(gyroDataParts[0]);
            gyroData.y = float.Parse(gyroDataParts[1]);
            gyroData.z = float.Parse(gyroDataParts[2]);

            //transform.rotation = Quaternion.Euler(gyroData);
        }
        else
        {
            gyroData.x = -1;
            gyroData.y = -1;
            gyroData.z = -1;
        }
        return gyroData;

    }



    void ReceiveNum(IAsyncResult result)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.EndReceive(result, ref endPoint);

        // Check if the received bytes can be converted to an integer
        if (receivedBytes.Length == 4)
        {
            // Convert received bytes to an integer
            int receivedNum = BitConverter.ToInt32(receivedBytes, 0);

            // Log the received number
            //Debug.Log("Received number: " + receivedNum);
        }
        else
        {
            // Log a message for invalid byte length
            //Debug.Log("Invalid byte length for integer");
        }

        // Continue listening for messages
        udpClient.BeginReceive(ReceiveNum, null);
    }

    void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}