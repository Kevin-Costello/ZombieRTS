using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionScript : MonoBehaviour
{
    public GameObject[] ConstructablesPreview;
    public GameObject[] Constructables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if(Input.GetKeyDown("e") && GetComponentInParent<UnitController>().isSelected == true)
        {
            Debug.Log("e was pressed and builder was selected");
            ConstructionPreview();

        }

    }

    void ConstructionPreview()
    {
        GameObject newBuild = Instantiate(ConstructablesPreview[0], transform.position +  new Vector3(0f, -.2f, 2f), (Quaternion.identity * Quaternion.Euler(-90, Quaternion.identity.y, Quaternion.identity.z)));
        newBuild.transform.SetParent(gameObject.transform);
    }
}
