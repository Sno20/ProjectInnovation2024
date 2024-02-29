using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
  private Quaternion initialOrientation;
  private bool isCalibrated = false;

  [SerializeField] private Outline outline;
  private Color failColor = Color.red;
  private Color succesColor = Color.green;

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      Debug.Log("Gyroscope not supported"); //message if not supported
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
    // Capture the initial orientation as the inverse of the current attitude
    // This makes the current orientation the "neutral" or reference point
    initialOrientation = Quaternion.Inverse(Input.gyro.attitude);
    isCalibrated = true;
  }
}
