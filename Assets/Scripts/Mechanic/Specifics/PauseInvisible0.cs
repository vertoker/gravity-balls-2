using UnityEngine;

/// Невидимая пауза на 0 уровне
public class PauseInvisible0 : MonoBehaviour
{
    public GameObject im1;
    public GameObject im2;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        im1.SetActive(false);
        im2.SetActive(false);
    }
}
