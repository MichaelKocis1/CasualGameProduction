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
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        spawnBarrierLine = syringeSpawnPoint.transform.position.y;
    }

    private void OnEnable() {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        if (mainCamera != null)
        {
            screenPos2D = touchPositionAction.ReadValue<Vector2>();
            camDepth = mainCamera.transform.position.x;
            screenPos3D = new Vector3(screenPos2D.x, screenPos2D.y, camDepth);
            worldPos = mainCamera.ScreenToWorldPoint(screenPos3D);

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

            if (worldPos.y > spawnBarrierLine)
            {
                syringeSpawnPoint.transform.position = new Vector3(syringeSpawnPoint.transform.position.x, syringeSpawnPoint.transform.position.y, worldPos.z);
                // would be touching on the syringe to move it.
                // Will more clearly define these zones later
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
            Vector3 aimDirection = instantiatedObj.transform.position - worldPos;
            aimDirection = new Vector3(0, aimDirection.y * -2, aimDirection.z * -2);

            // Rotate syringe in aim direction
            //syringeSpawnPoint.transform.rotation = Quaternion.Euler((180 / Mathf.PI * Mathf.Tan(aimDirection.z / aimDirection.y) + 270), 0, 270);

            // Shoots projectile
            instantiatedObj.GetComponent<Rigidbody>().AddForce(aimDirection, ForceMode.Impulse);
            ballsRemaining.ChangeBalls(-1);
        }

        Debug.Log("Screen Position: " + screenPos3D.x + ", " + screenPos3D.y);
        Debug.Log("World Position: " + worldPos.z + ", " + worldPos.y);
    }
}

