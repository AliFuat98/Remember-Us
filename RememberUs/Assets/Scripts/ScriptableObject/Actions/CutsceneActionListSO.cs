using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Cutscene", menuName = "Cutscene/Create Action List")]
public class CutsceneActionListSO : ScriptableObject {
  public List<ScriptableObject> actions;
}