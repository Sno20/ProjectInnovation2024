using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class PourPotion : MonoBehaviour {

  [SerializeField] private bool iphone = false; //has to be set before pouring is active

  private float minPhoneRotationX;
  private float maxPhoneRotationX;
  private float minPourAngleY;
  private float maxPourAngleY;

  private Quaternion initialOrientation;
  private bool isCalibrated = false;

  private Vector3 spriteRotation;
  private Quaternion targetRotation;
  [SerializeField] private float rotationSpeed = 2f;

  bool didPour = false;
  [SerializeField] TextMeshProUGUI textBox;


  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      Debug.Log("Gyroscope not supported"); //message if not supported
    }
    targetRotation = transform.rotation;
  }

  private void Update() {
    if (!isCalibrated) {
      return;
    }
    CheckPhone();
    GyroCheck();
  }

  public void CalibrateGyro() { //assign this to a button to calibrate before starting the minigame
    initialOrientation = Quaternion.Inverse(Input.gyro.attitude);
    isCalibrated = true;
  }


  private void GyroCheck() {
    Quaternion correctedOrientation = Input.gyro.attitude * initialOrientation; //apply the calibration offset to the current orientation
    Vector3 gyroRotation = correctedOrientation.eulerAngles; // Use corrected orientation

    if (iphone) {
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

    textBox.text = gyroRotation.ToString();
  }


  private void Pour() {
    if(!didPour) {
      Debug.Log("Pouring now");
    }
    didPour = true;
  }

  private void CheckPhone() {
    if (iphone) {
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
