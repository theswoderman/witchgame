using UnityEngine;
using System.Collections.Generic;

public class Enemy : Character
{
    public Character playerCharacter;
    public float detectionRadius = 5f;
    public Character currentTarget;
    public float stopDistance;
    public float retargetInterval = 3f;
    private float retargetTimer;
    public int expGranted;
    public float powerGranted = 0.1f;

    protected override void Start()
    {
        base.Start();
        playerCharacter = PlayerController.Instance;
        isPlayer = false;
        DecideNextTarget();
    }

    protected override void Update()
    {
        base.Update();
        retargetTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (retargetTimer <= 0 || currentTarget == null)
        {
            DecideNextTarget();
            retargetTimer = retargetInterval;
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        if (isAlive != true) return;
        //calculate direction
        Vector2 direction = (currentTarget.transform.position - transform.position);

        //check distance so they don't overlap perfectly
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

        if (distance > stopDistance)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void DecideNextTarget()
    {
        //Find every character within the detection radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        List<Character> potentialTargets = new List<Character>();

        foreach (var hit in hits)
        {
            Character c = hit.GetComponent<Character>();

            //add character if it isn't the enemy that is looking for a target
            if (c != null && c != this)
            {
                potentialTargets.Add(c);
            }
        }
        if (potentialTargets.Count == 0) { return; }
        // RNG
        float roll = Random.value;

        //creates a 'player' variable so we can check if the player is a valid target
        Character player = potentialTargets.Find(x => x.CompareTag("Player"));

        //target the player 90% of the time
        if (player != null && roll < 0.9f)
        {
            currentTarget = playerCharacter;
        }
        else if (roll > 0.8f)
        {
            int randomIndex = Random.Range(0, potentialTargets.Count);
            currentTarget = potentialTargets[randomIndex];
        }
    }

    protected override void Die()
    {
        Vector3 transformForGrowth = new Vector3(transform.localScale.x, transform.localScale.y,1);
        lastAttacker.Grow(transformForGrowth);

        if (!lastAttacker.isPlayer)
        {
            lastAttacker.FullHeal();
        }
        else
        {
            lastAttacker.Heal(characterHealth.max * 0.2f);
        }
        lastAttacker.PowerUp(this);
        gameController.OnEnemyDeath(this, lastAttacker);
        base.Die();
    }
}
