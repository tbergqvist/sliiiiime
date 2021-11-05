using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public string inputAxisSuffix; 
    public float speed = 100f;
    public float jumpHeight = 250f;

    bool isJumping = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(speed, 0) * Time.deltaTime * Input.GetAxis("Horizontal" + inputAxisSuffix);
        if (Input.GetAxis("Vertical" + inputAxisSuffix) > 0.8f && !isJumping)
        {
            Jump();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isJumping)
        {
            rb.AddForce(new Vector2(0, Random.Range(2f, 5f)), ForceMode2D.Impulse);
            isJumping = false;
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight));
        isJumping = true;
    }
}
