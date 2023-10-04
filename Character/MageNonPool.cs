using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageNonPool : EnemyNonPool
{
    [SerializeField] public Transform projectileSpawnPoint;
    [SerializeField] public Projectile projectilePrefab;
    public LayerMask Mask;
    public ObjectPool ProjectilePool;
    private float sphereCastRadius = 0.1f;
    private RaycastHit Hit;
    private Projectile projectile;

    public bool canHeal = false;
    public float healCooldown = 10f;
    private float CurrentHealTime = 0f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        ProjectilePool = ObjectPool.createInstance(projectilePrefab, 3);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (CurrentHealTime >= healCooldown)
        {
            canHeal = true;
        }
        else
        {
            CurrentHealTime += Time.deltaTime;
        }

        if (EnemyAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.5f && EnemyAnims.GetCurrentAnimatorStateInfo(0).IsName("MageHeal"))
        {
            m_EnemyNavAgent.isStopped = false;
            currentStrafeTime = 0;
            EnemyAnims.SetBool("isHealing", false);
        }
    }

    public void rangeAttack(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 10f)
        {
            m_EnemyNavAgent.isStopped = true;
        }
        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            //EnemyAnims.SetBool("RangeAttack", true);
            attackCounter = 0f;

            EnemyAnims.SetBool("isAttacking", true);
            PoolableObject poolableObject = ProjectilePool.GetObject();
            if (poolableObject != null)
            {
                projectile = poolableObject.GetComponent<Projectile>();

                projectile.damage = 5f;
                projectile.transform.position = projectileSpawnPoint.position;
                projectile.transform.rotation = transform.rotation;
                projectile.Rigidbody.AddForce(transform.forward * projectilePrefab.moveSpeed, ForceMode.VelocityChange);
                Debug.Log("Projectile shot");
            }


        }
    }

    private bool hasLineOfSight(Transform Target, float range)
    {
        if (Physics.SphereCast(projectileSpawnPoint.position, sphereCastRadius, ((Target.position) - (projectileSpawnPoint.position).normalized), out Hit, range, Mask))
        {
            return Hit.collider.GetComponent<Player>() != null;
        }

        return false;
    }

    public void healAlly(GameObject ally)
    {
        if (ally.GetComponent<Enemy>() != null)
        {
            m_EnemyNavAgent.isStopped = true;
            EnemyAnims.SetBool("isHealing", true);
            Enemy en = ally.GetComponent<Enemy>();
            en.gainHealth(10);
            CurrentHealTime = 0;
            canHeal = false;
        }
    }

    public bool allyCheck(float range, LayerMask targetLayer)
    {
        List<GameObject> allies = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayer);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Enemy>() != null && gameObject != colliders[i].gameObject)
                {
                    allies.Add(colliders[i].gameObject);
                }
            }
            if (allies.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public List<GameObject> getAlliesInRange(float range, LayerMask targetLayer)
    {
        List<GameObject> allies = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayer);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Enemy>() != null && gameObject != colliders[i].gameObject)
                {
                    allies.Add(colliders[i].gameObject);
                }
            }
            return allies;
        }
        else
        {
            return null;
        }
    }

    public void healLowest(List<GameObject> allies)
    {
        GameObject lowestAlly = allies[0];
        for (int i = 0; i < allies.Count; i++)
        {
            if (allies[i].GetComponent<Enemy>().getCurrentHealth() < lowestAlly.GetComponent<Enemy>().getCurrentHealth())
            {
                lowestAlly = allies[i];
            }
        }

        healAlly(lowestAlly);
    }

    public bool checkHealth(List<GameObject> allies)
    {

        GameObject lowestAlly = allies[0];
        for (int i = 0; i < allies.Count - 1; i++)
        {
            if (allies[i].GetComponent<Enemy>().getCurrentHealth() < lowestAlly.GetComponent<Enemy>().getCurrentHealth())
            {
                lowestAlly = allies[i];
            }
        }

        if (lowestAlly.GetComponent<Enemy>().getCurrentHealth() < lowestAlly.GetComponent<Enemy>().getMaxHealth())
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
