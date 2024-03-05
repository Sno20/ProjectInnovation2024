using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerUDP : MonoBehaviour
{

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

  private int swingCount = 0;
  private bool check = false;

  [SerializeField] Sprite brokenGlass;

  void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
    }
  }

  void Update() {

    if (pcListener.accelerationSqrMagnitude > 30f) {
      swingCount += 1;
    }

    if (!check && swingCount >= 10) {
      this.gameObject.GetComponent<SpriteRenderer>().sprite = brokenGlass;
      check = true;
    }
  }
}
