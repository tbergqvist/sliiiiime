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
    private void Update()
    {
        if(!IsInCameraView() && GetComponent<Collider2D>().enabled)
        {
            Died();
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
        if (TryGetComponent<MeshRenderer>(out var meshRenderer)) {
            meshRenderer.enabled = false;
        }
        if (TryGetComponent<Shooting>(out var shooting))
        {
            shooting.enabled = false;
        }
    }
    void EnablePlayer()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        if (TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            meshRenderer.enabled = true;
        }
        if (TryGetComponent<Shooting>(out var shooting))
        {
            shooting.enabled = true;
        }
    }
}
