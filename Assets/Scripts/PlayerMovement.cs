using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
    }

   public void OnMove(InputValue value)
{
    moveInput = value.Get<Vector2>();
}

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        controller.Move(move * speed * Time.deltaTime);

        if (move != Vector3.zero)
{
    transform.rotation = Quaternion.LookRotation(move);
}
     float movementSpeed = move.magnitude;
     animator.SetFloat("Speed", movementSpeed);
    }
}