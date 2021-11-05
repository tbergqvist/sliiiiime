using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerOwner;
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.CompareTag("Bomb"))
        {
            if ((other.gameObject.TryGetComponent<Movement>(out _)))
            {
                other.gameObject.transform.localScale *= 0.8f;
                Destroy(gameObject);
                return;
                var exp = Instantiate(explosion, gameObject.transform.position, Quaternion.identity, null);
                Destroy(exp, 3f);
            }
            else { return; }
        }
        if (other.gameObject.TryGetComponent(out Slime slime))
        {
            slime.TakeDamage(0.1f);
            playerOwner.GetComponent<Slime>().DealtDamage(0.1f);

            Destroy(gameObject);
        }
        else if(other.gameObject.TryGetComponent<Platform>(out _))
        {
            Destroy(gameObject);
        }
    }
}
