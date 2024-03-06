using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowUDP : MonoBehaviour
{

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

    [SerializeField] private GameObject explosionImage;
    public bool explosion = false;

    void Start()
    {
        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
        }
    }

    void Update()
    {
        if (pcListener.accelerationSqrMagnitude > 30f)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        explosionImage.SetActive(true);
        explosion = true;
    }
}
