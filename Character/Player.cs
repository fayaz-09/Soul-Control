using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected float jumpHeight;
    [SerializeField] private ProgressBar HealthBar;
    public float maxHealth;
    protected float currentHealth;
    protected bool isAlive;
    protected float moveSpeed;
    //private Animator playerAnims;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 6f;
        jumpHeight = 3f;
        //maxHealth = 40f;
        currentHealth = maxHealth;
        isAlive = true;
        //playerAnims = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButton("Fire1"))
        //{
        //    playerAnims.SetTrigger("isAttacking");
        //}

        if(currentHealth <= 0)
        {
            isAlive = false;
        }
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

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }

        HealthBar.setProgress(currentHealth / maxHealth, 3);
    }

    public void gainHealth(int gained)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += gained;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            HealthBar.setProgress(currentHealth / maxHealth, 3);
        }
        else
        {
            Debug.Log("Health Full");
        }

    }
    public float getJump()
    {
        return jumpHeight;
    }

    public void attack(Enemy attackEnemy)
    {
        attackEnemy.damageAnim();
        attackEnemy.takeDamage(10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            attack(other.GetComponent<Enemy>());
        }
    }

    


}
