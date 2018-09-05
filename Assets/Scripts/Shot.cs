using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shot : MonoBehaviour {

    // Time in seconds before a shot will hit the thing that shot the shot.
    // This is done so a shot doesn't hit its owner immidietly.
    const float graceTime = .2f;

    public float speed = 20f;
    public float maxLifetime = 10f;

    // The person who shot the shot.
    [HideInInspector]
    public SpaceCraft owner;

    Rigidbody2D rb;
    float lifetime;

    void Start () {
        rb = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime) {
            Destroy (gameObject);
        }
    }

    private void FixedUpdate () {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        // Did the shot hit the owner?
        bool ownerHit = owner != null && owner.gameObject == collision.gameObject;
        if (!(lifetime < graceTime && ownerHit)) {
            Impact (collision);
        } 
    }

    public abstract void Impact (Collider2D collision);
}
