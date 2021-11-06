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
    public float damage;
    float shootTimer;


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
        spawnedProjectile.GetComponent<Projectile>().damage = damage;

        SetProjectileColour(spawnedProjectile);

        Destroy(spawnedProjectile, 10);

    }

    [System.Obsolete]
    private void SetProjectileColour(GameObject spawnedProjectile)
    {
        switch (GetComponent<Slime>().playerNumber)
        {
            case GameManager.PlayerNumber.Player1:
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<ParticleSystem>().startColor = Color.red;
                break;
            case GameManager.PlayerNumber.Player2:
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<ParticleSystem>().startColor = Color.green;
                break;
            case GameManager.PlayerNumber.Player3:
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                spawnedProjectile.GetComponent<Projectile>().GetComponentInChildren<ParticleSystem>().startColor = Color.blue;
                break;
            default:
                break;
        }
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
