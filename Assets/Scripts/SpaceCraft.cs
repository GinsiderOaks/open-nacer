using UnityEngine;

/// <summary>
/// Abstract class for space ship-like entities.
/// </summary>
public abstract class SpaceCraft : MonoBehaviour, IHealth {

    public string craftName;
    public float maxHealth = 100f;

    public float Health { get; private set; }

    public virtual void Start () {
        Health = maxHealth;
    }

    public virtual void TakeDamage (float damage) {
        Health = Health - damage;
        Health = Mathf.Min (maxHealth, Health);

        if (Health <= 0f) {
            Die ();
        }
    }

    public virtual void Die () {
        print ("Player died.");
    }
}
