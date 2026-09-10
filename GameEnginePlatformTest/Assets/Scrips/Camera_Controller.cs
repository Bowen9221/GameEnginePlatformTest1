using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] InputSystem_Manager isManager;



    [Header("Camera Rotation")]
    public float _senseX;
    public float _senseY;
    public Transform _orientation;

    private Vector2 lookInput;

    private float xRotation;
    private float yRotation;


    private void OnEnable()
    {
        isManager.OnLookInput += ReceiveLookInput;
    }

    private void OnDisable()
    {
        isManager.OnLookInput -= ReceiveLookInput;
    }

    private void ReceiveLookInput(Vector2 mouseDelta)
    {
            lookInput = mouseDelta;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {

        //Getting mouse input
        float MouseX = lookInput.x * Time.deltaTime * _senseX;
        float MouseY = lookInput.y * Time.deltaTime * _senseY;

        yRotation += MouseX;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Handling cam rotations and orientation
        _orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
