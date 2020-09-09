using UnityEngine;

/// Скрипт телепорта, нуждающийся в зарядке от лазера
public class TeleportCharging : MonoBehaviour
{
    public Laser laser;
    public Transform tr;
    public Teleport teleportTarget;

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(tr.position, -tr.up, 100f);
        if (hit.transform.gameObject == teleportTarget.gameObject)
        {
            teleportTarget.active = true;
            teleportTarget.sr.color = laser.LaserColor();
        }
        else if (hit.transform.tag == "Teleport")
        {
            teleportTarget.active = false;
            teleportTarget.sr.color = new Color(0.25f, 0.25f, 0.25f, 1f);
            teleportTarget = hit.transform.GetComponent<Teleport>();
            teleportTarget.active = true;
            teleportTarget.sr.color = laser.LaserColor();
        }
        else
        {
            teleportTarget.active = false;
            teleportTarget.sr.color = new Color(0.25f, 0.25f, 0.25f, 1f);
        }
    }
}