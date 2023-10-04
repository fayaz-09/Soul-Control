using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Projectile : PoolableObject
{
    public float destroyTime = 5f;
    public float moveSpeed = 7f;
    public float damage = 5f;
    public Rigidbody Rigidbody;

    private const string DISABLE_METHOD_NAME = "OnDisable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().takeDamage(damage);
            Debug.Log("hit");
        }

        OnDisable();

    }

    public override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke(DISABLE_METHOD_NAME);
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
