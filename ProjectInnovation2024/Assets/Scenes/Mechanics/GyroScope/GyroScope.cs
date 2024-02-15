using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour {

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      Debug.Log("Gyroscope not supported"); //message if not supported
    }
  }

  private void Update() {
    SetZ();
    CheckZ();
  }

  private void SetZ() {
    Vector3 gyroRot2D = new Vector3(0, 0, Input.gyro.attitude.eulerAngles.z); //store gyroscope z in a vec3
    transform.rotation = Quaternion.Euler(gyroRot2D); //set obj rotation z to gyroscope rotation z) 
  }

  private void CheckZ() {
    if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 180) {
      Debug.Log("Pouring now");
    }
  }
}
