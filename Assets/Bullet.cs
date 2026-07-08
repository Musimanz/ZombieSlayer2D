using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Shooter bulletShooter;
    public Gun myGun;
    public float shootSpeed; 

    private bool isReady = false;

    public void InitializeBullet(Gun gun, Shooter shooter, float speed)
    {
        myGun = gun;
        bulletShooter = shooter;
        shootSpeed = speed;
        isReady = true; // Unlock movement loop safely
    }

    void Update()
    {
        // Don't execute anything until the Gun explicitly sets our data
        if (!isReady) return;

        shootBullet();
    }

    void shootBullet()
    {
        transform.position = new Vector3((transform.position.x + shootSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
