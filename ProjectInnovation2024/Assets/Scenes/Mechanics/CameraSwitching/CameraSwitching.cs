using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitching : MonoBehaviour {

  [SerializeField] private List<GameObject> screens = new List<GameObject>(); //setup array list
  public int currentScreen = 0;
  [SerializeField] private Image blackBackground;
  [SerializeField] private float fadeDuration = 1f;

  void Start() {
    blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0f);
  }

  void Update() {

  }

  private IEnumerator FadeToBlackAndSwitch(int nextScreen) {
    yield return StartCoroutine(FadeToBlack()); // Fade to black
    ActivateScreen(nextScreen); // Activate the next screen
    yield return StartCoroutine(FadeFromBlack()); // Fade back in
  }

  private IEnumerator FadeToBlack() {
    float currentTime = 0f;
    while (currentTime <= fadeDuration) {
      float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
      blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
      currentTime += Time.deltaTime;
      yield return null;
    }
  }

  private IEnumerator FadeFromBlack() {
    float currentTime = 0f;
    while (currentTime <= fadeDuration) {
      float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
      blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
      currentTime += Time.deltaTime;
      yield return null;
    }
  }

  private void ActivateScreen(int nextScreen) {
    screens[currentScreen].SetActive(false); // Deactivate the current screen
    currentScreen = nextScreen; // Update the current screen index
    CheckCurrentScreen(); // Ensure the new index is valid
    screens[currentScreen].SetActive(true); // Activate the new screen
  }

  private void CheckCurrentScreen() {
    if (currentScreen < 0) {
      currentScreen = screens.Count - 1;
    }
    else if (currentScreen >= screens.Count) {
      currentScreen = 0;
    }
  }

  public void LeftArrowClick() {
    int nextScreen = (currentScreen - 1 + screens.Count) % screens.Count;
    StartCoroutine(FadeToBlackAndSwitch(nextScreen));
  }

  public void RightArrowClick() {
    int nextScreen = (currentScreen + 1) % screens.Count;
    StartCoroutine(FadeToBlackAndSwitch(nextScreen));
  }
}
