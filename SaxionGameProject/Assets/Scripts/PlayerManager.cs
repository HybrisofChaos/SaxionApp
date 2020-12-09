using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput player){
        // Spawn the player with a random color so it's easier to distinguish them
        Color randColor = new Color(Random.Range(0, 1F), Random.Range(0, 1f), Random.Range(0, 1f));
        player.GetComponent<SpriteRenderer>().color = randColor;
    }
}
