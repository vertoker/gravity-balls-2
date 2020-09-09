using UnityEngine;

/// Тип лазера
public class TypeLaser : MonoBehaviour
{
    public enum typeIntecration { Damage = 1, Heal = 2, SlowMo = 3, Immortality = 4, Gravity = 5, Mass = 6};
    public typeIntecration ti = typeIntecration.Damage;
    public float input = 3f;

    public int Type2int()
    {
        int ret = 1;
        switch (ti)
        {
            case typeIntecration.Damage:
                ret = 1;
                break;
            case typeIntecration.Heal:
                ret = 2;
                break;
            case typeIntecration.SlowMo:
                ret = 3;
                break;
            case typeIntecration.Immortality:
                ret = 4;
                break;
            case typeIntecration.Gravity:
                ret = 5;
                break;
            case typeIntecration.Mass:
                ret = 6;
                break;
        }
        return ret;
    }
}