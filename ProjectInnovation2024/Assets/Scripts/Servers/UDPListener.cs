using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPListener : MonoBehaviour {

  //PC

  private UdpClient client;
  private IPEndPoint endPoint;

  public Quaternion gyroQuaternion;

  private void Start() {
    client = new UdpClient(8089);
    endPoint = new IPEndPoint(IPAddress.Any, 0);
  }

  private void Update() {
    /*ReceiveIPData();
    ReceiveGyroInput();*/

    ReceiveData();
  }

  void ReceiveData() {
    while (client.Available > 0) {
      byte[] inBytes = client.Receive(ref endPoint);
      string inString = Encoding.UTF8.GetString(inBytes);
      
      if (inString.StartsWith("IP:")) {
        // Handle IP setting, maybe remove the prefix and process the remaining string
        Debug.Log($"Received IP: {inString.Substring(3)} from {endPoint}");
      }
      else if (inString.StartsWith("GYRO:")) {
        // Handle Gyro input, remove the prefix before converting
        string gyroData = inString.Substring(5);
        gyroQuaternion = StringToQuaternion(gyroData);
        Debug.Log($"Received Gyro Data: {gyroData} from {endPoint}");
        // Use gyroQuaternion as needed
      }
    }
  }
/*  void ReceiveIPData() {
    if (client.Available > 0) {
      byte[] inBytes = client.Receive(ref endPoint); //receive from client
      string inString = Encoding.UTF8.GetString(inBytes); //convert bytes into strings
      Debug.Log($"Received:{inString} ({inBytes.Length} bytes) from {endPoint}"); //prints the string, number of bytes and from who we receive it
    }
  }

  void ReceiveGyroInput() {
    if (client.Available > 0) {
      byte[] inBytes = client.Receive(ref endPoint);
      string inString = Encoding.UTF8.GetString(inBytes);
      Debug.Log($"Received:{inString} ({inBytes.Length} bytes) from {endPoint}");
      Quaternion gyroQuaternion = StringToQuaternion(inString);
      // Use gyroQuaternion as needed
    }
  }*/

  Quaternion StringToQuaternion(string s) {
    string[] values = s.Split(',');
    if (values.Length == 4) {
      return new Quaternion(
          float.Parse(values[0]),
          float.Parse(values[1]),
          float.Parse(values[2]),
          float.Parse(values[3]));
    }
    return Quaternion.identity;
  }

  private void OnDestroy() {
    if (client != null)
      client.Close();
  }

}

  /*  public static void Main(string[] args) {
      UdpClient client = new UdpClient(8089); //set port to send data to

      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0); //know where a message comes from and values will be overwritten

      while (true) { //wait to receive messages
        byte[] inBytes = client.Receive(ref endPoint); //receive from client
        string inString = Encoding.UTF8.GetString(inBytes); //convert bytes into strings
        Debug.Log($"Received:{inString} ({inBytes.Length} bytes) from {endPoint}"); //prints the string, number of bytes and from who we receive it
      }
    }*/



  /*public string messageReceived;

  private const int port = 8089;
  private UdpClient client;
  int testNum = 0;

  void Start() {
    client = new UdpClient(port);
    client.BeginReceive(ReceiveData, null);
    client.BeginReceive(ReceiveNum, null);
  }

  void ReceiveData(IAsyncResult result) {
    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
    byte[] receivedBytes = client.EndReceive(result, ref endPoint);
    messageReceived = Encoding.ASCII.GetString(receivedBytes);

    Vector3 gyroData;

    string[] gyroDataParts = messageReceived.Split(','); Debug.Log(gyroDataParts);

    if (gyroDataParts.Length >= 3) {
      gyroData.x = float.Parse(gyroDataParts[0]);
      gyroData.y = float.Parse(gyroDataParts[1]);
      gyroData.z = float.Parse(gyroDataParts[2]);

      //transform.rotation = Quaternion.Euler(gyroData);
    }
    else {
      gyroData.x = -1;
      gyroData.y = -1;
      gyroData.z = -1;
    }

    Debug.Log("Received message: " + gyroData);

    // Handle received message
    //if (!receivedMessage.Contains("Mobile")) Debug.Log(receivedMessage);
    //Debug.Log("Received message: " + receivedMessage);

    // Continue listening for messages
    client.BeginReceive(ReceiveData, null);
  }

  private void Decode(string message) {
    if (message.Contains("gyro")) {

      //DecodeGyro(message);
    }
  }*/

  /*

  private Vector3 DecodeGyro(string gyroMessage) {
    Vector3 gyroData;

    string[] gyroDataParts = messageReceived.Split(','); Debug.Log(gyroDataParts);


    if (gyroDataParts.Length >= 3) {
      gyroData.x = float.Parse(gyroDataParts[0]);
      gyroData.y = float.Parse(gyroDataParts[1]);
      gyroData.z = float.Parse(gyroDataParts[2]);

      //transform.rotation = Quaternion.Euler(gyroData);
    }
    else {
      gyroData.x = -1;
      gyroData.y = -1;
      gyroData.z = -1;
    }
    return gyroData;*/

  /*  }

  }

    void ReceiveNum(IAsyncResult result) {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
      byte[] receivedBytes = client.EndReceive(result, ref endPoint);

      // Check if the received bytes can be converted to an integer
      if (receivedBytes.Length == 4) {
        // Convert received bytes to an integer
        int receivedNum = BitConverter.ToInt32(receivedBytes, 0);

        // Log the received number
        //Debug.Log("Received number: " + receivedNum);
      }
      else {
        // Log a message for invalid byte length
        //Debug.Log("Invalid byte length for integer");
      }

      // Continue listening for messages
      client.BeginReceive(ReceiveNum, null);
    }

    void OnDestroy() {
      if (client != null) {
        client.Close();
      }
    }*/
