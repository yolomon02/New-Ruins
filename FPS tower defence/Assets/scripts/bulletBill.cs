using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBill : MonoBehaviour
{
    private float timer = 10;
    public float m_MaxDamageBody = 20;
    public float m_MaxDamageHead = 40;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Zombie")
        {
            if(other.gameObject.GetComponent<zombieMovement>())
            {
                other.gameObject.GetComponent<zombieMovement>().hit = true;
            }
            else if(other.transform.parent.gameObject.GetComponent<zombieMovement>())
            {
                other.transform.parent.gameObject.GetComponent<zombieMovement>().hit = true;
            }

        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
