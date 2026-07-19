using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;

    private CharacterController controller;
    private Vector2 moveInput;

    public Vector2 lookInput;

    private Animator animator;

    private CatInteraction nearbyCat;
    private NpcDialogue nearbyNpc;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }


    public void OnInteract(InputValue value)
    {
        if (nearbyCat != null)
        {
            nearbyCat.Interact();
        }

        if (nearbyNpc != null)
        {
            nearbyNpc.Interact();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        CatInteraction cat = other.GetComponent<CatInteraction>();

        if (cat != null)
        {
            nearbyCat = cat;
        }


        NpcDialogue npc = other.GetComponent<NpcDialogue>();

        if (npc != null)
        {
            nearbyNpc = npc;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        CatInteraction cat = other.GetComponent<CatInteraction>();

        if (cat == nearbyCat)
        {
            nearbyCat = null;
        }


        NpcDialogue npc = other.GetComponent<NpcDialogue>();

        if (npc == nearbyNpc)
        {
            nearbyNpc = null;
        }
    }


    void Update()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * moveInput.y + right * moveInput.x;

        controller.Move(move * speed * Time.deltaTime);


        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }


        float movementSpeed = moveInput.magnitude;
        animator.SetFloat("Speed", movementSpeed);
    }
}