using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI health;
    public TextMeshProUGUI leftZombies;
    public float timer = 0;
    public int zombiesLeft = 0;
    public float healthBarMax;
    public float healthBarMin;
    public Transform healthBar;
    public GameObject deathScreen;
    public GameObject VictoryScreen;
    private pauseMenu menu;
    private bool Dead;
    private float DeathTimer = 3f;
    public bool Victory = false;
    public float TimerToAvoidTheGameBreaking = 5f;

    void Start()
    {
        VictoryScreen.SetActive(false);
        deathScreen.SetActive(false);
        currentHealth = maxHealth;
        health.text = currentHealth.ToString() + "%";
        menu = FindObjectOfType<pauseMenu>();
    }

    private void OnTriggerStay(Collider other)
    {
        zombieMovement zombie = other.GetComponent<zombieMovement>();
        if (zombie && menu.paused == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                currentHealth -= zombie.zombieDamage;
                timer = 1;
                health.text = currentHealth.ToString() + "%";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(menu.paused == false)
        {
            leftZombies.text = "Zombies left: " + zombiesLeft.ToString();
            healthBar.localScale = new Vector3((float)(currentHealth / 10), 1, 1);
            if (currentHealth <= 0 )
            {
                Dead = true;
                if (Dead == true)
                {
                    deathScreen.SetActive(true);
                    menu.paused = true;
                }
            }

            TimerToAvoidTheGameBreaking -= Time.deltaTime;
            if (TimerToAvoidTheGameBreaking <= 0)
            {
                if (zombiesLeft <= 0)
                {
                    Victory = true;
                    if (Victory == true)
                    {
                        VictoryScreen.SetActive(true);
                        menu.paused = true;
                    }
                }
            }

        }
        if(Dead == true || Victory == true)
        {
            DeathTimer -= Time.deltaTime;
            if(DeathTimer <= 0)
            {
                SceneManager.LoadScene(0);
                menu.play.m_MouseLook.SetCursorLock(false);
            }
        }
    }
}
