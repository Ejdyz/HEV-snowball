using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject exitPortal;
    private Portal otherPortal;
    
    public bool isBlue;
    private bool canTeleport = true;
    public float teleportCooldown = 0.5f;

    public AudioSource TeleportSound;

    private void Start()
    {
        otherPortal = exitPortal.GetComponent<Portal>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport || !other.gameObject.CompareTag("Ball")) return;
        
        Vector2 exitDirection = exitPortal.transform.right * (isBlue ? 1 : -1); 
        other.transform.position = exitPortal.transform.position + (Vector3)exitDirection; 
        other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.magnitude * exitDirection; 

        TeleportSound.Play();
            
        canTeleport = false;
        otherPortal.canTeleport = false;
            
        StartCoroutine(TeleportCooldown());
    }

    private IEnumerator TeleportCooldown()
    {
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
        otherPortal.canTeleport = true;
    }
}
