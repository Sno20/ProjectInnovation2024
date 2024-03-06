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

  private PcListener pcListener; //cache component


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

    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
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

  public void CalibrateGyro() {
    Quaternion currentGyroData = pcListener.gyroQuaternion;
    initialOrientation = Quaternion.Inverse(currentGyroData);
    isCalibrated = true;
  }

    }

}
