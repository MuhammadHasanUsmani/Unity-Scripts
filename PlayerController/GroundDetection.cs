using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    // Add these references in the hierarchy for Gizmos in the Scene view.
    [SerializeField] CapsuleCollider Collider;
    [SerializeField] Rigidbody Rigidbody;

    #region Game

    private void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Can only jump when you're grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Jump
            Rigidbody.AddForce(Vector3.up * 13.5f, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks if this GameObject's Collider is standing on another Collider. 
    /// </summary>
    bool IsGrounded()
    {
        // Check for ground
        if (Physics.SphereCast(transform.position, Collider.radius, -transform.up, out RaycastHit hit, Collider.height / 4))
        {
            if(hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Debug

    /// <summary>
    /// The center point for the ground detection sphere's Debug Gizmo.
    /// </summary>
    Vector3 GroundSphereCenter => transform.position - transform.up * Collider.height / 4;

    private void OnDrawGizmos()
    {
        // The sphere is green when you're on the ground, but red when you leave the ground.
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(GroundSphereCenter, Collider.radius);
    }

    #endregion
}
