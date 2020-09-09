using System.Collections;
using UnityEngine;
/// Триггер для инициации работы скрипта Active (background)
public class Initiator : GlobalFunctions
{
    public AnimationTypeCollider animationType = AnimationTypeCollider.Trigger;
    public Vector2 timerForOffset = new Vector2(0.2f, 1.8f);//on|off\\
    public float timeOffset = 3f;
    public bool isActiving = true;
    public bool oneTime = false;
    public bool oneTimeActive = true;
    public Active[] outputs = new Active[0];
    private bool t = true;
    private bool initiatorActive = false;
    private IEnumerator initiatorTimer;

    private void Start()
    {
        initiatorActive = PlayerPrefs.GetString("graphicsquality") == "high";
        initiatorTimer = InitiatorTimer(isActiving);
        if (animationType == AnimationTypeCollider.Start && t == true)
        {
            StartCoroutine(StartOffset());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (initiatorActive == true)
        {
            if (collision.CompareTag("Player") && animationType == AnimationTypeCollider.Trigger && t == true)
            {
                if (oneTimeActive == true)
                {
                    t = false;
                }

                if (oneTime == false)
                {
                    StartCoroutine(initiatorTimer);
                }
                else
                {
                    OneTimeTrue();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (initiatorActive == true)
        {
            if (collision.gameObject.CompareTag("Player") && animationType == AnimationTypeCollider.Collision && t == true)
            {
                if (oneTimeActive == true)
                {
                    t = false;
                }

                if (oneTime == false)
                {
                    StartCoroutine(initiatorTimer);
                }
                else
                {
                    OneTimeTrue();
                }
            }
        }
    }

    private IEnumerator StartOffset()
    {
        yield return new WaitForSeconds(timeOffset);
        if (oneTimeActive == true)
        {
            t = false;
        }

        if (oneTime == false)
        {
            StartCoroutine(initiatorTimer);
        }
        else
        {
            OneTimeTrue();
        }
    }

    private void OneTimeTrue()
    {
        if (isActiving == true)
        {
            if (outputs.Length != 0)
            {
                for (int i = 0; i < outputs.Length; i++)
                {
                    outputs[i].On();
                }
            }
        }
        else
        {
            if (outputs.Length != 0)
            {
                for (int i = 0; i < outputs.Length; i++)
                {
                    outputs[i].Off();
                }
            }
        }
    }

    private IEnumerator InitiatorTimer(bool active)
    {
        if (active == true)
        {
            yield return new WaitForSeconds(timerForOffset.x);
            if (outputs.Length != 0)
            {
                for (int i = 0; i < outputs.Length; i++)
                {
                    outputs[i].On();
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(timerForOffset.y);
            if (outputs.Length != 0)
            {
                for (int i = 0; i < outputs.Length; i++)
                {
                    outputs[i].Off();
                }
            }
        }
        StartCoroutine(InitiatorTimer(!active));
    }
}
