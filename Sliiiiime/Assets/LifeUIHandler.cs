using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeUIHandler : MonoBehaviour
{
    public List<GameObject> player1Lives;
    public List<GameObject> player2Lives;
    public List<GameObject> player3Lives;


    public void RemoveLife(GameManager.PlayerNumber playerNumber)
    {
        switch(playerNumber)
        {
            case GameManager.PlayerNumber.Player1:
                DisableLife(player1Lives);
                break;
            case GameManager.PlayerNumber.Player2:
                DisableLife(player2Lives);
                break;
            case GameManager.PlayerNumber.Player3:
                DisableLife(player3Lives);
                break;
        }
    }
    void DisableLife(List<GameObject> playerLives)
    {
        foreach (GameObject life in playerLives)
        {
            if (life.GetComponent<Image>().enabled)
            {
                life.GetComponent<Image>().enabled = false;
                return;
            }
        }
    }
}
