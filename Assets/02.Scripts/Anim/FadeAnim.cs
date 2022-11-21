using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCompleteEventArgs
{
    public GameObject targetObject;
    public Vector3 position;
    public Quaternion quaternion;
}


public class FadeAnim : MonoBehaviour
{
    public static event System.EventHandler<MoveCompleteEventArgs> EventHandler_CameraMoveTarget;
    public GameManager camera;
    private Transform targetObject;
    public Transform subTarget;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    public static bool IsActive = false;
    public float Zoomin = -5;

    private Bounds boundsData;
    private bool isBounds = false;

    private int PassCount = 0;

    private void Update()
    {
        if(IsActive)
        {
            Vector3 targetPosition;

            if(subTarget != null && PassCount == 0)
            {
                targetPosition = subTarget.transform.position;
                smoothTime = 0.1f;
            }
            else
            {
                if(!isBounds)
                {
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                }
                else
                {
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
                }
            }
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref velocity, smoothTime);
            camera.transform.LookAt(targetObject);
            if(Vector3.Distance(targetPosition,camera.transform.position) < 0.1f )
            {
                if(subTarget != null)
                {
                    if(targetPosition == subTarget.transform.position)
                    {
                        PassCount++;
                    }
                }
            }
        }
    }
}
