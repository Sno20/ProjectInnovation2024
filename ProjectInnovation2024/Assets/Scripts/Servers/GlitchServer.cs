using UnityEngine;
using TMPro;
using WebSocketSharp;

public class WindowsClient : MonoBehaviour
{
    private WebSocket socket;
    public TextMeshProUGUI gameCodeText;

    void Start()
    {
        // Set up the WebSocket server connection
        socket = new WebSocket("ws://congruous-remarkable-giraffe.glitch.me"); // Replace with your server address
        socket.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection opened");
            // Send a message to the server indicating that the Windows app is connected
            socket.Send("WindowsConnected");
        };

        socket.OnMessage += (sender, e) =>
        {
            // Handle incoming messages from the server
            Debug.Log($"Received message from server: {e.Data}");
            // Process the received message (e.g., extract the game code or handle 'MobileConnected')
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
        if (message.StartsWith("GameCode:"))
        {
            // Extract the game code from the message
            string gameCode = message.Substring("GameCode:".Length);
            // Log the received game code
            Debug.Log($"Received game code: {gameCode}");
            // Display the received game code in the TextMeshPro
            gameCodeText.text = $"Game Code: {gameCode}";
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
}
