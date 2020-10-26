using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{

    RaycastHit hit;
    List<UnitController> selectedUnits = new List<UnitController>();
    
    bool isDragging = false;
    Vector3 mousePosition;
    public GameObject moveMarker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            //Create ray from camera to mouse position
            var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Get the hit data from the ray
            if (Physics.Raycast(cameraRay, out hit))
            {
                //Do stuff with the hit data
                if (hit.transform.tag == "PlayerUnit")
                {
                    SelectUnit(hit.transform.gameObject.GetComponent<UnitController>(), Input.GetKey(KeyCode.LeftShift));
                }
                else
                {
                    //DeselectUnits();
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(isDragging)
            {
                DeselectUnits();

                foreach (var selectableUnits in GameObject.FindGameObjectsWithTag("PlayerUnit"))
                {
                    if (IsWithinRect(selectableUnits.transform))
                    {
                        SelectUnit(selectableUnits.gameObject.GetComponent<UnitController>(), true);
                    }
                }
                isDragging = false;
            }
        }
        if(Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Get the hit data from the ray
            if (Physics.Raycast(cameraRay, out hit))
            {
                //Do stuff with the hit data
                if (hit.transform.CompareTag("Ground"))
                {
                    foreach (var selectableObject in selectedUnits)
                    {
                        selectableObject.MoveUnit(hit.point);
                        GameObject[] oldMarkers = GameObject.FindGameObjectsWithTag("MoveMarker");
                        foreach (GameObject marker in oldMarkers )
                        {
                            Destroy(marker);
                        }
                        Instantiate(moveMarker, hit.point, Quaternion.identity);
                    }
                }
                if (hit.transform.CompareTag("Enemy"))
                {
                    foreach (var selectableObject in selectedUnits)
                    {
                        selectableObject.SetTarget(hit.transform);
                    }
                }
            }
        }
    }

    private void SelectUnit(UnitController unit, bool multiSelect = false)
    {
        if(!multiSelect)
        {
            DeselectUnits();
        }
        
        selectedUnits.Add(unit);
        unit.SetSelected(true);

        if (unit.health == 0)
        {
            unit.SetSelected(false);
        }

    }

    private void DeselectUnits()
    {

        for(int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].SetSelected(false);
        }
        selectedUnits.Clear();
    }

    private void OnGUI()
    {
        if(isDragging)
        {
            var rectSelect = Utils.GetScreenRect(mousePosition, Input.mousePosition);
            Utils.DrawScreenRect(rectSelect, new Color(0.4f, 0.8f, 0.8f, 0.2f));
            Utils.DrawScreenRectBorder(rectSelect, 1, Color.gray);

        }

    }

    private bool IsWithinRect(Transform transform)
    {
        if(!isDragging)
        {
            return false;
        }

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
    }

    public void IsKilled(GameObject unit)
    {
        var test = unit.GetComponent<UnitController>();
        selectedUnits.Remove(test);
    }
}
