using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "New Dialogue Action", menuName = "Cutscene/Actions/Create Timeline Action")]
public class TimelineActionSO : ScriptableObject, ICutsceneAction {
  public IEnumerator Execute() {
    PlayableDirector director = TimelineController.Instance.PlayTimeline();

    director.Play();
    while (director.state == PlayState.Playing) {
      yield return null;
    }
  }
}