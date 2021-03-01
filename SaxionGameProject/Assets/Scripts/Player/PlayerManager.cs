using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput player)
    {
        //Deprecated
        print(player.playerIndex + " Index");
        player.GetComponent<Player>().index = player.playerIndex;
    }
}
