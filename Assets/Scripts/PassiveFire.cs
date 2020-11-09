using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveFire : MonoBehaviour
{
    //NOT WORKING

    //private bool isTargeting = false;
    float distance;
    float nearestDistance = 100000;
    GameObject nearestTarget;
    private void Update()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, 10);
        UnitController unitController = gameObject.GetComponent<UnitController>();
        foreach (var hitCollider in objectsInRadius)
        {
            if(unitController.isMoving == false)
            {
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    distance = (transform.position - hitCollider.transform.position).sqrMagnitude;
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = hitCollider.gameObject;
                        unitController.currentTarget = nearestTarget.transform;
                    }

                }
            }
        }

        if(unitController.currentTarget == null)
        {
            nearestDistance = 100000;
        }
    }

    /*
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
    */
}
