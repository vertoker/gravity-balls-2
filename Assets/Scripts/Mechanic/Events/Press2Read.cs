using System.Collections;
using UnityEngine;

/// Триггер для терминалов и появления кнопки "прочесть"
public class Press2Read : MonoBehaviour
{
    public Animator animator;
    public bool act = false;
    public TipsInput tips;
    public Data data;

    public void Active(TipsInput tip)
    {
        if (act == false)
        {
            animator.SetInteger("Active", 1);
            tips = tip;
            act = true;
        }
    }

    public void DeActive()
    {
        if (act == true)
        {
            animator.SetInteger("Active", 2);
            //data.FalseP2R();
            act = false;
        }
    }

    public void Tap()
    {
        if (act == true)
        {
            animator.SetInteger("Active", 4);
            data.SetDialoge(tips.idTips);
        }
    }

    public void UnTap()
    {
        if (act == true)
        {
            animator.SetInteger("Active", 3);
        }
    }
}
