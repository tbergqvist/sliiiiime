using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public enum PlayerNumber { Player1, Player2, Player3, NPC };

    public static GameManager Instance { get { return _instance; } }
    public AudioSource soundtrackAudioSource;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
    }
    public void PlayerDied()
    {
        Slime[] allPlayers = FindObjectsOfType<Slime>();
        int nrOfAlivePlayers = 0;
        foreach (Slime player in allPlayers)
        {
            if(player.isAlive)
            {
                nrOfAlivePlayers++;
            }
        }
        if(nrOfAlivePlayers == 1)
        {
            foreach (Slime player in allPlayers)
            {
                if (player.isAlive)
                {
                    EndGame(player.playerNumber);
                }
            }
        }
    }
    public void EndGame(PlayerNumber winner)
    {
        GameObject UI = GameObject.Find("UI");
        Text winText = UI.transform.Find("WinText").GetComponent<Text>();
        switch (winner)
        {
            case PlayerNumber.Player1:
                winText.color = Color.red;
                winText.text = "Red WON!";
                break;
            case PlayerNumber.Player2:
                winText.color = Color.green;
                winText.text = "Green WON!";
                break;
            case PlayerNumber.Player3:
                winText.color = Color.blue;
                winText.text = "Blue WON!";
                break;
            default:
                break;
        }
    }
    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero,volume);
    }
}
