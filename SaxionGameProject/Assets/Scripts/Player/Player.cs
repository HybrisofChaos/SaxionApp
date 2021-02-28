using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject rangerPrefab;
    public int index;

    private bool ready = false;

    public delegate void ClassChangeAction(Player player, string className);
    public static event ClassChangeAction OnClassChange;

    private bool inputLocked = false;

    private GameManager gameManager;

    public Color color;

    public enum PlayerClass
    {
        KNIGHT = 0,
        RANGER = 1
    }

    public PlayerClass playerClass = PlayerClass.KNIGHT;

    private void ChooseColor()
    {
        switch (index)
        {
            case 0: // Player 1, Red
                this.color = new Color(UnityEngine.Random.Range(0.8F, 0.9F), UnityEngine.Random.Range(0.1F, 0.5F), UnityEngine.Random.Range(0.1F, 0.5F)); // R
                break;
            case 1: // Player 2, Blue
                this.color = new Color(UnityEngine.Random.Range(0.1F, 0.5F), UnityEngine.Random.Range(0.1F, 0.5F), UnityEngine.Random.Range(0.8F, 0.9F)); // B
                break;
            case 2: // Player 3, Yellow
                this.color = new Color(UnityEngine.Random.Range(0.8F, 0.9F), UnityEngine.Random.Range(0.8F, 0.9F), UnityEngine.Random.Range(0.1F, 0.5F)); // R G
                break;
            case 3: // Player 4, Green
                this.color = new Color(UnityEngine.Random.Range(0.1F, 0.5F), UnityEngine.Random.Range(0.8F, 0.9F), UnityEngine.Random.Range(0.1F, 0.5F)); // G
                break;
        }
    }

    void Awake()
    {
        ChooseColor();
        DontDestroyOnLoad(this.gameObject);
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager)
        {
            gameManager.AddPlayer(this);
        }

    }

    public void OnReady()
    {
        if (!gameManager.gameStarted)
        {
            if (!this.ready)
            {
                this.gameManager.AddReadyPlayer(this);
                this.ready = true;
            }
            else
            {
                this.gameManager.RemoveReadyPlayer(this);
                this.ready = false;
            }
        }
    }

    //Select classes in this case
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


    private void IterateThroughChildren(Transform parent, Color chosenColor)
    {
        Debug.Log(parent.childCount);
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.gameObject.GetComponent<SpriteRenderer>())
            {
                if (child.gameObject.name == "aim_arrow")
                {
                    chosenColor = Color.white - chosenColor;
                    chosenColor.a = 1;
                }
                child.gameObject.GetComponent<SpriteRenderer>().color = chosenColor;
            }
            IterateThroughChildren(child, chosenColor);
        }
    }

    public void LoadBody()
    {
        GameObject go = null;
        switch (playerClass)
        {
            case PlayerClass.KNIGHT:
                go = Instantiate(knightPrefab, this.transform);
                break;
            case PlayerClass.RANGER:
                go = Instantiate(rangerPrefab, this.transform);
                break;
        }

        IterateThroughChildren(go.transform, this.color);
    }
}
