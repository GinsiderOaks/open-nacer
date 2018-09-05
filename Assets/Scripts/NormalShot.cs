using UnityEngine;

public class NormalShot : Shot {

    public float damage;

    public override void Impact(Collider2D collision) {
        IHealth healthObject = collision.GetComponent<IHealth>();
        print (collision.gameObject.name);
        if (healthObject != null) {
            healthObject.TakeDamage (damage);
        }

        Destroy (gameObject);
    }
}
