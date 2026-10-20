using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Rotation")]
    public float turnSpeed = 15f;   
    public bool smoothRotation = true;

    private CharacterController controller;
    private Camera cam;

    public interface IInteractable
    {
        void Interact();
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }//e

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");  

   
        Vector3 move = new Vector3(h, 0f, v).normalized;

       
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.position.y; 
        Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePos);

        Vector3 direction = worldMousePos - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        if (smoothRotation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * turnSpeed
            );
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }
}