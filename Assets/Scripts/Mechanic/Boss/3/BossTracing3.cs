using System.Collections;
using UnityEngine;

public class BossTracing3 : MonoBehaviour
{
    public Vector4[] boxs = new Vector4[0];
    public BossManagement3 bm;
    public Transform trRaycast;
    public Transform trDirection1;
    public Transform trDirection2;
    public Transform trDirection3;
    public Transform trDirection4;
    public Transform trDirection5;
    public Transform trDirection6;
    public Transform trDirection7;
    public Transform trDirection8;
    private float angle = 0f;
    private int lengthBoxs;

    public void Awake()
    {
        Gizmos.color = Color.green;
        lengthBoxs = boxs.Length;
    }

    /// Выдаёт позицию следующего передвижения (самый длинный raycast)
    public Vector2 GetPosRaycast()
    {
        angle = Random.Range(-180f, 180f);
        trRaycast.localEulerAngles = new Vector3(0f, 0f, angle);

        RaycastHit2D hit1 = Physics2D.Raycast(trDirection1.position, RotateVector(angle), 200f);
        RaycastHit2D hit2 = Physics2D.Raycast(trDirection2.position, RotateVector(angle + 180f), 200f);
        RaycastHit2D hit3 = Physics2D.Raycast(trDirection3.position, RotateVector(angle - 90f), 200f);
        RaycastHit2D hit4 = Physics2D.Raycast(trDirection4.position, RotateVector(angle + 90f), 200f);
        RaycastHit2D hit5 = Physics2D.Raycast(trDirection5.position, RotateVector(angle - 45f), 200f);
        RaycastHit2D hit6 = Physics2D.Raycast(trDirection6.position, RotateVector(angle + 45f), 200f);
        RaycastHit2D hit7 = Physics2D.Raycast(trDirection7.position, RotateVector(angle - 135f), 200f);
        RaycastHit2D hit8 = Physics2D.Raycast(trDirection8.position, RotateVector(angle + 135f), 200f);

        float distance1 = Vector2.Distance(trDirection1.position, hit1.point);
        float distance2 = Vector2.Distance(trDirection2.position, hit2.point);
        float distance3 = Vector2.Distance(trDirection3.position, hit3.point);
        float distance4 = Vector2.Distance(trDirection4.position, hit4.point);
        float distance5 = Vector2.Distance(trDirection5.position, hit5.point);
        float distance6 = Vector2.Distance(trDirection6.position, hit6.point);
        float distance7 = Vector2.Distance(trDirection7.position, hit7.point);
        float distance8 = Vector2.Distance(trDirection8.position, hit8.point);

        Vector2 ret = hit1.point;
        float bigDis = distance1;
        if (distance2 >= bigDis) { bigDis = distance2; ret = hit2.point; }
        if (distance3 >= bigDis) { bigDis = distance3; ret = hit3.point; }
        if (distance4 >= bigDis) { bigDis = distance4; ret = hit4.point; }
        if (distance5 >= bigDis) { bigDis = distance5; ret = hit5.point; }
        if (distance6 >= bigDis) { bigDis = distance6; ret = hit6.point; }
        if (distance7 >= bigDis) { bigDis = distance7; ret = hit7.point; }
        if (distance8 >= bigDis) { bigDis = distance8; ret = hit8.point; }
        return ret;
    }

    /// Рисует появляющиеся raycast'ы
    public void OnDraw()//GizmosSelected()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(trDirection1.position, RotateVector(angle), 200f);
        RaycastHit2D hit2 = Physics2D.Raycast(trDirection2.position, RotateVector(angle + 180f), 200f);
        RaycastHit2D hit3 = Physics2D.Raycast(trDirection3.position, RotateVector(angle - 90f), 200f);
        RaycastHit2D hit4 = Physics2D.Raycast(trDirection4.position, RotateVector(angle + 90f), 200f);
        RaycastHit2D hit5 = Physics2D.Raycast(trDirection5.position, RotateVector(angle - 45f), 200f);
        RaycastHit2D hit6 = Physics2D.Raycast(trDirection6.position, RotateVector(angle + 45f), 200f);
        RaycastHit2D hit7 = Physics2D.Raycast(trDirection7.position, RotateVector(angle - 135f), 200f);
        RaycastHit2D hit8 = Physics2D.Raycast(trDirection8.position, RotateVector(angle + 135f), 200f);

        Gizmos.DrawLine(trDirection1.position, hit1.point);
        Gizmos.DrawLine(trDirection2.position, hit2.point);
        Gizmos.DrawLine(trDirection3.position, hit3.point);
        Gizmos.DrawLine(trDirection4.position, hit4.point);
        Gizmos.DrawLine(trDirection5.position, hit5.point);
        Gizmos.DrawLine(trDirection6.position, hit6.point);
        Gizmos.DrawLine(trDirection7.position, hit7.point);
        Gizmos.DrawLine(trDirection8.position, hit8.point);
    }

    /// Вращение вектора
    public Vector2 RotateVector(float angle)
    {
        return Quaternion.Euler(0, 0, angle) * Vector2.up;
    }

    /// Нахождение объекта в каком-либо из зон
    public int BoxPos(Vector2 pos)
    {
        for (int i = 0; i < lengthBoxs; i++)
        {
            if (pos.x < boxs[i].x && pos.y < boxs[i].y && pos.x > boxs[i].z && pos.y > boxs[i].w)
            {
                return i;
            }
        }
        return -1;
    }
}