using UnityEngine;
using System.Collections;
using UnityEngine.iOS;

public class TouchTEST : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;

    Vector2 screenPos2D;
    Vector3 screenPos3D;
    Vector3 worldPos;
    public float camDepth;

    private BallsRemaining ballsRemaining;

    public GameObject syringeSpawnPoint;
    public float spawnBarrierLine;
    Vector3 aimDirection;

    [SerializeField] private LineRenderer trajectoryLine;

    private void Awake() {
        trajectoryLine.enabled = false;
    }

    void Start() {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        spawnBarrierLine = syringeSpawnPoint.transform.position.y;
    }

    void Update() {
        Touch touch;

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                if (mainCamera != null)
                {
                    screenPos2D = touch.position;
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

                    // If pressing at or above syringe, move syringe
                    if (worldPos.y > spawnBarrierLine)
                    {
                        syringeSpawnPoint.transform.position = new Vector3(syringeSpawnPoint.transform.position.x, syringeSpawnPoint.transform.position.y, worldPos.z);
                    }
                    else
                    {
                        trajectoryLine.enabled = true;
                        trajectoryLine.positionCount = 2;
                        trajectoryLine.SetPosition(0, syringeSpawnPoint.transform.position);
                        trajectoryLine.SetPosition(1, worldPos);

                        // Calculates aim direction from syringeSpawn to touch position
                        aimDirection = worldPos - syringeSpawnPoint.transform.position;
                        aimDirection = new Vector3(0, aimDirection.y * 1, aimDirection.z * 1);

                        rotateSyringe();
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                if (mainCamera != null)
                {
                    screenPos2D = touch.position;
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

                    // If pressing at or above syringe, move syringe
                    if (worldPos.y > spawnBarrierLine)
                    {
                        syringeSpawnPoint.transform.position = new Vector3(syringeSpawnPoint.transform.position.x, syringeSpawnPoint.transform.position.y, worldPos.z);
                    }
                    else
                    {
                        trajectoryLine.enabled = true;
                        trajectoryLine.positionCount = 2;
                        trajectoryLine.SetPosition(0, syringeSpawnPoint.transform.position);
                        trajectoryLine.SetPosition(1, worldPos);

                        // Calculates aim direction from syringeSpawn to touch position
                        aimDirection = worldPos - syringeSpawnPoint.transform.position;
                        aimDirection = new Vector3(0, aimDirection.y * 1, aimDirection.z * 1);

                        rotateSyringe();
                        trajectoryLine.enabled = false;
                        spawnBall();
                    }
                }
            }
            
        }        
    }

    private void rotateSyringe() {
        // Rotate syringe in aim direction
        syringeSpawnPoint.transform.rotation = Quaternion.Euler((180 / Mathf.PI * Mathf.Atan(aimDirection.z / aimDirection.y)), 0, 0);
        
        // Debug.Log((180 / Mathf.PI * Mathf.Atan(aimDirection.z / aimDirection.y)));
    }

    private void spawnBall() {
        if (ballsRemaining.getNumBallsRemaining() > 0)
        {
            // Spawns projectile at tip of syringe
            GameObject instantiatedObj = GameObject.Instantiate(player, new Vector3(0, syringeSpawnPoint.transform.position.y, syringeSpawnPoint.transform.position.z), Quaternion.identity);

            // Shoots projectile
            instantiatedObj.GetComponent<Rigidbody>().AddForce(aimDirection * 2, ForceMode.Impulse);
            ballsRemaining.ChangeBalls(-1);
        }

        // Debug.Log("Screen Position: " + screenPos3D.x + ", " + screenPos3D.y);
        // Debug.Log("World Position: " + worldPos.z + ", " + worldPos.y);
    }
}