using UnityEngine;

public class NormalShot : Shot {

    public float damage;

    public override void Impact(Collider2D collision) {
        IHealth healthObject = collision.GetComponent<IHealth>();
        if (healthObject != null) {
            healthObject.TakeDamage (damage);
        }

        Destroy (gameObject);
    }
}
