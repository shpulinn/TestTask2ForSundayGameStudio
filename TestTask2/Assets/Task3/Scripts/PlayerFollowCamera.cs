using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void LateUpdate ()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z) + offset;
    }
}
