using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraComponent : MonoBehaviour
{
    private Camera camera;

    private Vector3 rightDirection = Vector3.right;
    private Vector3 leftDirection = Vector3.left;
    private Vector3 upDirection = Vector3.up;
    private Vector3 downDirection = Vector3.down;

    [SerializeField] private float mouseSpeed;

    private float delta = 10.0f;

    private void Awake()
    {
        camera = this.gameObject.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (camera.orthographicSize > 3f)
                camera.orthographicSize -= 1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (camera.orthographicSize < 30f)
                camera.orthographicSize += 1f;
        }
        // else if (Input.mousePosition.x >= Screen.width - delta)
        // {
        //     transform.position += rightDirection * Time.deltaTime * mouseSpeed;
        // }
        // else if (Input.mousePosition.x <= delta)
        // {
        //     transform.position += leftDirection * Time.deltaTime * mouseSpeed;
        // }
        // else if (Input.mousePosition.y >= Screen.height + -delta)
        // {
        //     transform.position += upDirection * Time.deltaTime * mouseSpeed;
        // }
        // else if (Input.mousePosition.y <= delta)
        // {
        //     transform.position += downDirection * Time.deltaTime * mouseSpeed;
        // }
    }


    private Vector3 MouseDirection(Vector3 mousePos)
    {
        Vector2 direction = (camera.WorldToScreenPoint(this.transform.position) - mousePos).normalized;

        return direction;
    }

    
}
