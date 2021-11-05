using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerOwner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
