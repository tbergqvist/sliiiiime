using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUIHandler : MonoBehaviour
{
    public List<GameObject> player1Lives;
    public List<GameObject> player2Lives;
    public List<GameObject> player3Lives;


    public void RemoveLife(Slime.PlayerNumber playerNumber)
    {
        switch(playerNumber)
        {
            case Slime.PlayerNumber.Player1:
                DisableLife(player1Lives);
                break;
            case Slime.PlayerNumber.Player2:
                DisableLife(player2Lives);
                break;
            case Slime.PlayerNumber.Player3:
                DisableLife(player3Lives);
                break;
        }
    }
    void DisableLife(List<GameObject> playerLives)
    {
        foreach (GameObject life in playerLives)
        {
            if (life.GetComponent<SpriteRenderer>().enabled)
            {
                life.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
        }
    }
}
