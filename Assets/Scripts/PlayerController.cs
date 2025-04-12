using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    Vector2 input;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    // using this instead of update because its more reliable than controlling physics based on frame rate
    void FixedUpdate() {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
}
