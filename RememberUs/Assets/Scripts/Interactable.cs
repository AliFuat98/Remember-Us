using UnityEngine;

public class Interactable : MonoBehaviour {
  public GameObject interact;

  private void Start() {
    interact.SetActive(false);
  }

  public void ShowInteract(bool show) {
    interact.SetActive(show);
  }

  public void Interact() {
    Debug.Log("Interacted with " + gameObject.name);
  }
}