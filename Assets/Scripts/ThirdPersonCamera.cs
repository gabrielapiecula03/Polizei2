using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 7f;
    public float height = 5f;
    public float sensitivity = 0.1f;

    private PlayerMovement playerMovement;
    private float yaw;

    void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        if (playerMovement == null)
            return;

        yaw += playerMovement.lookInput.x * sensitivity;

        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        Vector3 offset = rotation * new Vector3(0f, height, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}