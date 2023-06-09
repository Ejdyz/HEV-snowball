using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallController : MonoBehaviour
{
  private Rigidbody2D rb;

  public float launchPower = 10f;
  public float sizeIncreaseRate = 0.1f;
  public float scaleTooBig = 40f;
  public float decelerationRate = 0.5f;
  public float minimumSpeed = 0.1f;
  public float maximumSpeed = 50f;
  
  private Vector3 startPosition;
  private Vector3 endPosition;
  private bool isDragging = false;
  private bool isLaunched = false;

  [SerializeField] AudioSource CollisionSound;
  [SerializeField] AudioSource BounceSound;
  [SerializeField] AudioSource WinSound;
  [SerializeField] int NextLevelSceneId;
  
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    rb.velocity = Vector3.zero;
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(1))
    {
      startPosition = Input.mousePosition;
      isDragging = true;
    }

    if (Input.GetMouseButtonUp(1) && isDragging)
    {
      endPosition = Input.mousePosition;
      isDragging = false;
      LaunchBall();
    }

    if (isLaunched && rb.velocity != Vector2.zero)
    {
      GrowBall();
      Vector2 deceleration = rb.velocity * (decelerationRate * Time.deltaTime);
      rb.velocity -= deceleration;
    }

    if (rb.velocity.magnitude < minimumSpeed)
    {
      rb.velocity = Vector2.zero;
    }

    if (rb.velocity.magnitude > maximumSpeed)
    {
      rb.velocity = rb.velocity.normalized * maximumSpeed;
    }
    
    CheckSize();
  }

  void LaunchBall()
  {
    Vector3 direction = (startPosition - endPosition).normalized;
    float distance = Vector3.Distance(startPosition, endPosition);
    Vector2 force = new Vector2(direction.x * distance * launchPower, direction.y * distance * launchPower);

    rb.AddForce(force, ForceMode2D.Impulse);
    isLaunched = true;
  }

  void GrowBall()
  {
    float ballSpeed = rb.velocity.magnitude;
    float growthFactor = sizeIncreaseRate * ballSpeed * Time.deltaTime;
    rb.transform.localScale += new Vector3(growthFactor, growthFactor, 0);
  }

  void ShrinkBall()
  {
    rb.transform.localScale /= 2;
  }
  void CheckSize()
  {
    if (rb.transform.localScale.y >= scaleTooBig)
    {
      SceneManager.LoadScene(8);
      //end screen
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Bouncer"))
    {
      ShrinkBall();
      BounceSound.Play();
      // Play wall hit sound effect 

    }
    if (collision.gameObject.CompareTag("Tree"))
    {
      CollisionSound.Play();
      // Play tree hit sound effect
      //start animation of tree
    }

    if (collision.gameObject.CompareTag("Fence"))
    {
      CollisionSound.Play();
      // Play wall hit sound effect
    }

    if (!collision.gameObject.CompareTag("Finish")) return;
    if (!(rb.transform.localScale.y <= 25f)) return;
    
    StartCoroutine(changeSceneWithDelay(1, NextLevelSceneId));
    WinSound.Play();
  }
  
  IEnumerator changeSceneWithDelay(int delayInSeconds, int SceneId)
  {
    yield return new WaitForSeconds(delayInSeconds);
    SceneManager.LoadScene(SceneId);
  }
}
