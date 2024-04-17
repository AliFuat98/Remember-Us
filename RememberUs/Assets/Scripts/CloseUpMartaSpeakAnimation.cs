using UnityEngine;

public class CloseUpMartaSpeakAnimation : MonoBehaviour {
  Animator animator;

  private void Start() {
    animator = GetComponent<Animator>();
    DialogueManager.Instance.OnTypingChanged += DialogueManager_OnTypingChanged;
  }

  private void DialogueManager_OnTypingChanged(object sender, DialogueManager.OnTypingChangedEventArgs e) {
    if (e.IsNowTyping) {
      animator.SetTrigger("Speak");
    }
  }
}