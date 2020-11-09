using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieMovement : MonoBehaviour
{
    private GameObject m_Player;
    private playerHealth playerScript;
    private UnityEngine.AI.NavMeshAgent m_NavAgent;
    private Rigidbody m_Rigidbody;
    private bool m_Follow;
    public float m_zombieHealth = 100;
    public float m_damageAr15 = 15;
    public int zombieDamage = 10;
    public Material red;
    private Material original;
    private Renderer[] Renderers;
    private float hitTimer = 0.3f;
    public bool hit = false;

    private pauseMenu menu;
    //public spawnZombie spawner;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = m_Player.GetComponent<playerHealth>();
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = true;
        //spawner = GameObject.FindObjectOfType<spawnZombie>();

        menu = FindObjectOfType<pauseMenu>();
        Renderers = GetComponentsInChildren<Renderer>();
        original = Renderers[0].material;
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.paused == false)
        {
            if (m_Follow == true)
            {
                m_NavAgent.SetDestination(m_Player.transform.position);
            }
        }
        else
        {
            m_NavAgent.SetDestination(transform.position);
        }
        if (hit == true)
        {
            hitTimer -= Time.deltaTime;
        }
        if (hitTimer <= 0)
        {
            foreach (Renderer r in Renderers)
            {
                r.material = original;
            }
            hit = false;
            hitTimer = 0.3f;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ar15")
        {
            m_zombieHealth -= m_damageAr15;
            foreach(Renderer r in Renderers)
            {
                r.material = red;
            }
        }

        if(m_zombieHealth <= 0)
        {
            //spawner.zombieAmount.Remove(this.gameObject);
            Destroy(this.gameObject);
            playerScript.zombiesLeft--;
        }
    }

}
