using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public GameObject bloodEffect;
    public int attackRange = 10;
    public bool isSelected = false;
    public bool isDead = false;
    public bool isMoving = false;
    public GameObject bullet;
    public int health = 3;
    public float damageTimeout = 1.0f;


    public Transform currentTarget;
    private bool canTakeDamage = true;
    private float timeToFire = 0;
   

    private void Update()
    {
        if (currentTarget != null)
        {
            navAgent.destination = currentTarget.position;
            AttackTarget();
        }

        if(health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
            UnitSelector getSelector = GameObject.Find("Manager").GetComponent<UnitSelector>();
            getSelector.IsKilled(gameObject);
            
        }

        if(navAgent.velocity == Vector3.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    
    }

    public void MoveUnit(Vector3 destination)
    {
        currentTarget = null;
        navAgent.destination = destination;

    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }

    public void SetTarget(Transform enemy)
    {
        currentTarget = enemy;
    }

    private void AttackTarget()
    {
        float targetDistance = Vector3.Distance(currentTarget.position, transform.position);
        //Stop the unit when within firing range
        if (targetDistance <= attackRange)
        {
            //Rotate to face the enemy 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(currentTarget.position - transform.position), .5f);
            navAgent.destination = transform.position;

            
            
            if (Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / bullet.GetComponent<ProjectileScript>().fireRate;
                Instantiate(bullet, transform.position, Quaternion.RotateTowards(transform.rotation, currentTarget.rotation, 1));

            }
        }
    }

    private IEnumerator TakeDamage()
    {
        health -= 1;
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
;            if (canTakeDamage == true)
            {
                StartCoroutine(TakeDamage());
            }
        }
        
    }

}
