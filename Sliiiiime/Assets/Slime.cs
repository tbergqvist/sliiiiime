using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameManager.PlayerNumber playerNumber;
    public int lives;
    public float respawnTime;
    public bool isAlive = true;
    public AudioClip takeDamageSound;
    public AudioClip respawnSound;

    void Start()
    {
    }

    public void TakeDamage(float amount)
    {
        transform.localScale -= new Vector3(amount, amount, amount);
        GameManager.Instance.PlaySound(takeDamageSound,0.8f);
        if(transform.localScale.x <= 0)
        {
            Died();
        }
    }
    public void DealtDamage(float amount)
    {
        transform.localScale += new Vector3(amount, amount, amount);
    }
    void Died()
    {
        DisablePlayer();
        GameObject.Find("UI").GetComponent<LifeUIHandler>().RemoveLife(playerNumber);
        lives--;
        if(lives < 0)
        {
            Eliminated();
        }
        else
        {
            Invoke("Respawn", respawnTime);
        }
    }
    void Eliminated()
    {
        isAlive = false;
        GameManager.Instance.PlayerDied();
    }
    void Respawn()
    {
        EnablePlayer();
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
        transform.localScale = Vector3.one;
        GameManager.Instance.PlaySound(respawnSound,1);
    }
    void DisablePlayer()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Shooting>().enabled = false;
    }
    void EnablePlayer()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Shooting>().enabled = true;
    }
}
