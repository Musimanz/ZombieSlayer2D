using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Transform targetObj;
    public float moveSpeed;
    public bool alive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj == null) return;

        float step = moveSpeed * Time.deltaTime;

        if (alive)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObj.position, step);

            HandleFacingDirection();
        }

    }

    void HandleFacingDirection()
    {
        // If the player is to the right of the zombie and the zombie is facing left
        if (targetObj.position.x < transform.position.x && transform.localScale.x > 0)
        {
            // Flip to face right (Assuming your default sprite faces left)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // If the player is to the left of the zombie and the zombie is facing right
        else if (targetObj.position.x > transform.position.x && transform.localScale.x < 0)
        {
            // Flip to face left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

