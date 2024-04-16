using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour {
  public static TimelineController Instance { get; private set; }

  public List<PlayableDirector> timelines; // List of all Timelines
  private int currentIndex = 0; // Index to track the current timeline
  private bool isPaused = false;

  private void Awake() {
    Instance = this;
  }

  public PlayableDirector PlayTimeline() {
    if (currentIndex < timelines.Count) {
      isPaused = false;
      timelines[currentIndex].Play();
      currentIndex++;
      return timelines[currentIndex-1];
    }
    Debug.LogError("Check timeline count and timeline action scriptable objects. They must be match");
    return null;
  }

  public void PauseTimeline() {
    if (currentIndex < timelines.Count) {
      timelines[currentIndex].Pause();
      isPaused = true;
    }
  }

  public void ResumeTimeline() {
    if (isPaused && currentIndex < timelines.Count) {
      timelines[currentIndex].Resume();
      isPaused = false;
    }
  }
}