using System.Collections.Generic;
using UnityEngine;


public class DamageArea : MonoBehaviour
{

    private Dictionary<Character, float> lastHitTimes = new Dictionary<Character, float>();
    private Character myOwner;
    //private float damageTimer=0f;

    void Start()
    {
        myOwner = GetComponentInParent<Character>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Character victim = other.GetComponent<Character>();

        if (victim == null) return;
        if (victim == myOwner) return;

        bool alreadyInDictionary = lastHitTimes.TryGetValue(victim, out float nextHitTime);
        if (!alreadyInDictionary || Time.time >= nextHitTime)
        {
            ApplyDamage(victim);
        }
    }

    public float FinalDamage()
    {
        return myOwner.stats.GetStat(myOwner.stats.statLookup, Statistic.flatDamage) * ((myOwner.stats.GetStat(myOwner.stats.statLookup, Statistic.increasedDamage)/100)+1);
    }

    void ApplyDamage(Character victim)
    {
        victim.TakeDamage(FinalDamage(), myOwner);
        lastHitTimes[victim] = Time.time + myOwner.stats.GetStat(myOwner.stats.statLookup, Statistic.damageInterval);
        //Debug.Log($"{FinalDamage()} Damage dealt at {Time.time}", this);

        if (victim.isPlayer == false)
        {
            float roll = Random.value;
            //target the attacker 10% of the time
            if (roll < 0.05f)
            {
                Enemy enemy = victim.GetComponentInParent<Enemy>();
                enemy.currentTarget = myOwner;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Character victim = other.GetComponent<Character>();
        if (victim != null && lastHitTimes.ContainsKey(victim))
        {
            lastHitTimes.Remove(victim);
        }
    }
}
