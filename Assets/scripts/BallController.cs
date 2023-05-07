using UnityEngine;

public class BallController : MonoBehaviour
{
  private Vector2 velocity;
  private Rigidbody2D rb;
  private float size = 9.24f;
  private Vector2 dragStartPos;
  private bool isDragging = false;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      isDragging = true;
      dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      rb.velocity = Vector2.zero;
    }

    if (Input.GetMouseButtonUp(0) && isDragging)
    {
      Debug.Log("dragStartPos: " + dragStartPos);
      isDragging = false;
      Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 direction = dragEndPos - dragStartPos;

      rb.AddForce(new Vector2(10f, 10f), ForceMode2D.Impulse);
    }

    // Check if the ball is moving
    if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.02f)
    {
      // Calculate growth rate based on velocity
      size += velocity.magnitude * 0.001f;
    }
    else
    {
      // Set growth rate to zero if the ball is not moving
      size += 0f;
    }
    transform.localScale = new Vector3(size, size, 1f);
  }

  void FixedUpdate()
  {
    GetComponent<Rigidbody2D>().velocity = velocity;
  }
}
