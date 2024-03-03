using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PourPotion : MonoBehaviour
{

    [SerializeField] private float minPhoneRotationX;
    [SerializeField] private float maxPhoneRotationX;
    [SerializeField] private float minPourAngleY;
    [SerializeField] private float maxPourAngleY;

    /*[SerializeField] private GameObject mixGameObject;
    [SerializeField] private GameObject thisParent;

    [SerializeField] private GameObject calibration;*/

    private Quaternion initialOrientation;
    private bool isCalibrated = false;

    private Quaternion targetRotation;
    [SerializeField] private float rotationSpeed = 2f;

    private void Start()
    {


        if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
            //CalibrateGyro();
        }
        else
        {
            Debug.Log("Gyroscope not supported"); //message if not supported
        }
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        //initialOrientation = calibration.GetComponent<Calibration>().initialOrientation;

        if (!isCalibrated)
        {
            return;
        }

        GyroCheck();
    }

    private void CalibrateGyro()
    {
        // Capture the initial orientation as the inverse of the current attitude
        // This makes the current orientation the "neutral" or reference point
        initialOrientation = Quaternion.Inverse(Input.gyro.attitude);
        isCalibrated = true;
    }

    public void Recalibrate()
    { //a method to recalibrate if needed
        CalibrateGyro();
    }

    private void GyroCheck()
    {
        // Apply the calibration offset to the current orientation
        Quaternion correctedOrientation = Input.gyro.attitude * initialOrientation;
        Vector3 gyroRotation = correctedOrientation.eulerAngles; // Use corrected orientation

        /*if (gyroRot.x > minPhoneRotationX || gyroRot.x < maxPhoneRotationX)
        { //check we are pouring within the phone upright position within the x range
            if (gyroRot.y < minPourAngleY || gyroRot.y > maxPourAngleY)
            { //check if we are pouring correct direction
              //Vector3 spriteRotation = new Vector3(0, 0, -gyroRot.y); //    IOS    due to weird coordinate space we set the spriteRotation's Z to -y
                
                Vector3 spriteRotation = new Vector3(0, 0, gyroRot.x);  // ANDROID

              //transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
                targetRotation = Quaternion.Euler(spriteRotation); //easing
            }
        }*/
        /*//   sprite x                          sprite x
        if (gyroRot.y > minPhoneRotationX || gyroRot.y < maxPhoneRotationX)
        { //check we are pouring within the phone upright position within the x range
            //  sprite z                     sprite z
            if (gyroRot.x < minPourAngleY || gyroRot.x > maxPourAngleY)
            { //check if we are pouring correct direction
              //Vector3 spriteRotation = new Vector3(0, 0, -gyroRot.y); //    IOS    due to weird coordinate space we set the spriteRotation's Z to -y

                Vector3 spriteRotation = new Vector3(0, 0, gyroRot.x);  // ANDROID

                //transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
                targetRotation = Quaternion.Euler(spriteRotation); //easing
            }
        }*/

        //Vector3 spriteRotation = new Vector3(-gyroRotation.y, -gyroRotation.z, gyroRotation.x);
        //Vector3 spriteRotation = new Vector3(0,0, gyroRotation.x);
        //Debug.Log("x y z :" + -gyroRotation.y + "; " + -gyroRotation.z + "; " + gyroRotation.x);
        //transform.rotation = Quaternion.Euler(spriteRotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.rotation = correctedOrientation;

    }

    private void Pour()
    {
        Debug.Log("Pouring now");

        //  mechanic finished
        //bool to mix 

        //mixGameObject.SetActive(true);

        //thisParent.SetActive(false);

    }
}
