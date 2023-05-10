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
        
        other.transform.position = exitPortal.transform.position; 

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
