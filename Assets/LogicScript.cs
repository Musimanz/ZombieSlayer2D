using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text kills;
    public Text health;
    public Text ammo;
    [SerializeField] private int totalhealth = 100;
    public int totalKills;
    public GameObject gameOverobj;
    public AudioSource bgmSource;

    public void addKills(int mykills)
    {
        totalKills += mykills;
        kills.text = totalKills.ToString();
    }

    public void depleteHealth(int myHealth)
    {
        totalhealth = totalhealth - myHealth;
        health.text = totalhealth.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {

        gameOverobj.SetActive(true);
        bgmSource.Stop();

    }

    public void updateAmmo(int totalAmmo, int currentAmmo)
    {
        ammo.text = $"Ammo = {currentAmmo} / {totalAmmo}";
    }

}
