using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
  public static DialogueManager Instance;
  public TextMeshProUGUI dialogueText;
  public TextMeshProUGUI dialogueName;
  public Image dialogueImage;

  public GameObject dialoguePanel;
  public Button nextDialogButton;
  private Queue<string> sentences;

  public float typingSpeed = 0.05f;

  private bool isTyping = false;
  private string currentSentece = string.Empty;

  private void Awake() {
    nextDialogButton.onClick.AddListener(() => {
      if (isTyping) {
        // get result
        DisplayCompleteCurrentSentence();
      } else {
        DisplayNextSentence();
      }
    });
  }

  void Start() {
    Instance = this;

    sentences = new Queue<string>();
    dialoguePanel.SetActive(false); // Start with the dialogue panel hidden
  }

  public void StartDialogue(DialogueSO dialogueSO) {
    sentences.Clear();
    dialoguePanel.SetActive(true);

    dialogueName.text = dialogueSO.speakerName;
    dialogueImage.sprite = dialogueSO.image;

    foreach (string sentence in dialogueSO.sentences) {
      sentences.Enqueue(sentence); // Enqueue all sentences from the dialogue
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence() {
    if (sentences.Count == 0) {
      EndDialogue();
      return;
    }

    currentSentece = sentences.Dequeue();
    StopAllCoroutines(); // Stop the currently running typewriter coroutine if any
    StartCoroutine(TypeSentence()); // Start a new typewriter coroutine
  }

  private void DisplayCompleteCurrentSentence() {
    StopAllCoroutines();
    dialogueText.text = currentSentece;
    isTyping = false;
  }

  IEnumerator TypeSentence() {
    isTyping = true;
    dialogueText.text = "";
    foreach (char letter in currentSentece.ToCharArray()) {
      dialogueText.text += letter;
      yield return new WaitForSeconds(typingSpeed); // Wait for one frame after each character
    }

    isTyping = false;
  }

  void EndDialogue() {
    dialoguePanel.SetActive(false);
  }
}