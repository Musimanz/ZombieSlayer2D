using UnityEngine;

public class ZombieScreenTracker : MonoBehaviour
{
    private Camera mainCamera;
    private Danger dangerArrow;
    private Transform playerTransform;

    private bool isOffScreenLastFrame = false;

    void Start()
    {
        mainCamera = Camera.main;

        // Grab the danger arrow script from the scene
        GameObject dangerObj = GameObject.FindGameObjectWithTag("Danger");
        if (dangerObj != null)
        {
            dangerArrow = dangerObj.GetComponent<Danger>();
        }

        // Grab player's transform using the Singleton pattern we set up
        if (Shooter.Instance != null)
        {
            playerTransform = Shooter.Instance.transform;
        }
    }

    void Update()
    {
        if (mainCamera == null || dangerArrow == null || playerTransform == null) return;

        // 1. Convert zombie's world position to Camera Viewport space
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);

        // 2. Check if the zombie is currently off-screen
        // (Z > 0 ensures the zombie is in front of the camera, not behind it)
        bool isOffScreen = screenPoint.x < 0f || screenPoint.x > 1f || screenPoint.y < 0f || screenPoint.y > 1f || screenPoint.z < 0f;

        if (isOffScreen)
        {
            // Calculate which side of the player the zombie is on
            bool comingFromRight = transform.position.x > playerTransform.position.x;

            dangerArrow.ActivateIndicator(comingFromRight);
            isOffScreenLastFrame = true;
        }
        else
        {
            // If the zombie just stepped ON screen this frame, turn the arrow off!
            if (isOffScreenLastFrame)
            {
                dangerArrow.DeactivateIndicator();
                isOffScreenLastFrame = false;
            }
        }
    }

    // Safety: If the zombie is killed while off-screen, turn the arrow off!
    void OnDestroy()
    {
        if (dangerArrow != null && isOffScreenLastFrame)
        {
            dangerArrow.DeactivateIndicator();
        }
    }
}