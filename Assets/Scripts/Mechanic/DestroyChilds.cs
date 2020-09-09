using UnityEngine;

/// Уничтожение физического объекта, который ещё также можно разрушить
public class DestroyChilds : MonoBehaviour
{
    private Rigidbody2D[] childs;
    private Transform tr;
    private int c = 0;

    private void Awake()
    {
        tr = transform;
        c = tr.childCount;
        childs = new Rigidbody2D[c];
        for (int i = 0; i < c; i++)
        {
            childs[i] = tr.GetChild(i).GetComponent<Rigidbody2D>();
        }
    }

    public void ExplosionVelocity(Vector2 v, bool isEffect, bool isBomb)
    {
        if (isBomb == true)
        {
            Destroy(gameObject, 3f);
            for (int i = 0; i < c; i++)
            {
                childs[i].velocity = v;
            }
        }

        if (isEffect == false)
        {
            for (int i = 0; i < c; i++)
            {
                childs[i].gameObject.layer = 0;
            }
        }
        else
        {
            for (int i = 0; i < c; i++)
            {
                childs[i].gameObject.layer = 13;
            }
        }
    }
}
