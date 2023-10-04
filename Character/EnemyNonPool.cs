using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNonPool : EntityNonPool
{
    protected Animator EnemyAnims;
    public Transform[] waypoints;
    //public Transform testPoint;
    public NavMeshAgent m_EnemyNavAgent;

    protected int m_currentWaypoint = 0;
    protected float m_maxWait = 2f;
    protected float m_waitTime = 0f;
    protected bool m_waiting = false;

    protected float attackTime = 1f;
    protected float attackCounter = 0f;

    public float minStrafeTime;
    protected float currentStrafeTime = 0f;
    protected bool isStrafing = false;
    protected float strafePos;
    public bool canAttack;

    [SerializeField] private ProgressBar HealthBar;
    private float hitCounter = 0f;

    protected bool tookHit;
    public bool isMoving => m_EnemyNavAgent.velocity.magnitude > float.Epsilon;

    // Start is called before the first frame update
    public virtual void Start()
    {
        moveSpeed = 5.5f;
        maxHealth = 40f;
        currentHealth = maxHealth;
        isAlive = true;
        tookHit = false;
        canAttack = true;
        strafePos = Random.Range(1, 8);
        minStrafeTime = Random.Range(7, 15);
        m_EnemyNavAgent = GetComponent<NavMeshAgent>();
        m_EnemyNavAgent.speed = moveSpeed;
        EnemyAnims = GetComponent<Animator>();

        if (waypoints[0] == null)
        {
            waypoints[0] = transform;
            waypoints[0].position.Set(
            transform.position.x + 100,
            transform.position.y + 0.5f,
            transform.position.z + 10);

            waypoints[1] = transform;
            waypoints[1].position.Set(
            transform.position.x - 100,
            transform.position.y + 0.5f,
            transform.position.z - 10);
        }
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (isAlive == false)
        {
            Die();
        }

        if (isStrafing)
        {
            currentStrafeTime += Time.deltaTime;
            m_EnemyNavAgent.speed = 2.5f;
            m_EnemyNavAgent.updateRotation = false;
        }
        else
        {
            m_EnemyNavAgent.speed = moveSpeed;
            m_EnemyNavAgent.updateRotation = true;
        }

        if (EnemyAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.1f && EnemyAnims.GetCurrentAnimatorStateInfo(0).IsName("EnemySwordAttack"))
        {
            m_EnemyNavAgent.isStopped = false;
            hitCounter = 0;
            canAttack = false;
            currentStrafeTime = 0;
            EnemyAnims.SetBool("isAttacking", false);
        }

        if (EnemyAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && EnemyAnims.GetCurrentAnimatorStateInfo(0).IsName("takeHit"))
        {
            EnemyAnims.SetBool("isAttacking", false);
            //EnemyAnims.SetBool("tookDamage", false);
        }


        if (m_EnemyNavAgent.velocity.magnitude >= 3f)
        {
            EnemyAnims.SetBool("isMoving", true);
            EnemyAnims.SetBool("isStrafe", false);
        }
        else if (m_EnemyNavAgent.velocity.magnitude >= 2f && m_EnemyNavAgent.velocity.magnitude < 3f)
        {
            EnemyAnims.SetBool("isStrafe", true);
            EnemyAnims.SetBool("isMoving", false);
        }
        else
        {
            EnemyAnims.SetBool("isStrafe", false);
            EnemyAnims.SetBool("isMoving", false);
        }
    }

    public void damageAnim()
    {
        tookHit = true;
        EnemyAnims.SetTrigger("tookDamage");
    }


    public void Die()
    {
        Debug.Log("Enemy slain");
        Destroy(HealthBar.gameObject);
        gameObject.SetActive(false);
    }
    public override void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }

        HealthBar.setProgress(currentHealth / maxHealth, 3);
    }

    public override void gainHealth(int gained)
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

        HealthBar.setProgress(currentHealth / maxHealth, 3);
    }

    public void setUpHealthBar(Canvas canvas, Camera camera)
    {
        HealthBar.transform.SetParent(canvas.transform);
        if (HealthBar.TryGetComponent<FaceTarget>(out FaceTarget faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

    public bool checkWithinRange(float range, LayerMask targetLayer)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform getTargetInRange(float range, LayerMask targetLayer)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayer);
        if (colliders.Length > 0)
        {
            return colliders[0].transform;
        }
        else
        {
            return null;
        }
    }

    public float getDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public void attack(Player player)
    {
        Debug.Log("Player took damage");
        player.takeDamage(10f);
    }

    public void attackTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            m_EnemyNavAgent.isStopped = true;
        }
        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            EnemyAnims.SetBool("isAttacking", true);
            attackCounter = 0f;
        }
    }

    public void chaseTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            m_EnemyNavAgent.destination = target.position;
        }
    }

    public void patrol()
    {
        if (m_waiting)
        {
            m_waitTime += Time.deltaTime;
            if (m_waitTime >= m_maxWait)
            {
                m_waiting = false;
            }
        }
        else
        {
            Transform wp = waypoints[m_currentWaypoint];
            if (Vector3.Distance(transform.position, wp.position) <= 3f)
            {
                m_waitTime = 0f;
                m_waiting = true;

                m_currentWaypoint = (m_currentWaypoint + 1) % waypoints.Length;
                Debug.Log(m_currentWaypoint);
            }
            else
            {
                m_EnemyNavAgent.destination = wp.position;
            }
        }
    }

    public void strafeAroundTarget(Transform target, float dist)
    {
        Vector3 dest = new Vector3(
            target.position.x + dist * Mathf.Cos(2 * Mathf.PI * strafePos / 8),
            target.position.y + 0.5f,
            target.position.z + dist * Mathf.Sin(2 * Mathf.PI * strafePos / 8));

        if (Vector3.Distance(transform.position, dest) < 2.5f)
        {
            if (strafePos <= 8) strafePos += 0.5f;
            else strafePos = 1;
        }

        Vector3 targetLook = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetLook);
        m_EnemyNavAgent.destination = dest;
        //testPoint.position = dest;
    }

    public float getCurrentStrafeTime()
    {
        return currentStrafeTime;
    }

    public void setStrafeTrue()
    {
        isStrafing = true;
    }
    public void setStrafeFalse()
    {
        isStrafing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.tag == "Player")
        {
            attack(other.GetComponent<Player>());
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

}
