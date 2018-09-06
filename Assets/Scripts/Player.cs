using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SpaceCraft {

    // The object responsible for shooting.
    public Cannon cannon;
    // The minimum amount of seconds between shots.
    [Range(.1f, 5f)]
    public float shotCoolDown;

    // The animator of the player.
    Animator animator;

    float _shotCoolDown;
    bool canShoot = true;

    internal override void Start () {
        base.Start ();

        // Fetches components from player.
        animator = GetComponent<Animator> ();

        _shotCoolDown = shotCoolDown;
	}

    internal override void Update () {
        base.Update ();

        if (_shotCoolDown >= shotCoolDown) {
            _shotCoolDown = shotCoolDown;
            canShoot = true;
        }
        else {
            _shotCoolDown += Time.deltaTime;
        }

        bool shoot = Input.GetKey (KeyCode.Mouse0);
        bool useAbility = Input.GetKey (KeyCode.Space);

        if (shoot) {
            TryShoot ();
        }

        if (useAbility) {
            TryUseAbility ();
        }
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
        float tourque = -moveInput.x * turnSpeed * Time.fixedDeltaTime;
        Rb.AddTorque (tourque);
        float forwardForce = moveInput.y * moveSpeed * Time.fixedDeltaTime;
        Rb.AddForce (forwardForce * transform.right);

        // To ensure the player can't move too fast,
        // the drag increases along with the velociy of the player.
        Rb.drag = Rb.velocity.magnitude * linearDragRate + linearDragMinimum;
        // Same applies for angular drag.
        Rb.angularDrag = Mathf.Abs(Rb.angularVelocity) 
            * angularDragRate + angularDragMinimum;

        
        Debug.Log (string.Format ("Velocity: {0}, Speed, {1}, Torque: {2}, Health: {3}.",
                   Rb.velocity,
                   Rb.velocity.magnitude,
                   Rb.angularVelocity,
                   Health));
    }

    bool TryShoot () {
        if (canShoot) {
            canShoot = false;
            _shotCoolDown = 0f;
            cannon.Shoot (this);
            return true;
        }
        return false;
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

    public override void Die() {
        print ("Player died.");
    }
}
