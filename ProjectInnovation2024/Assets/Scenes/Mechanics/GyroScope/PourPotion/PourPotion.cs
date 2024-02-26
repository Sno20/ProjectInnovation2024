using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PourPotion : MonoBehaviour {

  [SerializeField] private float minPhoneRotationX = 330f;
  [SerializeField] private float maxPhoneRotationX = 30f;
  [SerializeField] private float minPourAngleY = 0;
  [SerializeField] private float maxPourAngleY = 180;

  [SerializeField] private TextMeshProUGUI textBox; //for testing
  [SerializeField] private bool upright = true; //for testing

  private Quaternion initialOrientation;
  private bool isCalibrated = false;

  private Quaternion targetRotation;
  private float rotationSpeed = 2f;

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
      CalibrateGyro();
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

    
    if (gyroRot.x > minPhoneRotationX || gyroRot.x < maxPhoneRotationX) { //check we are pouring within the phone upright position within the x range
      if (gyroRot.y < minPourAngleY || gyroRot.y > maxPourAngleY) { //check if we are pouring correct direction
        spriteRotation = new Vector3(0, 0, -gyroRot.y); //due to weird coordinate space we set the spriteRotation's Z to -y
        transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
      }
     /* else if {
        targetRotation = Quaternion.Euler(spriteRotation); //easing
      }*/
    }

    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    textBox.text = gyroRot.ToString() + "\n" + minPhoneRotationX + "\n" + maxPhoneRotationX + "\n" + spriteRotation.ToString(); //show text
  }

  private void Pour() {
    textBox.text = "Good job!";
    Debug.Log("Pouring now");
  }
}
