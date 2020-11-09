using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BarrelScript : MonoBehaviour
{
    private Transform child;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet"))
        {
            child = gameObject.transform.GetChild(0);
            child.gameObject.SetActive(true);
            child.transform.parent = null;
            Destroy(gameObject);
            CheckCollisions();
        }
    }

    private void CheckCollisions()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, 8);
        ZombieController zombieController = gameObject.GetComponent<ZombieController>();
        foreach (var hitCollider in objectsInRadius)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Destroy(hitCollider.gameObject);
            }

            if (hitCollider.gameObject.CompareTag("PlayerUnit"))
            {

                UnitSelector getSelector = GameObject.Find("Manager").GetComponent<UnitSelector>();
                getSelector.IsKilled(hitCollider.gameObject);
                Destroy(hitCollider.gameObject);
            }
        }

    }
}
