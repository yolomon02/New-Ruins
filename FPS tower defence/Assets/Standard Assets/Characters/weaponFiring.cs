using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weaponFiring : MonoBehaviour
{
    //fire systems
    public Rigidbody m_Bullet;
    public Transform m_FireTransform;
    public float m_LaunchForceAR = 200f;
    public float m_autoSpeed = 1f;
    private float m_bulletTimer;
    public float ARstrayFactor = .2f;
    public float ARstrayFactorAIM = .05f;

    //reload systems
    public TextMeshProUGUI BulletsLeft;
    public float reloadTimer;
    public float reloadTimerMax = 1.5f;
    public bool reload;
    public GameObject reloading;
    public float maxBulletsAR = 30;
    public float bulletsLeftAR = 5;

    //aim systems
    public Transform aiming;
    public Transform noAiming;
    public bool isAiming;

    private pauseMenu menu;


    void Start()
    {
        reloading.SetActive(false);
        reloadTimer = reloadTimerMax;
        m_bulletTimer = m_autoSpeed;
        BulletsLeft.text = "Ammo: " + bulletsLeftAR.ToString() + " / " + maxBulletsAR.ToString();

        menu = FindObjectOfType<pauseMenu>();
    }

    void Update()
    {
        if (menu.paused == false)
        {
            m_bulletTimer -= Time.deltaTime;
            if (bulletsLeftAR <= 10)
            {
                BulletsLeft.color = Color.yellow;
            }
            else
            {
                BulletsLeft.color = Color.white;
            }
            if (reload == false)
            {
                //fires on mouse down
                if (Input.GetButtonDown("Fire1") && bulletsLeftAR != 0)
                {
                    Fire();
                    bulletsLeftAR--;
                }
                //enables auto fire
                else if (Input.GetButton("Fire1") && bulletsLeftAR != 0)
                {
                    if (m_bulletTimer <= 0)
                    {
                        Fire();
                        m_bulletTimer = m_autoSpeed;
                        bulletsLeftAR--;
                        BulletsLeft.text = "Ammo: " + bulletsLeftAR.ToString() + " / " + maxBulletsAR.ToString();
                    }
                }
            }
            if ((Input.GetKeyDown(KeyCode.R) && bulletsLeftAR < maxBulletsAR) || bulletsLeftAR == 0)
            {
                reload = true;
            }
            if (reload == true)
            {
                reloading.SetActive(true);
                reloadTimer -= Time.deltaTime;
                if (reloadTimer <= 0)
                {
                    reload = false;
                    bulletsLeftAR = maxBulletsAR;
                    BulletsLeft.text = "Ammo: " + maxBulletsAR.ToString() + " / " + maxBulletsAR.ToString();
                    reloadTimer = reloadTimerMax;
                    reloading.SetActive(false);
                }
            }
            if (Input.GetMouseButton(1))
            {
                this.transform.SetPositionAndRotation(aiming.position, aiming.rotation);
                isAiming = true;
            }
            else
            {
                this.transform.SetPositionAndRotation(noAiming.position, noAiming.rotation);
                isAiming = false;
            }
        }
           
    }
    private void Fire()
    {
        float randomNumberX;
        float randomNumberY;
        float randomNumberZ;
        // determines the accuracy on the gun by creating floats
        if (isAiming)
        {
            randomNumberX = Random.Range(-ARstrayFactorAIM, ARstrayFactorAIM);
            randomNumberY = Random.Range(-ARstrayFactorAIM, ARstrayFactorAIM);
            randomNumberZ = Random.Range(-ARstrayFactorAIM, ARstrayFactorAIM);
        }
        else
        {
            randomNumberX = Random.Range(-ARstrayFactor, ARstrayFactor);
            randomNumberY = Random.Range(-ARstrayFactor, ARstrayFactor);
            randomNumberZ = Random.Range(-ARstrayFactor, ARstrayFactor);
        }

        // spawns the bullet
        Rigidbody bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, (m_FireTransform.rotation * m_Bullet.rotation)) as Rigidbody;

        // adds the floats together
        bulletInstance.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

        // fires the bullet
        bulletInstance.velocity = m_LaunchForceAR * bulletInstance.transform.forward;

    }
    
    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject);
    }
}
