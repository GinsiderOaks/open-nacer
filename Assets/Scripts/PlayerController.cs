using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    // Movement speed multiplier.
    [Range(0f, 400f)]
    public float moveSpeed = 160f;
    // Turn speed multiplier.
    [Range(0f, 200f)]
    public float turnSpeed = 80f;
    // The minimum linear drag.
    [Range(0f, .1f)]
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
    // The rigid body of the player.
    Rigidbody2D rb;
    // The animator of the player.
    Animator animator;

	void Start () {
        // Fetches components from player.
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
	}
	
	void FixedUpdate () {
        // Gets input from player.
        Vector2 moveInput = GetMoveInput ();
        // As the player can't move backwards,
        // the y-component of the input is clamped between [0, inf]
        moveInput.y = Mathf.Max (0f, moveInput.y);

        // If the player is signaling to accelerate,
        // the back burner graphic should appear.
        if (moveInput.y > 0f) {
            animator.SetBool ("Boosting", true);    
        }
        // Otherwise it shouldn't.
        else {
            animator.SetBool ("Boosting", false);
        }

        // Calculate and apply forces.
        float tourque = -moveInput.x * turnSpeed * Time.deltaTime;
        rb.AddTorque (tourque);
        float forwardForce = moveInput.y * moveSpeed * Time.deltaTime;
        rb.AddForce (forwardForce * transform.right);

        // To ensure the player can't move too fast,
        // the drag increases along with the velociy of the player.
        rb.drag = rb.velocity.magnitude * linearDragRate + linearDragMinimum;
        // Same applies for angular drag.
        rb.angularDrag = Mathf.Abs(rb.angularVelocity) 
            * angularDragRate + angularDragMinimum;

        Debug.Log (string.Format ("Velocity: {0}, Speed, {1}, Torque: {2}",
                   rb.velocity,
                   rb.velocity.magnitude,
                   rb.angularVelocity));
    }

    /// <summary>
    /// Get the movement-related input from the player.
    /// The x-component represents the turning, while
    /// the y-component represents the forward speed.
    /// </summary>
    /// <returns>Input axes as Vector2</returns>
    Vector2 GetMoveInput () {
        var input = new Vector2 {
            x = Input.GetAxis ("Horizontal"),
            y = Input.GetAxis ("Vertical")
        };

        return input;
    }
}
