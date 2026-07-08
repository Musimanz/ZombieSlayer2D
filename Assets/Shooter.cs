using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    private float playerHealth = 100;
    private float moveSpeed = 10;
    public bool isRight = true;
    private bool lastPos;
    public Animator animator;
    private float timer = 0.4f;
    private float jumpSpeed = 20f;
    public LogicScript logic;
    private bool alive = true;
    private bool inAir = false;
    [SerializeField] private AudioClip zombieBiteSFX;
    [SerializeField] private AudioClip playerDeathSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        lastPos = isRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            if (alive)
            { 
                SoundFXManager.instance.PlaySoundFXClip(playerDeathSFX, transform, 1f);
                alive = false;
            }
            logic.gameOver();
            Destroy(gameObject, 2f);
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalking", true);
            transform.position += (Vector3.left * moveSpeed * Time.deltaTime);
            isRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isWalking", true);
            transform.position += (Vector3.right * moveSpeed * Time.deltaTime);
            isRight = true;
        }
        else
        {
            // Neither key is pressed -> Stop walking animation
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !inAir)
        {
            myRigidBody.linearVelocity = Vector2.up * jumpSpeed;
            inAir = true;
            //Debug.Log("InAir Set to true");
        }

        if (isRight != lastPos)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
            lastPos = isRight;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (timer < 0.4f)
            {
                timer += Time.deltaTime;
            }
            else if (playerHealth > 0)
            {
                playerHealth -= 20;
                StartCoroutine(BiteRoutine());
                logic.depleteHealth(20);
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            inAir = false;
            //Debug.Log("inAir set to false");
        }
    }

    private System.Collections.IEnumerator BiteRoutine()
    {
        // 1. Tell your Sound Manager to play the sequential audio clips
        SoundFXManager.instance.PlaySoundFXClip(zombieBiteSFX, transform, 0.4f);

        // 3. WAIT right here until the audio finishes playing
        yield return new WaitForSeconds(zombieBiteSFX.length);
    }
}
