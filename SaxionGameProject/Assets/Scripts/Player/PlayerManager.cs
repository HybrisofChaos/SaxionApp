using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    public void IterateThroughChildren(Transform parent, Color chosenColor)
    {
        Debug.Log(parent.childCount);
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            child.gameObject.GetComponent<SpriteRenderer>().color = chosenColor;
            IterateThroughChildren(child,chosenColor);
        }
    }

    public void OnPlayerJoined(PlayerInput player){
        // Spawn the player with a random color so it's easier to distinguish them
        Color randColor = new Color(Random.Range(0, 1F), Random.Range(0, 1f), Random.Range(0, 1f));
        IterateThroughChildren(player.transform,randColor);
        //   player.GetComponent<SpriteRenderer>().color = randColor;
    }
}
