using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shot : MonoBehaviour {

    public float speed = 20f;
    public float lifetime = 10f;

    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D> ();	
	}
	
	void Update () {
        rb.velocity = transform.right * speed;

        lifetime -= Time.deltaTime;
        if (lifetime < 0f) {
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        Impact (collision);    
    }

    public abstract void Impact (Collider2D collision);
}
