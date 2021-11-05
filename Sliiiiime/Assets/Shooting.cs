using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject player;
    public float projectileSpeed = 10f;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - new Vector2(player.transform.position.x, player.transform.position.y)).normalized;
            Vector3 projectileForce = direction * projectileSpeed;
            var bla = Mathf.Atan2(direction.y, direction.x);
            var withOffset = new Vector2(player.transform.position.x, player.transform.position.y) + direction * 0.5f;

            var spawnedProjectile = Instantiate(projectile, new Vector3(withOffset.x, withOffset.y, 0), Quaternion.Euler(direction));
            spawnedProjectile.transform.Rotate(0, 0, bla / Mathf.PI * 180 + 90);
            spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce, ForceMode2D.Impulse);

            spawnedProjectile.GetComponent<Projectile>().playerOwner = player;

            Destroy(spawnedProjectile, 2);
        }
    }
}
