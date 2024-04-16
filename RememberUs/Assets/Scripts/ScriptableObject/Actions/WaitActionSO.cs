using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Action", menuName = "Cutscene/Actions/Create Wait Action")]
public class WaitActionSO : ScriptableObject, ICutsceneAction {
  public float duration;

  public IEnumerator Execute() {
    yield return new WaitForSeconds(duration);
  }
}