using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGyroToCapsule : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  //private Quaternion gyroData;

  private UDPListener udpListener; // Cache the component
  private Quaternion previousGyroData;

  void Start() {
    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }
  }

  void Update() {
    if (udpListener != null) {
      Quaternion currentGyroData = udpListener.gyroQuaternion;

      // Only update if there's a change to reduce unnecessary updates
      if (currentGyroData != previousGyroData) {
        Vector3 gyroDataVector = currentGyroData.eulerAngles;
        transform.rotation = Quaternion.Euler(gyroDataVector.x, gyroDataVector.y, gyroDataVector.z);
        previousGyroData = currentGyroData; // Update the previous data
      }
    }

    /*gyroData = senderListener.GetComponent<UDPListener>().gyroQuaternion;
    Vector3 gyroDataVector = gyroData.eulerAngles;
    transform.rotation = Quaternion.Euler(gyroDataVector.x, gyroDataVector.y, gyroDataVector.z);*/
  }
}
