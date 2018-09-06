using UnityEngine;

/// <summary>
/// Abstract class for space ship-like entities.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class SpaceCraft : MonoBehaviour, IHealth {

    public string craftName;
    public float maxHealth = 100f;

    // Movement speed multiplier.
    [Range (0f, 400f)]
    public float moveSpeed = 160f;
    // Turn speed multiplier.
    [Range (0f, 200f)]
    public float turnSpeed = 80f;
    // The minimum linear drag.
    [Range (0f, .1f)]
    public float linearDragMinimum = .01f;
    // The minimum angular drag.
    [Range (0f, .1f)]
    public float angularDragMinimum = .05f;
    // Drag increases proportionally to movement speed. This is the proportionality constant.
    [Range (0f, .01f)]
    public float linearDragRate = .003f;
    // Drag increases proportionally to angularspeed. This is the proportionality constant.
    [Range (0f, .01f)]
    public float angularDragRate = .003f;

    public AbilityTypes abilityType;
    [Range (1f, 20f)]
    public float abilityCooldown;
    public float abilityPower = 1f;

    // The rigid body of the space craft.
    public Rigidbody2D Rb { get; private set; }
    public float Health { get; private set; }

    internal float _abilityCooldown;
    internal bool canUseAbility = true;

    internal virtual void Start () {
        Health = maxHealth;
        Rb = GetComponent<Rigidbody2D> ();

        _abilityCooldown = abilityCooldown;
    }

    internal virtual void Update () {
        if (_abilityCooldown >= abilityCooldown) {
            _abilityCooldown = abilityCooldown;
            canUseAbility = true;
        }
        else {
            _abilityCooldown += Time.deltaTime;
        }
    }

    internal bool TryUseAbility () {
        if (canUseAbility) {
            canUseAbility = false;
            _abilityCooldown = 0f;
            Abilities.Ability ability = Abilities.GetAbility (abilityType);
            ability (this, abilityPower);
            return true;
        }
        return false;
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
