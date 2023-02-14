using Player;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private const float MinYRotation = -5;
    private const float MaxYRotation = 20;
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _player;

    private float _currentYRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        if (_player.TryGetComponent(out PlayerHealth playerHealth))
            playerHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        if (_player.TryGetComponent(out PlayerHealth playerHealth))
            playerHealth.Died -= OnDied;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis(MouseX);
        float mouseY = Input.GetAxis(MouseY);
        
        mouseX *= _mouseSensitivity;
        mouseY *= _mouseSensitivity;
        
        _currentYRotation += mouseY;
        _currentYRotation = Mathf.Clamp(_currentYRotation, MinYRotation, MaxYRotation);

        transform.localEulerAngles = Vector3.right * -_currentYRotation;
            
        _player.Rotate(Vector3.up * mouseX);
    }

    private void OnDied()
    {
        enabled = false;
    }
}
