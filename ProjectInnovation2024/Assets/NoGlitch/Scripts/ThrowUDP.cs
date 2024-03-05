using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

  [SerializeField] private GameObject explosionImage;

  void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
    }
  }

  void Update() {
    if (pcListener.accelerationSqrMagnitude > 30f) {
      Explosion();
    }
  }

  private void Explosion() {
    explosionImage.SetActive(true);
  }
}
