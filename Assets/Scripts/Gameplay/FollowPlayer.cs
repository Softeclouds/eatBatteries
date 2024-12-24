using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
}