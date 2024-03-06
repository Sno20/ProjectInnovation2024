using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixUDP : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;
    private UDPListener udpListener; //cache component

    [SerializeField] private Sprite redBeaker;
    [SerializeField] private Sprite purpleBeaker;

    private bool finishedPouring = false;
    private int swingCount = 0;
    public bool check = false;

    private void Start()
    {
        if (senderListener != null)
        {
            udpListener = senderListener.GetComponent<UDPListener>();
        }
    }

    private void Update()
    {
        if (finishedPouring)
        {
            if (this.gameObject.GetComponent<PourPotionUDP>() != null)
            {
                if (udpListener.accelerationSqrMagnitude > 20f)
                {
                    swingCount++;

                }
                if (!check && swingCount >= 20)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;
                    check = true;
                }

            }
        }
    }

    public void FinishedPouring()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = redBeaker;
        finishedPouring = true;
    }
}
