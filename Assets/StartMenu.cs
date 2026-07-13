using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject kills;
    public GameObject ammo;
    public GameObject health;
    public GameObject highscore;
    public AudioSource startmenutheme;
    public AudioSource bgm;

    void Awake()
    {
        startmenutheme.Play();
        kills.SetActive(false);
        ammo.SetActive(false);
        health.SetActive(false);

        // 1. Ensure time is paused so zombies don't move/spawn behind the menu
        Time.timeScale = 0f;

        // 2. Make sure the start menu is visible immediately on launch
        startMenuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        // 3. Turn off the start menu panel
        startMenuPanel.SetActive(false);
        kills.SetActive(true);
        ammo.SetActive(true);
        health.SetActive(true);
        startmenutheme.Stop();
        bgm.Play();

        // 4. Resume normal game time so gameplay starts!
        Time.timeScale = 1f;
    }
}