using UnityEngine;

public class LockCameraToPlayer : MonoBehaviour
{
    public Transform playerTransform;

    public void UpdateCameraTarget(Transform targetTransform)
    {
        playerTransform = targetTransform;
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
