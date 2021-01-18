using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int GAME_START_READY_TIME = 5;

    private List<Player> players = new List<Player>();
    private List<Player> readyPlayers = new List<Player>();

    private LobbyManager lobbyManager;

    void Awake()
	{
        lobbyManager = GameObject.FindObjectOfType<LobbyManager>();
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(Player player)
    {
        this.players.Add(player);
        this.lobbyManager.AddPlayer(player);
    }

    public void AddReadyPlayer(Player player)
	{
        this.readyPlayers.Add(player);
        print(this.readyPlayers.Count + " : " + this.players.Count);
        if(this.readyPlayers.Count == this.players.Count)
		{
            StartCoroutine("StartGame");
		}

        lobbyManager.ReadyChange(player, true);
	}

    public void RemoveReadyPlayer(Player player)
	{
        if(this.readyPlayers.Count == this.players.Count)
		{
            StopCoroutine("StartGame");
            lobbyManager.StopGameCountdown();
		}

        this.readyPlayers.Remove(player);

        lobbyManager.ReadyChange(player, false);

    }

    IEnumerator StartGame()
	{
        lobbyManager.StartGameCountdown();
        yield return new WaitForSeconds(GAME_START_READY_TIME);
        // Go to level
	}
}
