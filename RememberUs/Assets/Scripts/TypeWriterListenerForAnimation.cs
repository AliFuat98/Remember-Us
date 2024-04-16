using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TypeWriterListenerForAnimation : MonoBehaviour
{
  Animator anim;
  private void Awake() {
    anim = GetComponent<Animator>();
  }
}
