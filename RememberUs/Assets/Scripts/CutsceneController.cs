using System.Collections;
using UnityEngine;

public class CutsceneController : MonoBehaviour {
  public CutsceneActionListSO cutsceneActionListSO;

  private void Start() {
    StartCoroutine(PlayCutscene());
  }

  private IEnumerator PlayCutscene() {
    yield return new WaitForSeconds(2f);
    foreach (ScriptableObject actionObject in cutsceneActionListSO.actions) {
      if (actionObject is ICutsceneAction action) {
        yield return StartCoroutine(action.Execute());
      }
    }
  }
}