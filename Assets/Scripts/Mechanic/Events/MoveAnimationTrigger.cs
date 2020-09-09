using UnityEngine;
/// Триггер для скрипта MoveAnimation
public class MoveAnimationTrigger : GlobalFunctions
{
    public AnimationTypeBP animationType = AnimationTypeBP.Basic;
    public AnimationType animationType2 = AnimationType.Infinity;
    public float timer = 1f;
    public bool anim = true;
    public bool oneTime = true;
    public GameObject[] outputs;
    private bool t = true;

    private void Awake()
    {
        ActiveAwake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && t == true)
        {
            Active(anim);
            if (oneTime == true)
            {
                t = false;
            }
        }
    }

    private void Active(bool anim)
    {
        if (animationType == AnimationTypeBP.Basic)
        {
            switch (animationType2)
            {
                case AnimationType.Start:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<BasicAnimation>().StartAnim(anim);
                    }
                    break;
                case AnimationType.End:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<BasicAnimation>().EndAnim(anim);
                    }
                    break;
                case AnimationType.All:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<BasicAnimation>().TimerAnim(timer, anim);
                    }
                    break;
            }
        }
        else if (animationType == AnimationTypeBP.Points)
        {
            switch (animationType2)
            {
                case AnimationType.Start:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<PointsAnimation>().StartAnim(anim);
                    }
                    break;
                case AnimationType.End:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<PointsAnimation>().EndAnim(anim);
                    }
                    break;
                case AnimationType.All:
                    for (int i = 0; i < outputs.Length; i++)
                    {
                        outputs[i].GetComponent<PointsAnimation>().TimerAnim(timer, anim);
                    }
                    break;
            }
        }
        else if (animationType == AnimationTypeBP.Move)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].GetComponent<MoveAnimation>().active = anim;
            }
        }
        else if (animationType == AnimationTypeBP.Tramp)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].GetComponent<TrampAnim>().active = anim;
            }
        }
        return;
    }

    private void ActiveAwake()
    {
        if (animationType == AnimationTypeBP.Basic)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].GetComponent<BasicAnimation>().animationType = animationType2;
            }
        }
        else if (animationType == AnimationTypeBP.Points)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].GetComponent<PointsAnimation>().animationType = animationType2;
            }
        }
        return;
    }
}