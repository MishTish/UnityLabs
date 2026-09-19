using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    public float speed = 8f;
    public float screenLimit = 4f; 

    void Update()
    {
        if (!GameManager.Instance.canMove)
        {
            return;
        }
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        transform.position += new Vector3(move, 0f, 0f);
        
        float clampedX = Mathf.Clamp(transform.position.x, -screenLimit, screenLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}