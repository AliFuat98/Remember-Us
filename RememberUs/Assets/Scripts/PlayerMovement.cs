using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  Animator animator;
  Rigidbody2D rb;

  public float speed = 5.0f;
  float xInput;
  float yInput;

  public Vector2 lastMoveDirection { get; private set; } = Vector2.right;

  public int FacingDirection { get; private set; } = -1;
  bool facingRight = false;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponentInChildren<Animator>();
  }

  void Update() {
    xInput = Input.GetAxisRaw("Horizontal");
    yInput = Input.GetAxisRaw("Vertical");
    animator.SetBool("Move", IsMoving());

    if (IsMoving()) {
      animator.SetFloat("xInput", xInput);
      animator.SetFloat("yInput", yInput);
    }
  }

  bool IsMoving() {
    return !(xInput == 0 && yInput == 0);
  }

  void FixedUpdate() {
    SetVelocity();
  }

  public void SetVelocity() {
    Vector2 moveDirection = new Vector2(xInput, yInput).normalized;

    // record last direction
    if (moveDirection != Vector2.zero) {
      lastMoveDirection = moveDirection;
    }

    rb.velocity = moveDirection * speed;
    //FlipController(xInput);
  }

  public void SetZeroVelocity() {
    rb.velocity = new Vector2(0f, 0f);
  }

  public virtual void FlipController(float xVelocity) {
    if (xVelocity > 0 && !facingRight) {
      Flip();
    } else if (xVelocity < 0 && facingRight) {
      Flip();
    }
  }

  public virtual void Flip() {
    FacingDirection *= -1;
    facingRight = !facingRight;
    transform.Rotate(0, 180, 0);
  }
}