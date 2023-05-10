using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject exitPortal;
    private Portal otherPortal;
    public bool isBlue;
    private bool canTeleport = true;
    public float teleportCooldown = 0.5f;

    private void Start()
    {
        otherPortal = exitPortal.GetComponent<Portal>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.gameObject.CompareTag("Ball")) // Check if the collided object is the ball
        {
            Vector2 exitDirection = exitPortal.transform.right * (isBlue ? 1 : -1); // Determine the exit direction based on the portal color
            other.transform.position = exitPortal.transform.position + (Vector3)exitDirection; // Move the ball to the exit portal
            other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.magnitude * exitDirection; // Adjust the ball's velocity to exit in the right direction

            // Prevent both portals from teleporting the ball for a short period of time
            canTeleport = false;
            otherPortal.canTeleport = false;

            // Start the cooldown period
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
        otherPortal.canTeleport = true;
    }
}
