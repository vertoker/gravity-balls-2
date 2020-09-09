using UnityEngine;

/// Скрипт для управления физикой и эффектами мёртвого игрока
public class DeadPlayerManagement : MonoBehaviour
{
    public GameObject[] childs;
    public TrailRenderer[] trs;
    public Rigidbody2D[] rbs;
    private int trsl;
    private int rbsl;
    
    private void Awake()
    {
        trsl = trs.Length;
        rbsl = rbs.Length;
    }

    public void SetAwake(bool high, Vector2 velocity)
    {
        for (int x = 0; x < trsl; x++)
        { trs[x].enabled = high; }
        for (int x = 0; x < rbsl; x++)
        { rbs[x].velocity = velocity; }
    }
}