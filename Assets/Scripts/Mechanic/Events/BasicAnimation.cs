using UnityEngine;
using System.Collections;

/// Общий скрипт базовой анимации (не оптимизированный)
public class BasicAnimation : GlobalFunctions
{
    public AnimationType animationType = AnimationType.Infinity;
    public float speedSpeed = 0.05f;
    public float rotation = 0f;

    private bool make = true;
    private bool animMake = false;
    private bool isMoved = false;
    private Transform tr;
    private float rotationActive = 0f;

    public void SetPos(bool pos, float m)
    {
        rotationActive = rotation * (pos ? 1 : m);
    }

    private void Start()
    {
        tr = transform;
        animMake = false;
        switch (animationType)
        {
            case AnimationType.Infinity:
                make = true;
                isMoved = true;
                rotationActive = rotation;
                break;
            case AnimationType.Start:
                make = false;
                isMoved = false;
                break;
            case AnimationType.End:
                make = true;
                isMoved = true;
                rotationActive = rotation;
                break;
            case AnimationType.All: 
                make = false;
                isMoved = false;
                break;
        }
    }

    public void TimerAnim(float timer, bool anim)
    {
        StartAnim(anim);
        StartCoroutine(TimerTimerAnim(timer, anim));
    }

    private IEnumerator TimerTimerAnim(float timer, bool anim)
    {
        yield return new WaitForSeconds(timer);
        EndAnim(anim);
    }

    public void StartAnim(bool anim)
    {
        make = true;
        if (anim == true)
        {
            animMake = true;
            isMoved = true;
        }
        else
        {
            rotationActive = rotation;
        }
    }

    public void EndAnim(bool anim)
    {
        if (anim == true)
        {
            animMake = true;
            isMoved = false;
        }
        else
        {
            make = false;
            rotationActive = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (animMake == true)
        {
            if (isMoved == true)
            {
                if (rotationActive != rotation)
                {
                    rotationActive = Mathf.MoveTowards(rotationActive, rotation, speedSpeed);
                }
                else
                {
                    animMake = false;
                    isMoved = false;
                }
            }
            else
            {
                if (rotationActive != 0f)
                {
                    rotationActive = Mathf.MoveTowards(rotationActive, 0f, speedSpeed);
                }
                else
                {
                    animMake = false;
                    isMoved = true;
                }
            }
        }
    }

    private void Update()
    {
        if (make == true)
        {
            float rot = tr.localEulerAngles.z;
            float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
            tr.localEulerAngles = new Vector3(0f, 0f, rot + rotationActive * s);
        }
    }
}