using UnityEngine;
using System.Collections;

/// Общий скрипт точечной анимации (очень не оптимизированный)
public class PointsAnimation : GlobalFunctions
{
    public AnimationType animationType = AnimationType.Infinity;
    public float speedSpeedPosition = 0.001f;
    public float speedPosition = 0.1f;
    public Vector3[] pointsPosition = new Vector3[0];
    public int counterPosition = 0;

    private float speedPositionActive = 0f;
    private int pointsPositionLength = 0;
    private bool make = true;
    private bool animMake = false;
    private bool isMoved = false;
    private Transform tr;

    public void SetPos(bool pos, float m) { speedPositionActive = speedPosition * (pos ? 1 : m); }

    private void Awake()
    {
        pointsPositionLength = pointsPosition.Length;
        tr = transform;
        switch (animationType)
        {
            case AnimationType.Infinity:
                make = true;
                isMoved = true;
                speedPositionActive = speedPosition;
                break;
            case AnimationType.Start:
                make = false;
                isMoved = false;
                break;
            case AnimationType.End:
                make = true;
                isMoved = true;
                speedPositionActive = speedPosition;
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
            speedPositionActive = speedPosition;
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
            speedPositionActive = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (animMake == true)
        {
            if (isMoved == true)
            {
                if (speedPositionActive != speedPosition)
                {
                    Vector2 ends = new Vector2(-speedPosition, speedPosition);
                    speedPositionActive = Mathf.MoveTowards(speedPositionActive, speedPosition, speedSpeedPosition);
                }
                else
                {
                    animMake = false;
                    isMoved = false;
                }
            }
            else
            {
                if (speedPositionActive != 0f)
                {
                    Vector2 ends = new Vector2(-speedPosition, speedPosition);
                    speedPositionActive = Mathf.MoveTowards(speedPositionActive, 0f, speedSpeedPosition);
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
        if (make)
        {
            if (tr.localPosition == pointsPosition[counterPosition])
            {
                counterPosition++;
                if (counterPosition == pointsPositionLength)
                { counterPosition = 0; }
            }
            else
            {
                float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
                tr.localPosition = Vector3.MoveTowards(tr.localPosition, pointsPosition[counterPosition], speedPositionActive * s);
            }
        }
    }
}
