using UnityEngine;

public class Interactable : MonoBehaviour {
  public GameObject interact;
  public DialogueSO dialogueSO;

  public void ShowInteract(bool show) {
    if (interact != null) {
      interact.SetActive(show);
    }
  }

  public virtual void Interact() {
    DialogueManager.Instance.StartDialogue(dialogueSO);
  }
}