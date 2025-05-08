using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -5);

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public float pitchMin = -20f;
    public float pitchMax = 80f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        
        Vector3 desiredPos = target.position + rotation * offset;
        transform.position = desiredPos;

        
        transform.LookAt(target.position);

        
        target.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
