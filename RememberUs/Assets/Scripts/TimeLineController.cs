using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class TimelineController : MonoBehaviour {
  public List<PlayableDirector> timelines; // List of all Timelines
  private int currentIndex = 0; // Index to track the current timeline
  private bool isPaused = false;

  private void Start() {
    // Subscribe to the stopped event for each timeline
    foreach (var timeline in timelines) {
      timeline.stopped += OnTimelineStopped;
    }

    // Start the first timeline if any exist
    if (timelines.Count > 0) {
      PlayTimeline(currentIndex);
    }
  }

  private void PlayTimeline(int index) {
    if (index < timelines.Count) {
      timelines[index].Play();
      isPaused = false;
    }
  }

  private void OnTimelineStopped(PlayableDirector director) {

    if (currentIndex < timelines.Count - 1) {
      currentIndex++;
      PlayTimeline(currentIndex);
    }
  }


  public void PauseTimeline() {
    if (currentIndex < timelines.Count) {
      timelines[currentIndex].Pause();
      isPaused = true;
    }
  }

  // Resume the currently paused timeline
  public void ResumeTimeline() {
    if (isPaused && currentIndex < timelines.Count) {
      timelines[currentIndex].Resume();
      isPaused = false;
    }
  }

  private void OnDestroy() {
    foreach (var timeline in timelines) {
      timeline.stopped -= OnTimelineStopped;
    }
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.P)) {
      PauseTimeline();
    }

    if (Input.GetKeyDown(KeyCode.R)) {
      ResumeTimeline();
    }
  }
}