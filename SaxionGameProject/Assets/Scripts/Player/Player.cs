using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerClassPrefab;

    public GameObject knightPrefab;
    public GameObject rangerPrefab;

    private bool ready = false;

    public delegate void ClassChangeAction(Player player, string className);
    public static event ClassChangeAction OnClassChange;

    private bool inputLocked = false;

    private GameManager gameManager;

    public enum PlayerClass
	{
        KNIGHT = 0,
        RANGER = 1
	}

    public PlayerClass playerClass = PlayerClass.KNIGHT;

    void Awake()
	{
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager)
		{
            gameManager.AddPlayer(this);
		}
	}

    public void setPlayerClassPrefab(GameObject prefab){
        this.playerClassPrefab = prefab;
    }

    public void OnReady(){
        this.ready = this.ready ? false : true;
        print(ready ? "ready" : "not ready");
		if (this.ready)
		{
            this.gameManager.AddReadyPlayer(this);
		}
		else
		{
            this.gameManager.RemoveReadyPlayer(this);
		}
    }

	public void OnMove(InputValue value)
	{
		if (this.ready || this.inputLocked) return;

		float xMov = value.Get<Vector2>().x;
        
        if (xMov == 0) return;

		int delta = (int)(xMov / Mathf.Abs(xMov)); //Either 1 or -1
		int enumCount = Enum.GetNames(typeof(PlayerClass)).Length;

		this.playerClass = (PlayerClass)Mathf.Abs(((int)this.playerClass + delta) % enumCount);
		OnClassChange(this, this.playerClass.ToString());

        StartCoroutine("LockInput");
	}

    IEnumerator LockInput()
	{
        this.inputLocked = true;
        yield return new WaitForSeconds(0.1f);
        this.inputLocked = false;
    }

}
