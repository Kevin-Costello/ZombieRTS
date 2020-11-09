using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRedirect : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isChasing = false;

    float distance;
    float nearestDistance = 100000;
    GameObject nearestTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
    }
    /*
    private void OnCollisionStay(Collision unit)
    {
        if (unit.gameObject.CompareTag("PlayerUnit") && isChasing == false)
        {
            isChasing = true;
            ZombieController zombie = gameObject.GetComponentInParent<ZombieController>();
            zombie.currentTarget = unit.transform;
        }
    }

    private void OnCollisionExit(Collision unit)
    {
        if(unit.gameObject.CompareTag("PlayerUnit"))
        {
            isChasing = false;
            GameObject defaultTarget = GameObject.FindGameObjectWithTag("Baby");
            ZombieController zombie = gameObject.GetComponentInParent<ZombieController>();
            zombie.currentTarget = defaultTarget.transform;
            
        }
    }\
    */
    private void CheckCollisions()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, 10);
        ZombieController zombieController = gameObject.GetComponent<ZombieController>();
        foreach (var hitCollider in objectsInRadius)
        {
            if (hitCollider.gameObject.CompareTag("PlayerUnit"))
            {
                distance = (transform.position - hitCollider.transform.position).sqrMagnitude;
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = hitCollider.gameObject;
                    zombieController.currentTarget = nearestTarget.transform;
                }

            }
        }
        
        if (zombieController.currentTarget == null)
        {
            nearestDistance = 100000;
        }
    }
}
