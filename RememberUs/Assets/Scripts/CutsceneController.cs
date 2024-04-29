using System.Collections;
using UnityEngine;

public class CutsceneController : MonoBehaviour {
  public CutsceneActionListSO cutsceneActionListSO;

  private void Start() {
    if (cutsceneActionListSO == null) {
      Debug.Log("assign a cutscene action list SO");
      return;
    }

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