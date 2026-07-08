using System;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float zombieHealth = 100;
    public MoveTowards movementScript;   
    public Animator animator;
    public LogicScript logic;
    public ZombieSpawner zSpawner;
    public Rigidbody2D rb;
    private float timer;
    private AudioSource currentZombieSound;
    [SerializeField] private AudioClip[] zombieSFX;
    [SerializeField] private AudioClip[] zombieDeathSFX;
    [SerializeField] private AudioClip[] bulletHitSFX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = UnityEngine.Random.Range(0f,3f);
        //Get rigidbody to turn zombie to static upon death
        rb = GetComponent<Rigidbody2D>();

        // Get Movetowards script on spawn for stopping movement on death
        movementScript = GetComponent<MoveTowards>();

        //Get logic script to update kill count
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        zSpawner = GameObject.FindGameObjectWithTag("ZombieSpawner").GetComponent<ZombieSpawner>();

        //Get player object for the target object of movetowards script
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            movementScript.targetObj = player.transform;
        }
        else
        {
            Debug.LogWarning("Zombie couldn't find an object with the 'Player' tag!");
        }

        movementScript.moveSpeed = zSpawner.zombieSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!logic.gameOverobj.activeSelf) 
        {

            // play zombie groans
            if (timer < 4f)
            {
                timer += Time.deltaTime;
            }
            else 
            { 
                //play zombie sounds
                currentZombieSound = SoundFXManager.instance.PlayRandSoundFXClip(zombieSFX, transform, 1f);
                timer = 0f;
            }

            // kill zombie processes
            if (zombieHealth <= 0 && movementScript.alive)
            {
                if (currentZombieSound != null)
                {
                    Destroy(currentZombieSound.gameObject);
                }

                SoundFXManager.instance.PlayRandSoundFXClip(zombieDeathSFX, transform, 0.5f);
                //Stop Zombie Movement upon death
                movementScript.alive = false;
                GetComponent<Collider2D>().isTrigger = true;
                rb.bodyType = RigidbodyType2D.Static;
                killZombie();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            zombieHealth -= 40;
            SoundFXManager.instance.PlayRandSoundFXClip(bulletHitSFX, transform, 0.2f);
        }
    }

    private void killZombie()
    {
        animator.SetBool("zombieShot", true);
        if (animator.GetBool("zombieShot"))
        {
            Destroy(gameObject, 2f);
            logic.addKills(1);
            Debug.Log("Zombie Destroyed");
        }
    }
}
