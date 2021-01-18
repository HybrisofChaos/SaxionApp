using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponStats : MonoBehaviour
{
    public enum Modes
    { melee, Straight, Follow, Throw }

    public float projectileSpeed;
    public float coolDown;
    public bool pierce;

    public Modes projectileMode;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.attachedRigidbody);
    }

    void Update()
    {

    }

}
