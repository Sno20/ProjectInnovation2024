using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  private UDPListener udpListener; //cache component

  [SerializeField] Sprite purpleBeaker;
  private int swingCount = 0;
  private bool check = false;

  private void Start() {
    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }
  }

  private void Update() {

    if (udpListener.accelerationSqrMagnitude > 20f) {
      swingCount++;

    }
    if (!check && swingCount >= 20) {
      this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;
      check = true;
    }
  }
}
