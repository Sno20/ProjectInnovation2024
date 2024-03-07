using System.Collections;
using UnityEngine;
using TMPro;

public class InGameDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Call this method to trigger the in-game dialogue
    public void TriggerDialogue()
    {
        gameObject.SetActive(true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // You can keep the input check here if you want to manually proceed with the dialogue
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // The dialogue is finished
            gameObject.SetActive(false);
            // Optionally, you can add logic to continue with the in-game scenario or perform other actions
        }
    }
}
