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

	public bool gameStarted;
	private bool playersSpawned;


	private GameObject spawnPointHolder;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		lobbyManager = GameObject.FindObjectOfType<LobbyManager>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		PlayerController.OnDeath += OnPlayerDeath;
	}

	public void AddPlayer(Player player)
	{
		this.players.Add(player);
		this.lobbyManager.AddPlayer(player);
	}

	public void AddReadyPlayer(Player player)
	{
		this.readyPlayers.Add(player);

		if (this.readyPlayers.Count == this.players.Count)
		{
			StartCoroutine("StartGame");
		}

		lobbyManager.ReadyChange(player, true);
	}

	public void RemoveReadyPlayer(Player player)
	{
		if (this.readyPlayers.Count == this.players.Count)
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
		playersSpawned = false;

		StartCoroutine("SpawnPlayers");
		this.GetComponent<PlayerInputManager>().DisableJoining();
	}

	private void OnPlayerDeath(GameObject playerDead, GameObject killer)
	{
		Player player = playerDead.transform.parent.GetComponent<Player>();

		print(playerDead.name + " was killed by " + killer.name);

		Destroy(playerDead.gameObject);
		StartCoroutine(StartSpawnPlayer(player, 2));
	}

	private IEnumerator SpawnPlayers()
	{
		if (!playersSpawned)
		{
			yield return new WaitForSeconds(0.5f); //Wait until the scene is loaded

			GameObject spawnPointHolder = GameObject.Find("SpawnPoints");
			int childrenCount = spawnPointHolder.transform.childCount;
			List<int> skipChildren = new List<int>();

			//Randomly pick a spawn point for the players
			foreach (Player p in players)
			{
				int spawnIndex;

				do
				{
					spawnIndex = Random.Range(0, childrenCount - 1);
				} while (skipChildren.Contains(spawnIndex));

				skipChildren.Add(spawnIndex);
				p.transform.position = spawnPointHolder.transform.GetChild(spawnIndex).position;
				if (p.transform.childCount == 0)
				{
					p.LoadBody();
				}
			}

			playersSpawned = true;
		}
	}

	private void SpawnPlayer(Player player)
	{
		if (!spawnPointHolder)
		{
			spawnPointHolder = GameObject.Find("SpawnPoints");
		}

		int spawnIndex;
		int childrenCount = this.spawnPointHolder.transform.childCount;
		spawnIndex = Random.Range(0, childrenCount - 1);

		player.transform.position = spawnPointHolder.transform.GetChild(spawnIndex).position;
		if (player.transform.childCount == 0)
		{
			player.LoadBody();
		}
	}

	private IEnumerator StartSpawnPlayer(Player player, float spawnTimer)
	{
		yield return new WaitForSeconds(spawnTimer);
		SpawnPlayer(player);
	}

	void OnDestroy()
	{
		PlayerController.OnDeath -= OnPlayerDeath;
	}
}
