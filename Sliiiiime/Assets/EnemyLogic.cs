using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    // Start is called before the first frame update


    public AudioClip shootSound;
    public float projectileSpeed = 5f;
    public float lifeTime = 20;
    public float moveSpeed = 3f;
    public GameObject projectile;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    public int lives;
    public float respawnTime;
    public bool isAlive = true;
    public AudioClip takeDamageSound;
    public AudioClip respawnSound;

    private Vector2 targetPosition;

    private float fireRepeatTime = 3f;
    private float fireTime;
    private float shootCooldown = 2f;
    private float shootTimer;
    private float jumpTime = 5;
    private float health = 100;
    private int rand;

    void Awake()
    {
        fireTime = Time.time + fireRepeatTime;
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");

        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > fireTime)
        {
            GameManager.Instance.PlaySound(shootSound);

            rand = Random.Range(1, 3);
            if (rand == 1)
            {
                if (player3 != null)
                {
                    targetPosition = player1.transform.position;
                }
            }
            if (rand == 2)
            {
                if (player3 != null)
                {
                    targetPosition = player2.transform.position;
                }
            }
            if (rand == 3)
            {
                if (player3 != null)
                {
                    targetPosition = player3.transform.position;

                }
            }
            shootTimer -= Time.deltaTime;
            ShootProjectile(targetPosition);
            shootTimer = shootCooldown;

            fireTime = Time.time + fireRepeatTime;
        }


        if (targetPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void ShootProjectile(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        Vector3 projectileForce = direction * projectileSpeed;
        var bla = Mathf.Atan2(direction.y, direction.x);
        var withOffset = new Vector2(transform.position.x, transform.position.y) + direction * 0.5f;

        var spawnedProjectile = Instantiate(projectile, new Vector3(withOffset.x, withOffset.y, 0), Quaternion.Euler(direction));
        spawnedProjectile.transform.Rotate(0, 0, bla / Mathf.PI * 180 + 90);
        spawnedProjectile.transform.localScale = gameObject.transform.localScale * (0.1f);
        spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spawnedProjectile.GetComponent<Collider2D>());
        spawnedProjectile.GetComponent<Projectile>().playerOwner = this.gameObject;
        spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        Destroy(spawnedProjectile, 10);
        fireTime += Time.time + fireRepeatTime;
    }

    private void Update()
    {
        if (!IsInCameraView())
        {
            Destroy(gameObject);
        }
    }

    bool IsInCameraView()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x >= -0.2f && viewPos.x <= 1.2f && viewPos.y >= -0.2f && viewPos.y <= 1.2f)
        {
            return true;
        }
        return false;
    }
    public void TakeDamage(float amount)
    {
        transform.localScale -= new Vector3(amount, amount, amount);
        GameManager.Instance.PlaySound(takeDamageSound, 0.8f);
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void DealtDamage(float amount)
    {
        transform.localScale += new Vector3(amount, amount, amount);
    }

}
