using UnityEngine;
using TMPro;
using WebSocketSharp;

public class MobileClient : MonoBehaviour
{
    private WebSocket socket;
    public TMP_InputField codeInput;

    void Start()
    {
        // Set up the WebSocket server connection
        socket = new WebSocket("ws://congruous-remarkable-giraffe.glitch.me"); // Replace with your server address
        socket.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection opened");
        };

        socket.OnMessage += (sender, e) =>
        {
            // Handle incoming messages from the server
            Debug.Log($"Received message from server: {e.Data}");
            // Process the received message (e.g., handle 'MobileConnected')
            ProcessMessage(e.Data);
        };

        socket.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket connection closed");
        };

        // Start the WebSocket server connection
        socket.Connect();
    }

    void Update()
    {
        // Update logic if needed
    }

    void ProcessMessage(string message)
    {
        // Process the received message
        if (message == "MobileConnected")
        {
            // Log that the mobile is connected
            Debug.Log("Mobile connected");
            // Transition to the game scene when the mobile is connected
            UnityEngine.SceneManagement.SceneManager.LoadScene("YourGameScene");
        }
        else
        {
            // Handle other messages if needed
            Debug.Log($"Received message from server: {message}");
        }
    }

    void OnDestroy()
    {
        // Close the WebSocket connection when the script is destroyed
        if (socket != null && socket.IsAlive)
        {
            socket.Close();
        }
    }

    // Function to send the entered code to the server
    public void SendEnteredCode()
{
    // Check if the WebSocket connection is open
    if (socket != null && socket.IsAlive)
    {
        // Get the entered code from the TMP_InputField
        string enteredCode = codeInput.text;
        Debug.Log($"Sending Entered Code: {enteredCode}");

        // Send a message to the server indicating the entered code
        socket.Send($"EnteredCode:{enteredCode}");
    }
    else
    {
        Debug.LogError("WebSocket connection is not open.");
    }
}
}
