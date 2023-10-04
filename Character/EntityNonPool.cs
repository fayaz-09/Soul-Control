using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityNonPool : MonoBehaviour
{
    public float maxHealth;
    protected float currentHealth;
    protected bool isAlive;
    protected float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 6;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public bool getAlive()
    {
        return isAlive;
    }
    public virtual void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

    public virtual void gainHealth(int gained)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += gained;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        else
        {
            Debug.Log("Health Full");
        }
    }

}