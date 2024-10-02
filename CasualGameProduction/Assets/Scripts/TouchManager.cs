using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    public float depth;
    
    private BallsRemaining ballsRemaining;


    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    void Start() {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }
    }

    private void OnEnable() {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();

        if (mainCamera != null) {
            depth = mainCamera.transform.position.x;
            Vector3 screenPos = new Vector3(screenPosition.x, screenPosition.y, depth);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

            if (ballsRemaining.getNumBallsRemaining() > 0) {
                Instantiate(player, worldPosition, Quaternion.identity);
                ballsRemaining.ChangeBalls(-1);
            }

            Debug.Log("Screen Position: " + screenPosition.x + ", " + screenPosition.y);
            Debug.Log("World Position: " + worldPosition.x + ", " + worldPosition.y); 
        }
    }
}
