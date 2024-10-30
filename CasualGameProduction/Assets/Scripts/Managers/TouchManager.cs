using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    Vector2 screenPos2D;
    Vector3 screenPos3D;
    Vector3 worldPos;
    public float camDepth;

    private BallsRemaining ballsRemaining;

    public GameObject syringeSpawnPoint;
    public float spawnBarrierLine;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    void Start() {
        // Gets the reference for ballsText
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        // Sets the height of the barrier line between the zones for moving the syringe and shooting
        spawnBarrierLine = syringeSpawnPoint.transform.position.y;
    }

    // Enables touch controls
    private void OnEnable() {
        touchPressAction.performed += TouchPressed;
    }
    
    // Disables touch controls
    private void OnDisable() {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        Debug.Log("TouchPress is called");
        if (mainCamera != null)
        {
            screenPos2D = touchPositionAction.ReadValue<Vector2>();
            camDepth = mainCamera.transform.position.x;
            screenPos3D = new Vector3(screenPos2D.x, screenPos2D.y, camDepth);
            worldPos = mainCamera.ScreenToWorldPoint(screenPos3D);

            if (context.started)
            {
                Debug.Log("START");
            }
            if (context.performed)
            {
                Debug.Log("PERFORMED");
            }
            if (context.canceled)
            {
                Debug.Log("CANCELED");
            }

            /*
            context.started // True when the button is initially pressed 

            context.performed // True while if you are holding the button down

            context.canceled // True when the button gets released

            States I want:
            Above line, below line
            Active touch (on/off) - disable touch if you slide over boundary line


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

            if (worldPos.y > spawnBarrierLine)
            {
                syringeSpawnPoint.transform.position = new Vector3(syringeSpawnPoint.transform.position.x, syringeSpawnPoint.transform.position.y, worldPos.z);
            }
            else
            {
                spawnBall();
            }
        }
    }

    private void spawnBall() {
        if (ballsRemaining.getNumBallsRemaining() > 0)
        {
            // Spawns projectile at tip of syringe
            GameObject instantiatedObj = GameObject.Instantiate(player, new Vector3(0, syringeSpawnPoint.transform.position.y, syringeSpawnPoint.transform.position.z), Quaternion.identity);

            // Calculates aim direction from projectile to touch position
            Vector3 aimDirection = worldPos - instantiatedObj.transform.position;
            aimDirection = new Vector3(0, aimDirection.y * 1, aimDirection.z * 1);

            // Rotate syringe in aim direction
            syringeSpawnPoint.transform.rotation = Quaternion.Euler((180 / Mathf.PI * Mathf.Atan(aimDirection.z / aimDirection.y)), 0, 0);
            Debug.Log((180 / Mathf.PI * Mathf.Atan(aimDirection.z / aimDirection.y)));

            // Shoots projectile
            instantiatedObj.GetComponent<Rigidbody>().AddForce(aimDirection * 2, ForceMode.Impulse);
            ballsRemaining.ChangeBalls(-1);
        }

        // Debug.Log("Screen Position: " + screenPos3D.x + ", " + screenPos3D.y);
        // Debug.Log("World Position: " + worldPos.z + ", " + worldPos.y);
    }
}