using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue Action", menuName = "Cutscene/Actions/Create Dialogue Action")]
public class DialogueActionSO : ScriptableObject, ICutsceneAction {
  public DialogueListSO dialogueList;

  public IEnumerator Execute() {
    bool isDialogueComplete = false;

    void OnDialogueFinished(object sender, System.EventArgs e) {
      isDialogueComplete = true;
    }

    DialogueManager.Instance.OnDialogueComplete += OnDialogueFinished;
    DialogueManager.Instance.StartDialogueList(dialogueList);

    // Wait until the dialogue is completed (i.e., next button is pressed)
    yield return new WaitUntil(() => isDialogueComplete);

    // Clean up the event to prevent memory leaks
    DialogueManager.Instance.OnDialogueComplete -= OnDialogueFinished;
  }
}