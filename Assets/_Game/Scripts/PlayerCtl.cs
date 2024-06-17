using UnityEditor;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;
    private float horizontal;
    private float vertical;
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
    }
    
}
