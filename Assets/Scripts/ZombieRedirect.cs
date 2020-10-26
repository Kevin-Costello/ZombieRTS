using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRedirect : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isChasing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }

}
