using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    bool isAlt;
    Vector2 clickPoint;
    float dragSpeed = 30.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) isAlt = true;
        if (Input.GetMouseButtonUp(0)) isAlt = false;

        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            if (isAlt)
            {
                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
                Debug.Log(clickPoint);

                position.z = position.y;
                position.y = .0f;

                Vector3 move = position * (Time.deltaTime * dragSpeed);

                float y = transform.position.y;

                transform.Translate(move);
                transform.position 
                    = new Vector3(transform.position.x, y, transform.position.z);
                //if (transform.position.z <= -18)
                //{
                //    isAlt = false;
                //}
            }
        }
    }
}