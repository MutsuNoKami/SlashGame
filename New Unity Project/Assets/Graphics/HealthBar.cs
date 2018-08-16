using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Scrollbar healthBar;
    public float health2 = 100;

    public void Damage(float value)
    {
        health2 -= value;
        healthBar.size = health2 / 100f;
    }
}
