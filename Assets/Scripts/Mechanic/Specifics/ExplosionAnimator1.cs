using UnityEngine;
using System.Collections;

/// Стартовая анимация взрыва
public class ExplosionAnimator1 : MonoBehaviour
{
    public Animator animatorEpicStart;
    public ElevatorBase elevatorBase;
    private GameObject[] elevators;

    public void Start()
    {
        elevators = elevatorBase.savers;
        int l = elevators.Length;
        if (l != 0)
        {
            StartCoroutine(LongExplosion());
        }
    }

    public IEnumerator LongExplosion()
    {
        yield return new WaitForSeconds(0.25f);
        if (elevators[PlayerPrefs.GetInt("elevatorsave")] != null)//2
        {
            animatorEpicStart.SetInteger("Action", 2);
        }
        else//1
        {
            animatorEpicStart.SetInteger("Action", 1);
        }
    }
}
