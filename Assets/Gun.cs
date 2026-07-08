using UnityEngine;

public class Gun : MonoBehaviour
{
    public Shooter shooterScript;

    public LogicScript logic;

    public GameObject Bullet;

    private bool lastPos;

    protected float gunShootSpeed = 50;

    public Transform FirePoint;

    private float timer;

    private float fireDelay = 0.5f;

    private int totalAmmo = 6;

    private int currentAmmo = 6;

    private bool isReloading = false;

    public GameObject muzzleFlash; 

    public float flashDuration = 0.05f;

    public float flashScale = 0.4f;

    [SerializeField] private AudioClip[] gunShootSFX;

    [SerializeField] private AudioClip[] reloadSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos = shooterScript.isRight;
        timer = fireDelay;

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        logic.updateAmmo(totalAmmo, currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {   
        // Setting bullet direction
        if (shooterScript.isRight != lastPos)
        {
            lastPos = shooterScript.isRight;
            gunShootSpeed = gunShootSpeed * -1;
        }

        if((currentAmmo < 1 || Input.GetKeyDown(KeyCode.R)) && !isReloading)
        {
            StartCoroutine(ReloadRoutine());
        }

        // Shooting gun upon pressing space and adhering to fireRate
        if (Input.GetKeyDown(KeyCode.Space) && timer >= fireDelay && currentAmmo > 0 && !isReloading)
        {
            spawnBullet();
            Shoot();
            currentAmmo -= 1;
            logic.updateAmmo(totalAmmo, currentAmmo);
            SoundFXManager.instance.PlayRandSoundFXClip(gunShootSFX, transform, 0.2f);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }


    void spawnBullet()
    {
        GameObject newBullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // 2. Flip the bullet itself if we are facing left
            if (!shooterScript.isRight)
            {
                newBullet.transform.localScale = new Vector3(newBullet.transform.localScale.x * -1, newBullet.transform.localScale.y, newBullet.transform.localScale.y);
            }

            bulletScript.InitializeBullet(this, shooterScript, gunShootSpeed);
        }

        Destroy(newBullet, 1f);
    }

    private System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true; // Lock the reload so Update() doesn't call this again next frame

        // 1. Tell your Sound Manager to play the sequential audio clips
        SoundFXManager.instance.PlaySeqSoundFXClips(reloadSFX, transform, 0.4f);

        // 2. Calculate how long the total sequence will last
        float totalWaitTime = GetTotalClipLength(reloadSFX);

        // 3. WAIT right here until the audio finishes playing
        yield return new WaitForSeconds(totalWaitTime);

        // 4. This code ONLY runs AFTER the wait time is complete!
        currentAmmo = 6;
        logic.updateAmmo(totalAmmo, currentAmmo);

        isReloading = false; // Unlock reload
    }

    // Helper helper function to calculate the combined length of your audio array
    private float GetTotalClipLength(AudioClip[] clips)
    {
        float total = 0f;
        foreach (AudioClip clip in clips)
        {
            if (clip != null) total += clip.length;
        }
        return total;
    }

    void Shoot()
    {
        // ... Your existing shooting logic (instantiating bullet, playing sound, etc.) ...

        // Trigger the muzzle flash
        StartCoroutine(TriggerMuzzleFlash());
    }

    private System.Collections.IEnumerator TriggerMuzzleFlash()
    {
        // 1. Turn the flash on
        muzzleFlash.SetActive(true);

        float flipX = (Random.Range(0, 2) == 0 ? 1 : -1) * flashScale;
        float flipY = (Random.Range(0, 2) == 0 ? 1 : -1) * flashScale;

        // 2. Randomly flip or rotate the flash slightly so it doesn't look identical every shot
        muzzleFlash.transform.localScale = new Vector3(flipX, flipY, 1f);

        // 3. Wait for a fraction of a second
        yield return new WaitForSeconds(flashDuration);

        // 4. Turn the flash off
        muzzleFlash.SetActive(false);
    }
}
