using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerOwner;
    public ParticleSystem shootPS;

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
            if (other.gameObject.TryGetComponent<Movement>(out _))
            {
                other.gameObject.transform.localScale *= 0.8f;
                Destroy(gameObject);
                return;
                
            }
            else { return; }
        }
        if (other.gameObject.TryGetComponent(out Slime slime))
        {
            slime.TakeDamage(0.1f);
            var ps = Instantiate(shootPS, transform.position, Quaternion.identity);
            var main = ps.main;
            switch (slime.playerNumber)
            {
                case GameManager.PlayerNumber.Player1:
                    main.startColor = Color.red;
                    break;
                case GameManager.PlayerNumber.Player2:
                    main.startColor = Color.green;
                    break;
                case GameManager.PlayerNumber.Player3:
                    main.startColor = Color.blue;
                    break;
                default:
                    main.startColor = Color.green;
                    break;
            }
            playerOwner.GetComponent<Slime>().DealtDamage(0.3f);

            Destroy(gameObject);
        }
        else if(other.gameObject.TryGetComponent<Platform>(out _))
        {
            Destroy(gameObject);
        }
    }
}
