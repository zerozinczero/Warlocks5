using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth {

    public UnityEvent OnDeath;

    #region IDamageable

    [SerializeField]
    private float current = 100f;
    public float Current {
        get { return current; }
    }

    [SerializeField]
    private float max = 100f;
    public float Max { get { return max; } set { max = value; } }

    public float Percent { get { return current / max; } }

    public bool IsAlive { get { return current > 0f; } }
    public bool IsDead { get { return current <= 0f; } }


    public void Damage(IDamageInfo damageInfo) {

        if (IsDead) {
            return;
        }

        float actualDamage = Mathf.Min(current, damageInfo.Amount);
        float endHealth = current - actualDamage;

        DamageEvent damageEvent = new DamageEvent(this, damageInfo);

        if (Equals(actualDamage, 0f)) {
            return;
        }

        damageEvent.HealthChange = -actualDamage;
        damageEvent.EndHealth = endHealth;

        ApplyHealthChange(damageEvent);

        if (IsDead && OnDeath != null) {
            OnDeath.Invoke();
        }
    }

    public void SetStats(IHealthStatsInfo stats) {

        HealthStatsEvent healthStatsEvent = new HealthStatsEvent(this, stats);
        healthStatsEvent.EndHealth = Mathf.Min(stats.Current, stats.Max);   // enforce that health cannot be greater than max health

        ApplyHealthChange(healthStatsEvent);

    }


    #endregion

    [System.Serializable]
    public class HealthEventCallback : UnityEvent<IHealthEvent> {}

    [SerializeField]
    private HealthEventCallback onHealthEvent = null;
    public HealthEventCallback OnHealthEvent { get { return onHealthEvent; } }

    #region Helpers

    private void ApplyHealthChange(IHealthEvent healthEvent) {
        
        max = healthEvent.EndMaxHealth;
        current = healthEvent.EndHealth;

        if(onHealthEvent != null) {
            onHealthEvent.Invoke(healthEvent);
        }

    }

    #endregion
}
