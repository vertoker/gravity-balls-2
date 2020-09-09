using UnityEngine;
/// Определение состояния босса
public class TriggerStateBoss3 : MonoBehaviour
{
    public bool LaserTarget = true;
    public bool LaserMover = true;
    public bool TrapsMover = true;
    public bool SawMover = true;
    public bool SawsAroundMover = true;
    public BossManagement3 bm3;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bm3.ControlDamagers(LaserTarget, LaserMover, TrapsMover, SawMover, SawsAroundMover);
        }
    }
}
