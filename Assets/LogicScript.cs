using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text kills;
    public Text highScore;
    public Text health;
    public Text ammo;
    [SerializeField] private int totalhealth = 100;
    public int totalKills = 0;
    public int totalHighScore = 0;
    public GameObject gameOverobj;
    public AudioSource bgmSource;
    [SerializeField] private AudioClip gameOverSFX;

    void Awake()
    {
        totalHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScore != null)
        {
            highScore.text = $"Highest Kills: {totalHighScore}";
        }
    }
    public void addKills(int mykills)
    {
        totalKills += mykills;
        kills.text = totalKills.ToString();
    }

    public void depleteHealth(int myHealth)
    {
        totalhealth = totalhealth - myHealth < 0? 0 : totalhealth - myHealth;
        health.text = totalhealth.ToString();
    }

    public void Heal(int myHealth)
    {
        if (totalhealth < 100)
        {
            totalhealth = totalhealth + myHealth;
            totalhealth = totalhealth > 100? 100 : totalhealth;
            health.text = totalhealth.ToString();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (totalKills > totalHighScore)
        {
            PlayerPrefs.SetInt("HighScore", totalKills);
        }
        gameOverobj.SetActive(true);
        bgmSource.Stop();

    }

    public void updateAmmo(int totalAmmo, int currentAmmo)
    {
        ammo.text = $"Ammo = {currentAmmo} / {totalAmmo}";
    }

}
