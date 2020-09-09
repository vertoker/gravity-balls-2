using UnityEngine;

/// Скрипт для вращения объекта в сторону позиции
public class LookTransform : MonoBehaviour
{
    public Transform at;
    private Transform tr;

    public void Awake()
    {
        tr = transform;
    }

    public void Update()
    {
        Vector2 a = at.position - tr.position;
        float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90f;
        tr.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}