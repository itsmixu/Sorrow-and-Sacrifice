using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;

    private Animator animator;
    private enum Direction { Down, Up, Left, Right }
    private Direction lastYDirection = Direction.Down;
    private Direction lastXDirection = Direction.Right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    
    private void UpdateAnimation()
    {
        if (input != Vector2.zero)
        {
            // Moving on the x-axis and down
            if (input.x != 0 && input.y < 0)
            {
                lastYDirection = Direction.Down;

                if (input.x > 0)
                {
                    animator.Play("WalkRight");
                    lastXDirection = Direction.Right;
                }
                else if (input.x < 0)
                {
                    animator.Play("WalkLeft");
                    lastXDirection = Direction.Left;
                }
            }

            // Moving on the x-axis and up
            else if (input.x != 0 && input.y > 0)
            {
                lastYDirection = Direction.Up;

                if (input.x > 0)
                {
                    animator.Play("WalkUpR");
                    lastXDirection = Direction.Right;
                }
                else if (input.x < 0)
                {
                    animator.Play("WalkUpL");
                    lastXDirection = Direction.Left;
                }
            }

            // If not moving on x-axis
            else if (input.x == 0)
            {
                if (input.y < 0)
                {
                    lastYDirection = Direction.Down;

                    switch (lastXDirection)
                    {
                        case Direction.Right:
                            animator.Play("WalkRight");
                            break;
                        case Direction.Left:
                            animator.Play("WalkLeft");
                            break;
                    }
                }

                else if (input.y > 0)
                {
                    lastYDirection = Direction.Up;

                    switch (lastXDirection)
                    {
                        case Direction.Right:
                            animator.Play("WalkUpR");
                            break;
                        case Direction.Left:
                            animator.Play("WalkUpL");
                            break;
                    }
                }
            }
        }
        else
        {
            // Idle animation
            switch (lastYDirection)
            { 
                case Direction.Up:
                    animator.Play("UpIdle");
                    break;
                case Direction.Down:
                    animator.Play("DownIdle");
                    break;
            }
        }
    }
}
