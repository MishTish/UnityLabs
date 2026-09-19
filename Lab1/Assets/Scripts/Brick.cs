using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum BrickType { Regular, Sturdy, Bonus }
    public BrickType type;

    private int hitsRequired = 1;
    private int currentHits;
    private int scoreValue = 10;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        switch (type)
        {
            case BrickType.Regular:
                hitsRequired = 1;
                scoreValue = 10;
                break;
            case BrickType.Sturdy:
                hitsRequired = 2;
                scoreValue = 20;
                break;
            case BrickType.Bonus:
                hitsRequired = 1;
                scoreValue = 50;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;

        currentHits++;

        if (currentHits >= hitsRequired)
        {
            GameManager.Instance.AddScore(scoreValue);
            GameManager.Instance.BrickDestroyed();
            Destroy(gameObject);
        }
        else
        {
            // Visual feedback for sturdy bricks taking damage
            float damagePercent = (float)currentHits / hitsRequired;
            sr.color = Color.Lerp(Color.white, Color.gray, damagePercent);
        }
    }
}