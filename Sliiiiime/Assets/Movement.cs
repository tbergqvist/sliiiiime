using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player1;
    public float speed = 100f;
    public float jumpHeight = 250f;

    public bool isJumping = false;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey))
        {
            player1.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(rightKey))
        {
            player1.transform.position += new Vector3(speed, 0) * Time.deltaTime;
        }

        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            player1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight));
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isJumping)
        {
            player1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Random.Range(2f, 5f)), ForceMode2D.Impulse);
            isJumping = false;
        }
        
    }
}
