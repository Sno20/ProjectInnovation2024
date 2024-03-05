using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsManager : MonoBehaviour
{

    [SerializeField] private GameObject cameraSwitchingController;
    [SerializeField] private List<GameObject> mechanics;
    private int currentScreen;
    private bool interactedWithUV = false;

    private void Start()
    {

    }

    private void Update()
    {
        currentScreen = cameraSwitchingController.GetComponent<CameraSwitching>().currentScreen;

        Debug.Log(currentScreen);

        SwitchMechanics(currentScreen);

    }
    private void SwitchMechanics(int room)
    {
        if (room == 0)
        {
            mechanics[0].SetActive(true);
        }
        else if (room != 0)
        {
            mechanics[0].SetActive(false);
        }
        if (room == 3)
        {
            mechanics[1].SetActive(true);

        }
        else if (room != 3)
        {
            mechanics[1].SetActive(false);
        }
    }

    public void InteractedWithUV()
    {
        interactedWithUV = true;
    }



}
