using UnityEngine;

/// Скрипт для рандомного перемещения объекта внутри прямоугольника
public class MoveAnimation : MonoBehaviour
{
    public enum AnimationType { MoveTowards = 1, Lerp = 2 }
    public AnimationType animationType = AnimationType.Lerp;
    public Vector2 minBorder;
    public Vector2 maxBorder;
    public Vector2 target;
    public float speed = 0.25f;
    public bool active = true;

    public void Start()
    {
        float x = Random.Range(minBorder.x, maxBorder.x);
        float y = Random.Range(minBorder.y, maxBorder.y);
        target = new Vector2(x, y);
    }

    private void Update()
    {
        if (active == true)
        {
            Vector2 pos = gameObject.transform.localPosition;
            if (pos == target)
            {
                float x = Random.Range(minBorder.x, maxBorder.x);
                float y = Random.Range(minBorder.y, maxBorder.y);
                target = new Vector2(x, y);
            }
            else
            {
                float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
                if (animationType == AnimationType.Lerp)
                {
                    gameObject.transform.localPosition = Vector3.Lerp(pos, target, speed * s);
                }
                else
                {
                    gameObject.transform.localPosition = Vector3.MoveTowards(pos, target, speed * s);
                }
            }
        }
    }
}
