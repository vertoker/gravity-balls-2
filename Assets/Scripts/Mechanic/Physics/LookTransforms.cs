using UnityEngine;

/// Скрипт для вращения объекта в сторону позиции (для массива объектов)
public class LookTransforms : MonoBehaviour
{
    public Transform at;
    public Transform[] trs;
    private int length;

    public void Awake()
    {
        length = trs.Length;
    }

    public void Update()
    {
        for (int i = 0; i < length; i++)
        {
            Vector2 a = at.position - trs[i].position;
            float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90f;
            trs[i].rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}