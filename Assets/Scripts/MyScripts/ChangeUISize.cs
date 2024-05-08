using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeUISize : MonoBehaviour
{
    private Vector3 initScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 maxScale = new Vector3(5.0f, 5.0f, 1.0f);
    private float size = 0.0f;
    private const float SIZE_CHANGE_SPEED = 0.001f;
    private bool isDoEnlarge;

    private void FixedUpdate()
    {
        if (!isDoEnlarge) return;

        UpdateUIScale(maxScale);

        if (this.gameObject.transform.localScale == maxScale)
        {
            isDoEnlarge = false;
        }
    }

    private void UpdateUIScale(Vector2 scale)
    {
        this.transform.localScale = Vector2.Lerp(initScale, scale, size);
        size += SIZE_CHANGE_SPEED;
    }

    public void StartEnlarge()
    {
        size = 0.0f;
        isDoEnlarge = true;
    }

    public void StopEnlarge()
    {
        isDoEnlarge = false;
    }
}
