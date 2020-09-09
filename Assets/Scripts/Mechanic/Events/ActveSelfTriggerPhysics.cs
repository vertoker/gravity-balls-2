﻿using UnityEngine;

/// Триггер для установления здоровья объектов
public class ActveSelfTriggerPhysics : MonoBehaviour
{
    public float healthTarget = 0f;
    public bool oneTime = true;
    public bool activeReverse = false;
    public PhysicsEmulation[] output;
    private bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i].health = healthTarget;
                output[i].HealthUpdate(transform, Vector2.zero, false);
            }
        }
    }
}