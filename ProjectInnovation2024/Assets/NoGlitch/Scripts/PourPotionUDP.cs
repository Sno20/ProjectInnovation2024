using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourPotionUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;

  private UDPListener udpListener; //cache component
  private Quaternion previousGyroData;

  private float minPhoneRotationX;
  private float maxPhoneRotationX;
  private float minPourAngleY;
  private float maxPourAngleY;

  [SerializeField] private GameObject calibrationController;
  private Calibration calibration;

  private Vector3 spriteRotation;
  private Quaternion targetRotation;
  [SerializeField] private float rotationSpeed = 5f;

  bool didPour = false;

  void Start() {
    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }

    if (calibrationController != null) {
      calibration = calibrationController.GetComponent<Calibration>();
    }

    targetRotation = transform.rotation;
  }

  private void Update() {
    if (!calibration.isCalibrated) {
      return;
    }
    CheckPhone();
    GyroCheck();
  }
  private void GyroCheck() {
    Quaternion currentGyroData = udpListener.gyroQuaternion;
    if (currentGyroData != previousGyroData) { //only update when the value changes

      Quaternion correctedOrientation = currentGyroData * calibration.initialOrientation; //apply the calibration offset to the current orientation
      Vector3 gyroRotation = correctedOrientation.eulerAngles; // Use corrected orientation

      if (calibration.iphone) {
        if (gyroRotation.x > minPhoneRotationX || gyroRotation.x < maxPhoneRotationX) { //check we are pouring within the phone upright position within the x range
          Vector3 spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // IPHONE
          targetRotation = Quaternion.Euler(spriteRotation); //easing
          if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
          {
            Pour();
          }
          transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
        }
      }
      else {
        spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // ANDROID without x if, otherwise -y
        targetRotation = Quaternion.Euler(spriteRotation); //easing
        if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
        {
          Pour();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
      }

      previousGyroData = currentGyroData; //update previous data
    }
  }

  private void Pour() {
    if (!didPour) {
      //Debug.Log("Pouring now");
    }
    didPour = true;
  }

  private void CheckPhone() {
    if (calibration.iphone) {
      minPhoneRotationX = 330;
      maxPhoneRotationX = 30;
      minPourAngleY = 180;
      maxPourAngleY = 240;
    }
    else {
      minPhoneRotationX = 350;
      maxPhoneRotationX = 10;
      minPourAngleY = 120;
      maxPourAngleY = 180;
    }
  }
}

