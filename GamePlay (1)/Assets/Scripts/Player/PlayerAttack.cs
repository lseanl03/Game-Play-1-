using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.7f;
    public int attackDamage = 2;
    public float timeBetweenAttacks = 0.7f;
    public float nextTimeAttack = 0f;
    public bool isDied;
    private void Start()
    {
        this.animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!this.isDied)
        {
            if (Time.time >= nextTimeAttack)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.Attack();
                    nextTimeAttack = Time.time + timeBetweenAttacks;
                }
            }
        }

    }
    public void Died()
    {
        isDied = true;
    }
    void Attack()
    {
        this.animator.SetTrigger("Attack");
        Invoke("TakeDamage", 0.15f);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void TakeDamage()
    {
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(attackDamage);
        }
    }
}
