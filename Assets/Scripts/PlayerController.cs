using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    Vector2 input;
    public Rigidbody2D rb;
    public Animator anim;

    public Transform playerTransform;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        playerTransform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);
        anim.SetFloat("Speed", input.sqrMagnitude);
    }

    // using this instead of update because its more reliable than controlling physics based on frame rate
    void FixedUpdate() {
        rb.MovePosition(rb.position + input.normalized * speed * Time.fixedDeltaTime);
    }
}
