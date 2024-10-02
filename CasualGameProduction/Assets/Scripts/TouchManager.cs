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
        if (mainCamera != null) {
            Vector2 screenPos2D = touchPositionAction.ReadValue<Vector2>();
            depth = mainCamera.transform.position.x;
            Vector3 screenPos3D = new Vector3(screenPos2D.x, screenPos2D.y, depth);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos3D);

            /*
            if (above designated height) {
                move position of syringe launcher only along z axis
            } else if (in stomach range) {
                - rotate syringe to face that point
                - make sure its only in box and cant go up or out of designated area
                    - if out of designated area stop line and process
                - create a dotted line moving toward touch point in zone 
                    - make that line have a max length so that it can't go infinitely
                    - scale line image in size to show strength of shot
                - when released, spawn ball with a force in direction of release point
                    - distance to release point determines strength of force
                - remove line
            }
            */

            if (ballsRemaining.getNumBallsRemaining() > 0) {
                Instantiate(player, worldPos, Quaternion.identity);
                ballsRemaining.ChangeBalls(-1);
            }

            Debug.Log("Screen Position: " + screenPos3D.x + ", " + screenPos3D.y);
            Debug.Log("World Position: " + worldPos.x + ", " + worldPos.y);
        }
    }

    private void spawnBall() {

    }
}

