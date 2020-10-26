using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveFire : MonoBehaviour
{
    //NOT WORKING

    private bool isTargeting = false;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider zombie)
    {
        if (zombie.gameObject.CompareTag("Enemy") && isTargeting == false)
        {
            isTargeting = true;
            UnitController unit = gameObject.GetComponentInParent<UnitController>();
            unit.currentTarget = zombie.transform;
        }
    }

    private void OnTriggerExit(Collider zombie)
    {
        if (zombie.gameObject.CompareTag("Enemy"))
        {
            isTargeting = false;
            UnitController unit = gameObject.GetComponentInParent<UnitController>();
            unit.currentTarget = null;

        }
    }
}
