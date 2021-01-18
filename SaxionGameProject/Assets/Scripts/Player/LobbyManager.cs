using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Transform lobbyPlayerObject;
    public GameObject countdownObject;

    private List<Player> players = new List<Player>();
    private float maxCountdown;
    private float countdown;

    private bool countdownStarted;

    public void Start()
	{
        maxCountdown = GameManager.GAME_START_READY_TIME;
        countdown = maxCountdown;

        Player.OnClassChange += classChange;
	}

    void Update()
	{
        if(countdownStarted && countdownObject)
		{
            countdownObject.GetComponent<Text>().text = "Game Starts in " + (int)countdown + " seconds";
            countdown -= Time.deltaTime;
		}
		else
		{
            countdownObject.GetComponent<Text>().text = "";
		}
	}

    void OnDestroy()
	{
        Player.OnClassChange -= classChange;
	}

    public void AddPlayer(Player player)
	{
        lobbyPlayerObject.GetChild(players.Count).gameObject.SetActive(true);

        this.players.Add(player);
	}

    //TODO: Move to GameManager
    private void classChange(Player player, string className)
	{
        int playerIndex = players.IndexOf(player);
        string formattedClassName = className.Substring(0, 1) + className.Substring(1).ToLower();
        Transform uiObject = this.GetPlayerUIObject(player);

        uiObject.GetChild(0).GetComponent<Text>().text = "< " + formattedClassName + " >";

        for(int i = 1; i < uiObject.transform.childCount; i++)
		{
            uiObject.GetChild(i).gameObject.SetActive(false);
		}
        uiObject.GetChild(1 + (int)player.playerClass).gameObject.SetActive(true);

    }

    public void StartGameCountdown()
	{
        countdownStarted = true;
	}

    public void StopGameCountdown()
	{
        countdownStarted = false;
        countdown = maxCountdown;
	}

    public void ReadyChange(Player player, bool ready)
	{
        int playerIndex = players.IndexOf(player);
        lobbyPlayerObject.GetChild(playerIndex).GetComponent<Text>().color = ready ? Color.green : Color.white;
	}

    public Transform GetPlayerUIObject(Player player)
	{
        return this.lobbyPlayerObject.GetChild(this.players.IndexOf(player));
	}
}
