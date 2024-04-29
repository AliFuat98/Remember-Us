using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteract : MonoBehaviour
{
  public LevelLoader levelLoader;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.TryGetComponent(out PlayerMovement _)) {
      levelLoader.LoadNextLevel();
    }
  }
}
