using UnityEngine;

public class FollowMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    public Vector3 offset;
     
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.fixedDeltaTime * speed);
    }
}
