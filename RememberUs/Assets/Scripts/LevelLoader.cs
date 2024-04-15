using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
  public Animator transitionAnimator;
  public float transitionTime = 1.0f;
  public bool fadeIn = true;
  public bool fadeOut = true;

  private void Start() {
    if (fadeIn) {
      transitionAnimator.SetTrigger("End");
    }
  }

  public void LoadNextLevel() {
    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
  }

  IEnumerator LoadLevel(int levelIndex) {
    if (fadeOut) {
      // play animation
      transitionAnimator.SetTrigger("Start");

      // wait
      yield return new WaitForSeconds(transitionTime);
    }

    // load scene
    SceneManager.LoadScene(levelIndex);
  }
}