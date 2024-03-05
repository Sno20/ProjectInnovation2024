using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  private UDPListener udpListener; //cache component

  [SerializeField] private GameObject explosionImage;

  void Start() {
    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }
  }

  void Update() {
    if (udpListener.accelerationSqrMagnitude > 30f) {
      Explosion();
    }
  }

  private void Explosion() {
    explosionImage.SetActive(true);
  }
}
