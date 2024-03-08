using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwitching : MonoBehaviour
{

    [SerializeField] private List<GameObject> screens = new List<GameObject>(); //setup array list

    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;
    [SerializeField] private GameObject downArrowButton;
    [SerializeField] private GameObject breweryButton;
    [SerializeField] private GameObject sideRoomButton;

    [SerializeField] private GameObject breweryZoomed;
    [SerializeField] private GameObject sideRoomWithKey;

    public int currentScreen = 0;
    [SerializeField] private Image blackBackground;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0f);
    }

    void Update()
    {

    }

    private IEnumerator FadeToBlackAndSwitch(int nextScreen)
    {
        yield return StartCoroutine(FadeToBlack()); // Fade to black
        ActivateScreen(nextScreen); // Activate the next screen
        yield return StartCoroutine(FadeFromBlack()); // Fade back in
    }

    private IEnumerator FadeToBlack()
    {
        float currentTime = 0f;
        while (currentTime <= fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeFromBlack()
    {
        float currentTime = 0f;
        while (currentTime <= fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private void ActivateScreen(int nextScreen)
    {
        screens[currentScreen].SetActive(false); // Deactivate the current screen
        currentScreen = nextScreen; // Update the current screen index
        CheckCurrentScreen(); // Ensure the new index is valid
        screens[currentScreen].SetActive(true); // Activate the new screen
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
        breweryZoomed.SetActive(false);
        downArrowButton.SetActive(false);
        sideRoomWithKey.SetActive(false);
    }

    private void CheckCurrentScreen()
    {
        if (currentScreen < 0)
        {
            currentScreen = screens.Count - 1;
        }
        else if (currentScreen >= 4)
        {
            currentScreen = 0;
        }

        if (currentScreen == 2)
        {
            breweryButton.SetActive(true);
        }
        if (currentScreen == 1)
        {
            sideRoomWithKey.SetActive(true);
        }
    }

    public void LeftArrowClick()
    {
        int nextScreen = (currentScreen - 1 + screens.Count) % screens.Count;
        StartCoroutine(FadeToBlackAndSwitch(nextScreen));
    }

    public void RightArrowClick()
    {
        int nextScreen = (currentScreen + 1) % screens.Count;
        StartCoroutine(FadeToBlackAndSwitch(nextScreen));
    }

    public void BackArrowClick()
    {
        StartCoroutine(FadeToBlackAndSwitch(currentScreen));
    }

    public void TurnOnBrewery()
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
        leftArrowButton.SetActive(false);
        rightArrowButton.SetActive(false);
        breweryButton.SetActive(false);

        downArrowButton.SetActive(true);
        breweryZoomed.SetActive(true);
    }

    public void TurnonSideroom()
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
        leftArrowButton.SetActive(false);
        rightArrowButton.SetActive(false);
        sideRoomWithKey.SetActive(false);

        downArrowButton.SetActive(true);
        sideRoomWithKey.SetActive(true);
    }
}
