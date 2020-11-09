using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnZombie : MonoBehaviour
{
    public Transform SpawnPosition;
    public GameObject m_Zombie;
    public float m_spawnSpeed = .01f;
    private float m_spawnTimer;
    public float m_amount = 0;
    public float m_maxZombies = 30;
    public List<GameObject> zombieAmount = new List<GameObject>();
    private float hitTimer = 0.3f;
    private bool hit = false;

    private GameObject m_Player;
    private playerHealth playerScript;

    private pauseMenu menu;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = m_Player.GetComponent<playerHealth>();

        m_spawnTimer = m_spawnSpeed;

        menu = FindObjectOfType<pauseMenu>();
    }

    void Update()
    {
        if (menu.paused == false)
        {
            m_spawnTimer -= Time.deltaTime;

            if (m_spawnTimer <= 0)
            {
                if (m_amount <= m_maxZombies)
                {
                    m_spawnTimer = m_spawnSpeed;
                    GameObject zombie = Instantiate(m_Zombie, SpawnPosition);
                    m_amount++;
                    zombieAmount.Add(zombie);
                    playerScript.zombiesLeft++;
                }
            }
        }
    }
}
