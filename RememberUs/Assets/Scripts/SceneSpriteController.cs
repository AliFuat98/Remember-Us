using System.Collections;
using UnityEngine;

public class SceneSpriteController : MonoBehaviour {
  public SpriteRenderer spriteRenderer;  // Single SpriteRenderer
  public SpriteFadeActionSO fadeAction;

  private void OnEnable() {
    fadeAction.OnFadeRequested += PerformFade;
  }

  private void OnDisable() {
    fadeAction.OnFadeRequested -= PerformFade;
  }

  private void PerformFade(Sprite newSprite, float duration) {
    StartCoroutine(FadeSpriteChange(newSprite, duration / 2));
  }

  private IEnumerator FadeSpriteChange(Sprite newSprite, float duration) {
    // Fade out current sprite
    yield return StartCoroutine(FadeSprite(spriteRenderer, 1, 0, duration));

    // Change sprite at the midpoint of the fade
    spriteRenderer.sprite = newSprite;

    // Fade in new sprite
    yield return StartCoroutine(FadeSprite(spriteRenderer, 0, 1, duration));
  }

  private IEnumerator FadeSprite(SpriteRenderer sprite, float startAlpha, float endAlpha, float duration) {
    float counter = 0f;
    while (counter < duration) {
      float alpha = Mathf.Lerp(startAlpha, endAlpha, counter / duration);
      sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
      counter += Time.deltaTime;
      yield return null;
    }
    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, endAlpha);
  }
}