using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieController : MonoBehaviour
{

    public NavMeshAgent navAgent;
    public Transform currentTarget;

    private Transform enemyInRange;

    public int health = 5;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(currentTarget == null)
        {
            GameObject defaultTarget = GameObject.FindGameObjectWithTag("Baby"); ;
            currentTarget = defaultTarget.transform;
        }

        if(enemyInRange == null)
        {
            navAgent.SetDestination(currentTarget.position);
        }
    }
}
