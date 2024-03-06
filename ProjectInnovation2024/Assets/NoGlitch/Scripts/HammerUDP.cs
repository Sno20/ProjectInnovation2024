using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HammerUDP : MonoBehaviour
{

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component


    private int swingCount = 0;
    private bool check = false;

    [SerializeField] private GameObject hammerObj;
    [SerializeField] private GameObject textBox;
    public bool hammer = false;

    [SerializeField] private GameObject potionObj;
    public bool potion = false;

    [SerializeField] Sprite brokenGlass;

    void Start()
    {
        if (senderListener != null)
        {
            udpListener = senderListener.GetComponent<UDPListener>();
        }
    }

    void Update()
    {

        if (hammer && udpListener.accelerationSqrMagnitude > 30f)
        {
            swingCount += 1;
        }

        if (!check && swingCount >= 7)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = brokenGlass;
            check = true;
        }
    }

    public void PickedHammer()
    {
        hammer = true;
        hammerObj.SetActive(false);
        
    }

    public void PickedPotion()
    {
        if (check)
        {
            potion = true;
            potionObj.SetActive(false);
        }
    }

}
