using UnityEngine;
using System.Collections;

public class BallLogic : MonoBehaviour
{
    
    public static BallLogic Instance;

    public float speed = 11f;
    public float respawnDelay = 1f;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector3 startPosition;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPosition = transform.position; // remember where it starts (e.g. just above paddle)
        StartCoroutine(StartLevelSequence());
    }
    void Update()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    void LaunchBall()
    {
        rb.linearVelocity = new Vector2(speed, speed);
    }

    public void ResetBall()
    {
        // Immediately stop and hide the ball
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        sr.enabled = false; // hide it during the delay

        StartCoroutine(RespawnAfterDelay());
    }
    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        sr.enabled = true;
        LaunchBall();
    }
    IEnumerator StartLevelSequence()
    {
        yield return StartCoroutine(UIManager.Instance.ShowLevelIntro(GameManager.CurrentLevelNumber));
        LaunchBall();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Paddle"))
        {
            Transform paddle = collision.collider.transform;

            // How far from paddle center did the ball hit? Range roughly -1 to 1
            float paddleWidth = collision.collider.bounds.size.x;
            float hitFactor = (transform.position.x - paddle.position.x) / (paddleWidth / 2f);
            hitFactor = Mathf.Clamp(hitFactor, -1f, 1f);

            // New direction: x based on hit position, y always upward
            Vector2 newDirection = new Vector2(hitFactor * 0.8f, 1f).normalized;
            rb.linearVelocity = newDirection * speed;
        }
    }
}

