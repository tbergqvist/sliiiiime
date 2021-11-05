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
        if (other.gameObject == playerOwner)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Movement>(out _))
        {
            playerOwner.transform.localScale = playerOwner.transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
            other.gameObject.transform.localScale = other.gameObject.transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);

            Destroy(gameObject);
        }
    }
}
