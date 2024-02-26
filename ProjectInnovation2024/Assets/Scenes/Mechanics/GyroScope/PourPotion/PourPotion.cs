using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PourPotion : MonoBehaviour {

  [SerializeField] private float minPhoneRotationX = 340f;
  [SerializeField] private float maxPhoneRotationX = 20f;
  [SerializeField] private float minPourAngleX = 0;
  [SerializeField] private float maxPourAngleX = 0;

  [SerializeField] private TextMeshProUGUI textBox; //for testing
  [SerializeField] private bool upright = true; //for testing

  private Quaternion initialOrientation;
  private bool isCalibrated = false;

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
      CalibrateGyro();
    }
    else {
      Debug.Log("Gyroscope not supported"); //message if not supported
    }
  }

  private void Update() {
    if (!isCalibrated) {
      return;
    }

    GyroCheck();
  }

  private void CalibrateGyro() {
    // Capture the initial orientation as the inverse of the current attitude
    // This makes the current orientation the "neutral" or reference point
    initialOrientation = Quaternion.Inverse(Input.gyro.attitude);
    isCalibrated = true;
  }

  //Optionally, expose this method to allow recalibration from the UI
  public void Recalibrate() {
    CalibrateGyro();
  }

  Vector3 spriteRotation;
  private void GyroCheck() {
    // Apply the calibration offset to the current orientation
    Quaternion correctedOrientation = Input.gyro.attitude * initialOrientation;
    Vector3 gyroRot = correctedOrientation.eulerAngles; // Use corrected orientation

    
    if (gyroRot.x > minPhoneRotationX || gyroRot.x < maxPhoneRotationX) {
      Debug.Log("in range");
      spriteRotation = new Vector3(0, 0, -gyroRot.y);
      transform.rotation = Quaternion.Euler(spriteRotation);
    }
    textBox.text = gyroRot.ToString() + "\n" + minPhoneRotationX + "\n" + maxPhoneRotationX + "\n" + spriteRotation.ToString();
  }

  private void Pour() {
    textBox.text = "Good job!";
    Debug.Log("Pouring now");
  }
}
