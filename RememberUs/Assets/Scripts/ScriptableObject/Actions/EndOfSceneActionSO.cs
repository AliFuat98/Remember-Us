using System.Collections;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Action", menuName = "Cutscene/Actions/Create End of Scene Action")]
public class EndOfSceneActionSO : ScriptableObject, ICutsceneAction {

  public event Action OnRequestNextLevel;

  public IEnumerator Execute() {
    OnRequestNextLevel?.Invoke();
    yield return null;
  }
}
