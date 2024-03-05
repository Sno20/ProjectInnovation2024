using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UVLightUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;

  private UDPListener udpListener; //cache component

  [SerializeField] private GameObject calibrationController;
  private Calibration calibration;

  [SerializeField] private float minPhoneRotationX = 180; //maximum up when faced away
  [SerializeField] private float maxPhoneRotationX = 320; //minimum left when faced away
  [SerializeField] private float minUVAngleY = 60; //minimum down when faced away
  [SerializeField] private float maxUVAngleY = 255; //maximum right when faced away

  [SerializeField] private GameObject textBox;
  [SerializeField] private bool iphone = false;

  private void Start() {
    if (senderListener != null) {
      udpListener = senderListener.GetComponent<UDPListener>();
    }

    if (calibrationController != null) {
      calibration = calibrationController.GetComponent<Calibration>();
    }
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
    Vector3 gyroRot = new Vector3(currentGyroData.eulerAngles.x, currentGyroData.eulerAngles.y, currentGyroData.eulerAngles.z); //set gyro input to vector3

    if (iphone && gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY) { //check gyro x and y rotation if screen is positioned away from player
      textBox.SetActive(true); //show secret text
    }
    else if (!iphone && gyroRot.x < minPhoneRotationX && gyroRot.x > maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY) {
      textBox.SetActive(true); //show secret text
    }
    else {
      textBox.SetActive(false); //hide secret text if wrong gyro phone rotation
    }
  }

  private void CheckPhone() {
    if (iphone) {
      minPhoneRotationX = 180;
      maxPhoneRotationX = 320;
      minUVAngleY = 60;
      maxUVAngleY = 255;
    }
    else {
      minPhoneRotationX = 340;
      maxPhoneRotationX = 60;
      minUVAngleY = 240;
      maxUVAngleY = 300;
    }
  }
}
