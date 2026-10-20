using UnityEngine;

public class Cameraa : MonoBehaviour
{
    public Transform target;
    public float height = 15f;
    public float distance = 0f;
    public float followSpeed = 8f;
    public float rotationSpeed = 8f;   

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position
                                + Vector3.up * height
                                + Vector3.back * distance;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * followSpeed
        );


        Quaternion desiredRotation = Quaternion.LookRotation(
         target.position - transform.position,
         Vector3.forward   
         );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}