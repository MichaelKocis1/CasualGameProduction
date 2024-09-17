using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Camera mainCamera;
    
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable() {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();

        if (Camera.main != null) {
            
            float depth = 17f; // Adjust this value based on your camera x 
            Vector3 screenPos = new Vector3(screenPosition.x, screenPosition.y, depth);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

              Instantiate(player, worldPosition, Quaternion.identity);


            Debug.Log("Screen Position: " + screenPosition.x + ", " + screenPosition.y);
            Debug.Log("World Position: " + worldPosition.x + ", " + worldPosition.y); 
        }
    }
}
