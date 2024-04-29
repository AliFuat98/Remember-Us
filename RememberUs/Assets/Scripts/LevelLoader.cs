using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
  public Animator transitionAnimator;
  public float fadeOutTransitionTime = 1.0f;
  public bool fadeIn = true;
  public bool fadeOut = true;

  public EndOfSceneActionSO endOfSceneActionSO;

  private void Start() {
    if (fadeIn) {
      transitionAnimator.SetTrigger("End");
    }

    if (endOfSceneActionSO != null) {
      endOfSceneActionSO.OnRequestNextLevel += LoadNextLevel;
    }
  }

  public void LoadNextLevel() {
    if (SceneManager.sceneCount <= SceneManager.GetActiveScene().buildIndex + 1) {
      Debug.Log("last level");
      return;
    }

    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
  }

  IEnumerator LoadLevel(int levelIndex) {
    if (fadeOut) {
      // play animation
      transitionAnimator.SetTrigger("Start");

      // wait
      yield return new WaitForSeconds(fadeOutTransitionTime);
    }

    // load scene
    SceneManager.LoadScene(levelIndex);
  }
}