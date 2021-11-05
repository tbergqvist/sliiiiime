using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public string inputAxisSuffix;
    public float speed = 100f;
    public float jumpHeight = 250f;
    public float wallCheckRange;

    bool isJumping = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal" + inputAxisSuffix);
        if (horizontalMove != 0)
        {
            TryMove(horizontalMove);
        }
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
        if (other.gameObject.CompareTag("Speeddrop"))
        {
            Destroy(other.gameObject);
            speed += 25;
            Invoke("decreaseSpeed", 7);
        }
        if (other.gameObject.CompareTag("Healthdrop"))
        {
            Destroy(other.gameObject);
            gameObject.transform.localScale *= 1.1f;
        }
    }

    void decreaseSpeed()
    {
        speed -= 25;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight));
        isJumping = true;
    }
    void TryMove(float horizontalMove)
    {
        Vector3 moveVector = new Vector3(speed, 0) * Time.deltaTime * horizontalMove;
        if (!WillWalkIntoWall(moveVector.normalized))
        {
            gameObject.transform.position += moveVector;
        }
    }
    bool WillWalkIntoWall(Vector3 moveVector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, wallCheckRange * transform.localScale.x);
        if(hit)
        {
            if (hit.transform.TryGetComponent<Platform>(out _))
            {
                return true;
            }
        }
        return false;
    }



}
