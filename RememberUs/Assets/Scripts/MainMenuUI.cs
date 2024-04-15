using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] private Button startButton;
  [SerializeField] private Button quitButton;
  [SerializeField] private LevelLoader levelLoader;

  private void Awake() {
    startButton.onClick.AddListener(() => {
      if (levelLoader != null) {
        levelLoader.LoadNextLevel();
      }
    });

    quitButton.onClick.AddListener(() => {
      Application.Quit();
    });
  }
}