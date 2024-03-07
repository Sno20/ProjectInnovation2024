using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MechanicsManager : MonoBehaviour
{

    [SerializeField] private GameObject cameraSwitchingController;
    [SerializeField] private List<GameObject> mechanics;
    private int currentScreen;
    private bool interactedWithUV = false;
    private bool potion = false;

    //[SerializeField] private GameObject zoomin1;
    //[SerializeField] private GameObject zoomin2;

    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    [SerializeField] private GameObject potionToPour;


    private void Start()
    {
    }

    private void Update()
    {


        currentScreen = cameraSwitchingController.GetComponent<CameraSwitching>().currentScreen;

        //Debug.Log(currentScreen);

        //Debug.Log("hammer? " + mechanics[1].GetComponentInChildren<HammerUDP>().hammer);

        SwitchMechanics(currentScreen);

        /*if (mechanics[0].GetComponentInChildren<UVLightUDP>().discovered)
        {
            interactedWithUV = true;
        }*/

        

    }
    private void SwitchMechanics(int room)
    {
        ////////////
        if (room == 0)  //  room 1 UV
        {
            mechanics[0].SetActive(true);
        }
        else if (room != 0)
        {
            mechanics[0].SetActive(false);
        }
        ////////////
        if (room == 1)  //  room 2 throw
        {

            if (mechanics[3].GetComponent<MixUDP>().check) //activate throw
            {
                mechanics[4].SetActive(true);
            }
            if (mechanics[4].GetComponentInChildren<ThrowUDP>().explosion) // after thrown activate door
            {
                mechanics[5].SetActive(true);
            }


        }
        else if (room != 1)
        {
            mechanics[4].SetActive(false);
            mechanics[5].SetActive(false);
        }
        ////////////
        if (room == 2)  //  room 3  pour & mix potion
        {
            mechanics[2].SetActive(true);
            /*mechanics[3].SetActive(true);
            if (mechanics[1].GetComponentInChildren<HammerUDP>().potion) // if we took the potion
            {
                potionToPour.SetActive(true); // set to true the vessel wuth mix in it
            }*/
        }
        else if (room != 2)
        {
            mechanics[2].SetActive(false);
            mechanics[3].SetActive(false);
        }
        ////////////
        if (room == 3)  //  room 4 hammer and pick up potion
        {
            mechanics[1].SetActive(true);

        }
        else if (room != 3)
        {
            mechanics[1].SetActive(false);
        }
        ////////////
        if (room == 4 && room == 2)  //  room 2 zoom in
        {
            mechanics[6].SetActive(true);
            
            if (mechanics[1].GetComponentInChildren<HammerUDP>().potion) // if we took the potion
            {
                potionToPour.SetActive(true); // set to true the vessel wuth mix in it
            }
        }
        else if (room != 4)
        {
            mechanics[6].SetActive(false);
            
            /* mechanics[2].SetActive(false);
             mechanics[3].SetActive(false);*/
        }


    }

    public void InteractedWithUV()
    {
        interactedWithUV = true;
    }


    public void OpenPour()
    {
        if (mechanics[1].GetComponentInChildren<HammerUDP>().potion)
        {

            potionToPour.SetActive(!potionToPour.activeSelf);
        }
        else Debug.Log("no potion");
    }


}
