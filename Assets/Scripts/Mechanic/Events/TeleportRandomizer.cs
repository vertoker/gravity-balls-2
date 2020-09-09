using UnityEngine;

/// Триггер для запутывания иерархии телепортов
public class TeleportRandomizer : MonoBehaviour
{
    public Teleport[] outputs;

    public void Awake()
    {
        Randomizer();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Randomizer();
        }
    }

    public void Randomizer()
    {
        Teleport[] outs = new Teleport[outputs.Length];
        int l = outputs.Length;
        for (int i = 0; i < l; i++)
        {
            outs[i] = outputs[outputs.Length - 1 - i];
        }

        for (int i = 0; i < l; i++)
        {
            int j = Random.Range(0, outs.Length - 1);
            Teleport tmp = outs[j];
            outs[j] = outs[i];
            outs[i] = tmp;
            outputs[i].teleportDestination = outs[i];
        }
    }
}
