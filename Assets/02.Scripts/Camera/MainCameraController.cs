using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector3 minCameraBoundary;
    [SerializeField] Vector3 maxCameraBoundary;
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.z = Mathf.Clamp(targetPos.z, minCameraBoundary.z, maxCameraBoundary.z);


        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}