using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class TopDownMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
   
    private float currentSpeed;
    private Vector2 movement;
    private Rigidbody2D rb2D;
    
    void Awake()
    {
       rb2D = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.linearVelocity = movement * currentSpeed;  
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }


    public void Run(InputAction.CallbackContext ctx)
    {
        if(ctx.ReadValue<float>() == 1) //Presssed
        {
            currentSpeed = runSpeed;
        }
        else //Released
        {
            currentSpeed = walkSpeed;
        }
    }
}
