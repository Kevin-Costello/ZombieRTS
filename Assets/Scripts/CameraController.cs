using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float moveSpeed = 0.02f;
    private float zoomSpeed = 10.0f;
    private float rotateSpeed = 0.05f;

    private float maxZoomIn = 4f;
    private float maxZoomOut = 40f;

    private Vector2 p1;
    private Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = transform.position.y * moveSpeed * Input.GetAxis("Vertical");
        float verticalSpeed = -transform.position.y * moveSpeed * Input.GetAxis("Horizontal");
        float scroll = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
        
        if((transform.position.y >= maxZoomOut) && (scroll > 0))
        {
            scroll = 0;
        }
        else if((transform.position.y <= maxZoomIn) && (scroll < 0))
        {
            scroll = 0;
        }

        if ((transform.position.y + scroll) > maxZoomOut)
        {
            scroll = maxZoomOut - transform.position.y;
        }
        else if((transform.position.y + scroll) < maxZoomIn)
        {
            scroll = maxZoomIn - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scroll, 0);
        Vector3 lateralMove = horizontalSpeed * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= verticalSpeed;

        Vector3 move = verticalMove + lateralMove + forwardMove;
        transform.position += move;

        getCameraRotation();
    }

    void getCameraRotation()
    {
        if(Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            p1 = p2;
        }
    }
}
