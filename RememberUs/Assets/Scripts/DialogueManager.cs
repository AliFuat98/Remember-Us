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

  #region IS TYPING

  public event EventHandler<OnTypingChangedEventArgs> OnTypingChanged;

  public class OnTypingChangedEventArgs {
    public bool IsNowTyping;
  }

  private bool isTyping = false;

  public bool IsTyping {
    get { return isTyping; }
    set {
      if (isTyping != value) {
        isTyping = value;

        // trigger event
        OnTypingChanged?.Invoke(this, new OnTypingChangedEventArgs {
          IsNowTyping = value
        });
      }
    }
  }

  #endregion IS TYPING

  private DialogueListSO currentDialogueList;
  private int currentDialogueIndex;

  private void Awake() {
    Instance = this;

    DontDestroyOnLoad(gameObject);

    nextDialogButton.onClick.AddListener(() => {
      if (IsTyping) {
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

  private void DisplayCompleteCurrentSentence() {
    StopAllCoroutines();
    dialogueText.text = currentSentence;
    IsTyping = false;
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

    if (dialogueSO.image != null) {
      dialoguerImage.sprite = dialogueSO.image;
      // Set image to fully opaque
      dialoguerImage.color = new Color(dialoguerImage.color.r, dialoguerImage.color.g, dialoguerImage.color.b, 1f);
    } else {
      dialoguerImage.sprite = null;
      // Set image to fully transparent
      dialoguerImage.color = new Color(dialoguerImage.color.r, dialoguerImage.color.g, dialoguerImage.color.b, 0f);
    }

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

  IEnumerator TypeSentence() {
    IsTyping = true;
    dialogueText.text = "";
    foreach (char letter in currentSentence.ToCharArray()) {
      dialogueText.text += letter;
      yield return new WaitForSeconds(typingSpeed); // Wait for some time after each character
    }

    IsTyping = false;
  }

  void EndDialogue() {
    dialoguePanel.SetActive(false);
    OnDialogueComplete?.Invoke(this, EventArgs.Empty);
  }
}