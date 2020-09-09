using UnityEngine;

/// Скрипт на объект, который может быть потвержен действию силового поля
public class VelocityInput : MonoBehaviour
{
    public bool inVelocityField = false;
    public GameObject[] fields = new GameObject[0];
    public GameObject[] attractors = new GameObject[0];
}