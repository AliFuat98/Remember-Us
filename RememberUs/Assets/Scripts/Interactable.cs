using UnityEngine;

public class Interactable : MonoBehaviour {
  public GameObject interact;
  public DialogueSO dialogueSO;

  private void Start() {
    interact.SetActive(false);
  }

  public void ShowInteract(bool show) {
    interact.SetActive(show);
  }

  public void Interact() {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogueSO);
  }
}