using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject {

  [TextArea(3, 10)]
  public string[] sentences;

  public string speakerName;
  public Sprite image;
  public Color speakerNameColor = Color.white;
}