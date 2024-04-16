using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sprite Fade Action", menuName = "Cutscene/Actions/Create Sprite Fade Action")]
public class SpriteFadeActionSO : ScriptableObject, ICutsceneAction {

  public Sprite spriteToFadeIn;  // Sprite to fade in
  public float fadeDuration = 1.0f;

  public event Action<Sprite, float> OnFadeRequested;  // Updated to pass Sprite and duration

  public IEnumerator Execute() {
    OnFadeRequested?.Invoke(spriteToFadeIn, fadeDuration);
    yield return new WaitForSeconds(fadeDuration);
  }
}