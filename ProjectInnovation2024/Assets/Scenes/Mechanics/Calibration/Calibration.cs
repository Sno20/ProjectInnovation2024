using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
  [SerializeField] private GameObject senderListener;
  //private Quaternion gyroData;

  private UDPListener udpListener; //cache component

  public Quaternion initialOrientation;
  public bool isCalibrated = false;

  [SerializeField] private Outline outline;
  private Color failColor = Color.red;
  private Color succesColor = Color.green;


  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      //Debug.Log("Gyroscope not supported"); //message if not supported
    }

    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }
  }

  private void Update() {
    if (!isCalibrated) {
      outline.effectColor = failColor;
      return;
    }
    else {
      outline.effectColor = succesColor;
    }
  }

  public void CalibrateGyro() {
    Quaternion currentGyroData = udpListener.gyroQuaternion;
    initialOrientation = Quaternion.Inverse(currentGyroData);
    isCalibrated = true;
  }
}
