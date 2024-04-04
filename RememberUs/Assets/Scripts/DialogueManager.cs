using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
  public TextMeshProUGUI dialogueText;
  public TextMeshProUGUI dialogueName;
  public Image dialogueImage;

  public GameObject dialoguePanel;
  public Button nextDialogButton;
  private Queue<string> sentences;

  private void Awake() {
    nextDialogButton.onClick.AddListener(() => {
      DisplayNextSentence();
    });
  }

  void Start() {
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

    string sentence = sentences.Dequeue(); // Get next sentence
    dialogueText.text = sentence; // Display it
  }

  void EndDialogue() {
    dialoguePanel.SetActive(false);
  }
}