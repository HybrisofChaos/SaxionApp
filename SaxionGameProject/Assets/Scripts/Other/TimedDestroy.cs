using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [SerializeField]
    private float time = 5f;

    [SerializeField]
    private bool startCountdownOnLoad = false;

    void Awake()
    {
        if (startCountdownOnLoad)
        {
            StartCountdown();
        }
    }

    public void StartCountdown()
	{
        Invoke("DestroyMe", time);
	}

    public void StopCountdown()
	{
        CancelInvoke("DestroyMe");
	}

    public void SetTime(float time)
	{
        this.time = time;
	}

    private void DestroyMe()
	{
        Destroy(this.gameObject);
	}
}
