using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowFactor = 0.05f;
    public float slowDownLength = 2f;

    public void DoSlowMotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void StopSlowMotion() {
        Time.timeScale = 1f;
    }


}
