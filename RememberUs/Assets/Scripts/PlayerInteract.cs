using UnityEngine;

public class PlayerInteract : MonoBehaviour {
  public Transform interactPoint;
  public float interactRange = 5f;
  public LayerMask interactableLayer;
  PlayerMovement doctorMovement;

  private Interactable xLastInteractable;

  public Interactable LastInteractable {
    get {
      return xLastInteractable;
    }
    set {
      // close previous if exists
      if (xLastInteractable != null) {
        LastInteractable.ShowInteract(show: false);
      }

      // assign the new value
      xLastInteractable = value;

      // if there is one, open it
      if (value != null) {
        LastInteractable.ShowInteract(show: true);
      }
    }
  }

  private void Start() {
    doctorMovement = GetComponent<PlayerMovement>();
  }

  void Update() {
    RaycastHit2D hit = Physics2D.Raycast(interactPoint.position, doctorMovement.lastMoveDirection, interactRange, interactableLayer);

    Interactable currentInteractable = null;

    if (hit.collider != null) {
      // there is an interactable

      if (hit.collider.TryGetComponent<Interactable>(out var interactable)) {
        // record last interactable
        currentInteractable = interactable;

        if (Input.GetKeyDown(KeyCode.E)) {
          interactable.Interact();
        }
      }
    }

    // Only update LastInteractable if the current interactable is different
    if (currentInteractable != LastInteractable) {
      LastInteractable = currentInteractable;
    }

  }

  void OnDrawGizmosSelected() {
    if (interactPoint == null || doctorMovement == null)
      return;

    Gizmos.color = Color.red;
    Vector3 direction = doctorMovement.lastMoveDirection * interactRange;

    Gizmos.DrawRay(interactPoint.position, direction);
  }
}