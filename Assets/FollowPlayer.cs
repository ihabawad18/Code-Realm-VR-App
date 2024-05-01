using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera;
    public float verticalOffset;
    public float leftOffset; 
    void Update()
    {
        // Calculate the adjusted position
        Vector3 offsetPosition = cameraTransform.forward * distanceFromCamera // Forward offset
                               + cameraTransform.up * verticalOffset      // Vertical offset
                               + cameraTransform.right * leftOffset;     // Leftward offset

        // Update the position
        transform.position = cameraTransform.position + offsetPosition;
        // Rotate the canvas to face towards the camera
        //transform.LookAt(vrCameraTransform);
        transform.rotation = cameraTransform.rotation;
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
