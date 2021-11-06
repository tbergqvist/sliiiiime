using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public string inputAxisSuffix;
    public float speed = 100f;
    public float jumpHeight = 250f;
    public float wallCheckRange;
    public float dashRange;
    public float dashDuration;
    public float dashCooldown;
    public Vector2 prevVel;
    public AudioClip bounceClip;
    public AudioClip jumpClip;

    private bool isDashReady = true;
    private bool isJumping = false;
    private bool isBouncing = false;
    Rigidbody2D rb;

    public GameObject explosion;

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
            if(Input.GetAxis("Dash" + inputAxisSuffix) > 0.8f && isDashReady)
            {
                StartCoroutine(Dash(horizontalMove));
            }
        }
        if (Input.GetAxis("Vertical" + inputAxisSuffix) > 0.8f && !isJumping)
        {
            Jump();
        }

        prevVel = rb.velocity;
    }
    private void stopBounce()
    {
        isBouncing = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
        {
            isJumping = false;
            if (!isBouncing)
            {
                rb.AddForce(-prevVel * 0.8f, ForceMode2D.Impulse);
                isBouncing = true;
                GameManager.Instance.PlaySound(bounceClip);
                Invoke("stopBounce", 0.2f);
            }
        }

        if (other.gameObject.CompareTag("Speeddrop"))
        {
            Destroy(other.gameObject);
            speed += 15;
            Invoke("decreaseSpeed", 7);
        }
        if (other.gameObject.CompareTag("Healthdrop"))
        {
            Destroy(other.gameObject);
            gameObject.transform.localScale += new Vector3(0.3f,0.3f,0.3f);
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(other.gameObject);
            gameObject.transform.localScale *= 0.8f;
            var exp = Instantiate(explosion, gameObject.transform.position, Quaternion.identity, null);
            Destroy(exp, 0.5f);
        }
    }

    void decreaseSpeed()
    {
        speed -= 15;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight));
        GameManager.Instance.PlaySound(jumpClip);
        isJumping = true;
    }
    void TryMove(float horizontalMove)
    {
        var actualSpeed = speed * (1 / Mathf.Max(Mathf.Min(transform.localScale.x, 1.5f), 0.5f));
        Vector3 moveVector = new Vector3(actualSpeed, 0) * Time.deltaTime * horizontalMove;
        if (!WillWalkIntoWall(moveVector.normalized))
        {
            gameObject.transform.position += moveVector;
        }
    }
    bool WillWalkIntoWall(Vector3 moveVector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, wallCheckRange * transform.localScale.x);
        if (hit)
        {
            if (hit.transform.TryGetComponent<Platform>(out _))
            {
                return true;
            }
        }
        return false;
    }
    void SetDashReady()
    {
        isDashReady = true;
    }
    IEnumerator Dash(float horizontalInput)
    {
        isDashReady = false;
        Invoke("SetDashReady", dashCooldown);
        float t = dashDuration;
        while (t > 0)
        {
            if (horizontalInput > 0)
            {
                transform.position += Vector3.right * Time.deltaTime * dashRange;
            }
            else
            {
                transform.position -= Vector3.right * Time.deltaTime * dashRange;
            }
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
