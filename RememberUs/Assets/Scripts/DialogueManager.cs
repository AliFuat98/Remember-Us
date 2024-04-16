using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
  public static DialogueManager Instance { get; private set; }

  public event EventHandler OnDialogueComplete;

  [SerializeField] private TextMeshProUGUI dialogueText;
  [SerializeField] private TextMeshProUGUI dialoguerName;
  [SerializeField] private Image dialoguerImage;

  [SerializeField] private GameObject dialoguePanel;
  [SerializeField] private Button nextDialogButton;
  private Queue<string> sentences;
  private string currentSentence = string.Empty;

  public float typingSpeed = 0.05f;
  private bool isTyping = false;

  private DialogueListSO currentDialogueList;
  private int currentDialogueIndex;

  private void Awake() {
    Instance = this;

    DontDestroyOnLoad(gameObject);

    nextDialogButton.onClick.AddListener(() => {
      if (isTyping) {
        // get result imidiately
        DisplayCompleteCurrentSentence();
      } else {
        DisplayNextSentence();
      }
    });
  }

  void Start() {
    sentences = new Queue<string>();

    dialoguePanel.SetActive(false);
  }

  public void StartDialogueList(DialogueListSO dialogueList) {
    currentDialogueList = dialogueList;
    currentDialogueIndex = 0;
    StartDialogue(currentDialogueList.dialogueList[currentDialogueIndex]);
  }

  public void StartDialogue(DialogueSO dialogueSO) {
    sentences.Clear();
    dialoguePanel.SetActive(true);

    dialoguerName.text = dialogueSO.speakerName;
    dialoguerImage.sprite = dialogueSO.image;

    foreach (string sentence in dialogueSO.sentences) {
      sentences.Enqueue(sentence); // Enqueue all sentences from the dialogue
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence() {
    if (sentences.Count == 0) {
      // Check if there are more dialogues in the list
      if (++currentDialogueIndex < currentDialogueList.dialogueList.Length) {
        StartDialogue(currentDialogueList.dialogueList[currentDialogueIndex]);
      } else {
        EndDialogue();
      }
      return;
    }

    currentSentence = sentences.Dequeue();
    StopAllCoroutines(); // Stop the currently running typewriter coroutine if any
    StartCoroutine(TypeSentence()); // Start a new typewriter coroutine
  }

  private void DisplayCompleteCurrentSentence() {
    StopAllCoroutines();
    dialogueText.text = currentSentence;
    isTyping = false;
  }

  IEnumerator TypeSentence() {
    isTyping = true;
    dialogueText.text = "";
    foreach (char letter in currentSentence.ToCharArray()) {
      dialogueText.text += letter;
      yield return new WaitForSeconds(typingSpeed); // Wait for some time after each character
    }

    isTyping = false;
  }

  void EndDialogue() {
    dialoguePanel.SetActive(false);
    OnDialogueComplete?.Invoke(this, EventArgs.Empty);
  }
}