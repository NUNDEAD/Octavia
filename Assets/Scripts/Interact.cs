using UnityEngine;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{
    public Vector2 boxSize;
    public LayerMask boxLayer;

    public void InteractWith(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
            return;


        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.zero, 1, boxLayer);

        if (hit && hit.collider.TryGetComponent(out Intercatable interactable))
        {
            interactable.onInteract.Invoke();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}