using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Calibration : MonoBehaviour
{
    [SerializeField] private GameObject senderListner;
    //private Quaternion gyroData;
    private UDPListener udpListener; //cache component
    private GameManager gameManager;



    public Quaternion initialOrientation;
    public bool isCalibrated = false;
    public bool iphone = false;

    [SerializeField] private Outline outline;
    private Color failColor = Color.red;
    private Color succesColor = Color.green;

    private bool choseVersion = false;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
        }
        else
        {
            //Debug.Log("Gyroscope not supported"); //message if not supported
        }

        if (senderListner != null)
        {
            udpListener = senderListner.GetComponent<UDPListener>();
        }

    }

    private void Update()
    {
        if (!isCalibrated)
        {
            outline.effectColor = failColor;
            return;
        }
        else
        {
            outline.effectColor = succesColor;
        }
    }

    public void CalibrateGyro()
    {
        Quaternion currentGyroData = udpListener.gyroQuaternion;
        initialOrientation = Quaternion.Inverse(currentGyroData);
        isCalibrated = true;
    }

    public void IsIphone()
    {
        iphone = true;
        choseVersion = true;
    }

    public void IsAndroid()
    {
        iphone = false;
        choseVersion = true;
    }
    
    public void Proceed()
    {

        gameManager.initialOrientation = initialOrientation;
        gameManager.isIphone = iphone;
        gameManager.isCalibrated = isCalibrated;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
