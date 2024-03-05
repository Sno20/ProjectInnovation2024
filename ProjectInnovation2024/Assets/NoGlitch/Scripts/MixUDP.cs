using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

  [SerializeField] Sprite purpleBeaker;
  private int swingCount = 0;
  private bool check = false;

  private void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
    }
  }

  private void Update() {

    if (pcListener.accelerationSqrMagnitude > 20f) {
      swingCount++;

    }
    if (!check && swingCount >= 20) {
      this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;
      check = true;
    }
  }
}
