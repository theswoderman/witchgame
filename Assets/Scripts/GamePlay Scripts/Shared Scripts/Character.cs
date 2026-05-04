using UnityEngine;
using System;


[RequireComponent(typeof(StatHandler))]
public class Character : MonoBehaviour
{
    public ValuePool characterHealth;
    [SerializeField]
    protected Vector3 targetScale;
    [HideInInspector]
    public StatHandler stats;
    [HideInInspector]
    public float growthSpeed = 5;
    public float moveSpeed;
    protected Rigidbody2D rb;
    public Character lastAttacker;
    [HideInInspector]
    public bool isPlayer;
    [HideInInspector]
    public bool isAlive;
    protected GameObject deathEffect;
    protected Animator anim;
    [HideInInspector]
    public GameController gameController;

    protected virtual void Awake()
    {
        isAlive = true;
        stats = GetComponent<StatHandler>();
        targetScale = transform.localScale;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        rb.mass = transform.localScale.x;
        gameController = GameController.Instance;
        //set health values
        characterHealth.max = stats.GetStat(stats.statLookup,Statistic.addedHealth) * (1+(stats.GetStat(stats.statLookup, Statistic.increasedHealth)/100));
        characterHealth.current = stats.GetStat(stats.statLookup, Statistic.addedHealth) * (1 + (stats.GetStat(stats.statLookup, Statistic.increasedHealth) / 100));
        characterHealth.regen = stats.GetStat(stats.statLookup, Statistic.healthRegen);
    }

    public void TakeDamage(float damage, Character attacker)
    {
        characterHealth.AdjustCurrent(-damage);
        lastAttacker = attacker;  // need this so that we can apply on death effects to the killer
        CheckDeath();
    }

    public void Heal(float healing)
    {
        characterHealth.current += healing;
    }

    public void FullHeal()
    {
        characterHealth.current = characterHealth.max;
    }

    public void PowerUp(Character victim)
    {
        stats.GrowStat(Statistic.addedHealth, victim.stats.GetStat(victim.stats.statLookup, Statistic.addedHealth) * 0.1f);
        stats.GrowStat(Statistic.flatDamage, victim.stats.GetStat(victim.stats.statLookup, Statistic.flatDamage) * 0.1f);
        stats.GrowStat(Statistic.increasedDamage, victim.stats.GetStat(victim.stats.statLookup, Statistic.increasedDamage) * 0.1f);
    }

    public void Grow(Vector3 enemyTransform)
    {
        if (targetScale.x < 0)
        {
            float growthAmount = enemyTransform.x * 0.1f;
            targetScale = new Vector3(
                targetScale.x - growthAmount,
                targetScale.y + growthAmount,
                1);
        }
        else
        {
            float growthAmount = enemyTransform.x * 0.1f;
            targetScale = new Vector3(
                Math.Abs(targetScale.x) + growthAmount,
                Math.Abs(targetScale.y) + growthAmount,
                1);
        }
    }

    void CheckDeath()
    {
        if (characterHealth.current <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isAlive = false;
        Destroy(gameObject);     
    }

    protected virtual void Update()
    {
        //grow
        if (Math.Abs(transform.localScale.x) < Math.Abs(targetScale.x))
        {
            transform.localScale = Vector3.MoveTowards(
                transform.localScale,
                targetScale,
                growthSpeed * Time.deltaTime);
        }

        //scale mass
        if (rb != null)
        {
            if (rb.mass < transform.localScale.x)
            {
                rb.mass = transform.localScale.x;
            }
        }

        //animate
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x) + Mathf.Abs(rb.linearVelocity.y);
        anim.SetFloat("Speed", currentSpeed);
        if (!isAlive)
        {
            anim.SetBool("IsDead", !isAlive);
        }

        //regen
        characterHealth.Update();
    }
}
