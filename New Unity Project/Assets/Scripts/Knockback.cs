using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public int currentHealth;
    public int maxHealth;
    private Player player;
    private attackTrigger trigger;
    public float duration = 0.02f;
    public float power = 3;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    public IEnumerator Knockback1(float knockDuration, float knockPower, Vector3 knockDirection)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(0, 0);
        while (knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockDirection.x * 10, knockDirection.y * knockPower, transform.position.z));
        }

        yield return 0;

    }
    public IEnumerator Knockback2(float knockDuration, float knockPower, Vector3 knockDirection)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(0, 0);
        while (knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockDirection.x * -10, knockDirection.y * knockPower, transform.position.z));
        }

        yield return 0;

    }
    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (player.transform.localScale.x == -1)
        {
            StartCoroutine(Knockback1(duration, power, transform.position));
        }
        if (player.transform.localScale.x == 1)
        {
            StartCoroutine(Knockback2(duration, power, transform.position));
        }

    }
}