using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public string levelName;
    public static int GAME_START_READY_TIME = 5;

    private List<Player> players = new List<Player>();
    private List<Player> readyPlayers = new List<Player>();

    private LobbyManager lobbyManager;

    private bool gameStarted;

    void Awake()
	{
        Object.DontDestroyOnLoad(this.gameObject);
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
        SceneManager.LoadScene(this.levelName);
        gameStarted = true;
        StartCoroutine("SpawnPlayers");
        this.GetComponent<PlayerInputManager>().DisableJoining();
	}

    private IEnumerator SpawnPlayers()
	{
        yield return new WaitForSeconds(0.5f); //Wait until the scene is loaded
        GameObject spawnPointHolder = GameObject.Find("SpawnPoints");
        int childrenCount = spawnPointHolder.transform.childCount;
        List<int> skipChildren = new List<int>();

        foreach(Player p in players)
		{
            int spawnIndex;

            do {
                spawnIndex = Random.Range(0, childrenCount - 1);
            } while (skipChildren.Contains(spawnIndex));

            skipChildren.Add(spawnIndex);
            p.transform.position = spawnPointHolder.transform.GetChild(spawnIndex).position;
            p.LoadBody();
        }
	}
}
