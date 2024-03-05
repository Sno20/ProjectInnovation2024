using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using TMPro;

public class UDPSender : MonoBehaviour {
  //MOBILE

  private const int port = 8089;
  public string messageToSend = null;
  public TMP_InputField text;
  private UdpClient client = new UdpClient();
  private string targetIP = null;

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      //Debug.Log("Gyroscope not supported"); //message if not gyroscope supported
    }

  }

  public void SetIP(string ip) {
    targetIP = text.text;
    //Debug.Log("Target IP: " + targetIP);
  }

  void SendToTarget(byte[] packet) {
    //Debug.Log("Sending packet to target");
    client.Send(packet, packet.Length, targetIP, port);
  }

  void Update() {
    if (messageToSend != null) {
      if (targetIP != null) {
        byte[] bytes = Encoding.ASCII.GetBytes(messageToSend);
        SendToTarget(bytes);
      }
      messageToSend = null;
    }

    if (!string.IsNullOrEmpty(targetIP)) { // ensure IP is set before sendingk
      SendGyroData();
      SendAccelerationData();
    }
  }

  void SendGyroData() { //original way to send data
    if (Input.gyro.enabled) {
      Quaternion gyroAttitude = Input.gyro.attitude;
      string gyroData = "GYRO:" + QuaternionToString(gyroAttitude);
      byte[] bytes = Encoding.ASCII.GetBytes(gyroData);
      SendToTarget(bytes);
    }
  }

  string QuaternionToString(Quaternion q) {
    return $"{q.x},{q.y},{q.z},{q.w}";
  }
  void SendAccelerationData() {
    Vector3 acceleration = Input.acceleration;
    float sqrMagnitude = acceleration.sqrMagnitude; // Calculate squared magnitude
    string accelerationData = "ACCEL:" + sqrMagnitude.ToString();
    byte[] bytes = Encoding.ASCII.GetBytes(accelerationData);
    SendToTarget(bytes);
  }

}