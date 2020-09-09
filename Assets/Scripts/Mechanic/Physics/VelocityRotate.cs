using UnityEngine;

public class VelocityRotate : MonoBehaviour
{
    //Скрипт для вращения силы физических объектов
    public float rotate = 0f;//угол вращения
    public bool oneTime = true;//одноразовость использования
    private bool active = true;//активность скрипта

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (active == true)
        {
            if (oneTime == true)//проверка на одноразовость
            {
                active = false;
            }
            //изменение направления объекта
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 vel = rb.velocity;
            rb.velocity = RotateVector(vel, rotate);
        }
    }

    public Vector2 RotateVector(Vector2 a, float offsetAngle)//метод вращения объекта
    {
        float power = Mathf.Sqrt(a.x * a.x + a.y * a.y);//коэффициент силы
        float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90f + offsetAngle;//угол из координат с offset'ом
        return Quaternion.Euler(0, 0, angle) * Vector2.up * power;
        //построение вектора из изменённого угла с коэффициентом силы
    }
}