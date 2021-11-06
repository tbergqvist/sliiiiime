using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float shootCooldown;
    public GameObject projectile;
    public AudioClip shootSound;
    public string inputAxisSuffix;
    public ParticleSystem shootPS;
    float shootTimer;


    void Start()
    {

    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetAxis("Fire" + inputAxisSuffix) > 0.8f && shootTimer <= 0)
        {
            ShootProjectile();
            shootTimer = shootCooldown;
        }
    }
    void ShootProjectile()
    {
        GameManager.Instance.PlaySound(shootSound);
        Vector2 direction = GetShootDirection();
        Vector3 projectileForce = direction * projectileSpeed;
        var bla = Mathf.Atan2(direction.y, direction.x);
        var withOffset = new Vector2(transform.position.x, transform.position.y) + direction * 0.5f;

        var spawnedProjectile = Instantiate(projectile, new Vector3(withOffset.x, withOffset.y, 0), Quaternion.Euler(direction));
        spawnedProjectile.transform.Rotate(0, 0, bla / Mathf.PI * 180 + 90);
        spawnedProjectile.transform.localScale = gameObject.transform.localScale * (0.3f);
        spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spawnedProjectile.GetComponent<Collider2D>());
        spawnedProjectile.GetComponent<Projectile>().playerOwner = gameObject;

        var ps = Instantiate(shootPS, transform.position + new Vector3(direction.x * transform.localScale.x / 2, direction.y * transform.localScale.x / 2, 0), Quaternion.identity);
        var main = ps.main;
        switch (GetComponent<Slime>().playerNumber)
        {
            case GameManager.PlayerNumber.Player1:
                main.startColor = Color.red;
                break;
            case GameManager.PlayerNumber.Player2:
                main = shootPS.main;
                main.startColor = Color.green;
                break;
            case GameManager.PlayerNumber.Player3:
                main = shootPS.main;
                main.startColor = Color.blue;
                break;
            default:
                break;
        }

        Destroy(spawnedProjectile, 10);
    }
    Vector2 GetShootDirection()
    {
        if (GetComponent<Slime>().playerNumber == GameManager.PlayerNumber.Player1)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            return direction;
        }
        else
        {
            float horizontalAxis = Input.GetAxis("HorizontalAim" + inputAxisSuffix);
            float verticalAxis = Input.GetAxis("VerticalAim" + inputAxisSuffix);
            Vector2 direction = new Vector2(horizontalAxis, verticalAxis).normalized;
            return direction;
        }

    }
}
