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

    [SerializeField] TextMeshProUGUI textBox;

    Vector3 spriteRotation;


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
            spriteRotation = new Vector3(0, 0, gyroRotation.y);  // ANDROID without if, otherwise -y
            targetRotation = Quaternion.Euler(spriteRotation); //easing
        //if (gyroRotation.x > minPhoneRotationX || gyroRotation.x < maxPhoneRotationX) //check we are pouring within the phone upright position within the x range
        //{
            if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
            {
                Pour();
                //Vector3 spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // IPHONE
                //transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
            }
        //}
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
        //iphone: 330 / 30
        //android 350 / 10

        textBox.text = gyroRotation.ToString();
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
