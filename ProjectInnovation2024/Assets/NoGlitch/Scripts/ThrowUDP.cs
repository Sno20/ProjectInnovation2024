using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowUDP : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;
    private UDPListener udpListener; //cache component

    [SerializeField] private GameObject sideRoomButton;
    public bool explosion = false;

    void Start()
    {
        if (senderListener != null)
        {
            udpListener = senderListener.GetComponent<UDPListener>();
        }
    }

    void Update()
    {
        if (udpListener.accelerationSqrMagnitude > 30f)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
    sideRoomButton.SetActive(true);
        explosion = true;
    }
}
