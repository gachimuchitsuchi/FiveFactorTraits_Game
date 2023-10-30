using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private const float TIME_LIMIT = 10.0f;
    private float countDown;
    private bool isDoCountDown;
    public event Action countZeroEvent = delegate { };

    // Update is called once per frame
    void Update()
    {
        if (!isDoCountDown) return;

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        if (countDown < 0)
        {
            countDown = 0;
            if (countZeroEvent != null) countZeroEvent();
            Debug.Log("0•b‚Å‚·");
        }
    }

    public void StartCountDown()
    {
        countDown = TIME_LIMIT;
        isDoCountDown = true;
    }

    public void StopCountDown()
    {
        isDoCountDown = false;
    }

    public float GetTime()
    {
        return countDown;
    }
}
