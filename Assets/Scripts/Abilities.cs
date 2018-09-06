using System.Collections;
using UnityEngine;

/// <summary>
/// Static class for abilities SpaceCrafts can use.
/// </summary>
public static class Abilities {

    /// <summary>
    /// Delegate for every ability.
    /// </summary>
    /// <param name="craft">The craft that used this ability.</param>
    /// <param name="power">The power level of the ability.</param>
    public delegate void Ability (SpaceCraft craft, float power);

    static Ability[] abilities = {
        None,
        Boost
    };

    /// <summary>
    /// Return an ability method associated with the type given.
    /// </summary>
    /// <param name="type">The type of ability.</param>
    /// <returns>An ability.</returns>
    public static Ability GetAbility(AbilityTypes type) {
        return abilities[(int)type];
    }

    /// <summary>
    /// Empty ability for ships without one.
    /// </summary>
    /// <param name="craft">Redundant parameter.</param>
    /// <param name="power">Redundant parameter.</param>
    public static void None(SpaceCraft craft, float power) {
        return;
    }

    /// <summary>
    /// Boost the ship in its forward (right) direction.
    /// </summary>
    /// <param name="craft">The craft that use this ability.</param>
    /// <param name="power">How strong the boost is.</param>
    public static void Boost(SpaceCraft craft, float power) {
        Rigidbody2D rb = craft.Rb;
        Vector2 force = craft.transform.right * craft.moveSpeed * power;
        rb.AddForce(force);
    }
}

public enum AbilityTypes {
    None,
    Boost
}
