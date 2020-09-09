using UnityEngine;

/// Хранилище общих специфических математических и логических функций
public class GlobalFunctions : MonoBehaviour
{
    public enum TypePlaying { Sound = 0, Music = 1 };
    public enum AnimationType { Infinity = 1, Start = 2, End = 3, All = 4 };
    public enum AnimationTypeCollider { Start = 1, Trigger = 2, Collision = 3 };
    public enum AnimationTypeBP { Basic = 1, Points = 2, Move = 3, Tramp = 4 };

    public float Stable(float input, float min, float max)
    {
        float dist = max - min;
        bool b = true;
        while (b)
        {
            if (input > max)
            {
                input = input - dist;
            }
            else if (input < min)
            {
                input = input + dist;
            }
            else { b = false; }
        }
        return input;
    }
    public float Distance(Vector2 c)
    {
        return Mathf.Sqrt(c.x * c.x + c.y * c.y);
    }
    public float Stable2(float input, float min, float max)
    {
        if (input > max)
        {
            input = max;
        }
        else if (input < min)
        {
            input = min;
        }
        return input;
    }
    public float Stable3(float input, float min, float max)
    {
        float dist = max - min;
        if (input > max)
        {
            input = input - dist;
        }
        else if (input < min)
        {
            input = input + dist;
        }
        return input;
    }
    public float MoveToward(float current, float target, float speed, Vector2 ends)
    {
        if (current > target)
        {
            current = Stable2(current - speed, target, ends.y);
        }
        else if (current < target)
        {
            current = Stable2(current + speed, ends.x, target);
        }
        return current;
    }
    public float MoveToward2(float current, float target, float speed, float min, float max)
    {
        if (current > target)
        {
            current -= speed;
            if (current < target)
            {
                current += max - target;
            }
        }
        else if (current < target)
        {
            current += speed;
            if (current > target)
            {
                current -= target - min;
            }
        }
        return current;
    }
    public float MoveToward3(float current, float target, float speed)
    {
        if (current > target)
        {
            current -= speed;
            if (current < target)
            {
                current = target;
            }
        }
        else if (current < target)
        {
            current += speed;
            if (current > target)
            {
                current = target;
            }
        }
        return current;
    }
    public bool IsVectorTarget(Vector2 pos, Vector2 target, float assist)
    {
        float assist2 = assist / 2f;
        if (target.x - assist2 < pos.x && target.x + assist2 > pos.x)
        {
            if (target.y - assist2 < pos.y && target.y + assist2 > pos.y)
            {
                Debug.Log(true);
                return true;
            }
        }
        Debug.Log(false);
        return false;
    }
    public Color ColorAriphA(Color input, float count)
    {
        float res = Stable2(input.a + count, 0f, 1f);
        return new Color(input.r, input.g, input.b, res);
    }
    public int[] Add(int[] old, int addComponent)
    {
        int[] n = new int[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public int[] Remove(int[] old, int removeComponent)
    {
        int[] n = new int[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (i != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new int[0];
        }
        return n;
    }
    public GameObject[] Add(GameObject[] old, GameObject addComponent)
    {
        GameObject[] n = new GameObject[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public GameObject[] Remove(GameObject[] old, int removeComponent)
    {
        GameObject[] n = new GameObject[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (i != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new GameObject[0];
        }
        return n;
    }
    public GameObject[] Remove(GameObject[] old, GameObject removeComponent)
    {
        GameObject[] n = new GameObject[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new GameObject[0];
        }
        return n;
    }

    public Rigidbody2D[] Add(Rigidbody2D[] old, Rigidbody2D addComponent)
    {
        Rigidbody2D[] n = new Rigidbody2D[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public Rigidbody2D[] Remove(Rigidbody2D[] old, Rigidbody2D removeComponent)
    {
        Rigidbody2D[] n = new Rigidbody2D[(int)Stable2(old.Length - 1, 0, old.Length)];
        if (old.Length > 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new Rigidbody2D[0];
        }
        return n;
    }

    public AudioSource[] Add(AudioSource[] old, AudioSource addComponent)
    {
        AudioSource[] n = new AudioSource[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public AudioSource[] Remove(AudioSource[] old, AudioSource removeComponent)
    {
        AudioSource[] n = new AudioSource[(int)Stable2(old.Length - 1, 0, old.Length)];
        if (old.Length > 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new AudioSource[0];
        }
        return n;
    }
}
