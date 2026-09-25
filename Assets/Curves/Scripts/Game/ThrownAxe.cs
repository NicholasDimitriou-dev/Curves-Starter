using System;
using UnityEngine;

/*
 * ThrownAxe keeps the axe's held pose so it can be attached to the hand
 * after a throw. Launch physics and collision response belong here;
 * PlayerController decides when to throw and recall it.
 */

public class ThrownAxe : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Collider axeCollider;
    public ParticleSystem axeParticles;
    public float spinspeed = 1000f;
    bool _stuck;
    Transform _hand;
    Vector3 _heldLocalPosition;
    Quaternion _heldLocalRotation;
    
    public TrailRenderer tr;

    private void Awake()
    {
        axeParticles = GetComponent<ParticleSystem>();
    }

    
    // on its own, separate from the physics root. Keep its look and collision the same.
    // Check: the held axe looks the same, and throw and catch still work.
    // Next: Slice 8.2 below.

    
    // Pick an axis and speed that suit the mesh. Leave the root's rotation to physics.
    // Next: Slice 8.3 at the hooks below and in PlayerController.ReturnAxe.

    public Vector3 CatchPosition => _hand.TransformPoint(_heldLocalPosition);

    public void Launch(Vector3 direction, float impulse, CharacterController thrower)
    {
        _hand = transform.parent;
        _heldLocalPosition = transform.localPosition;
        _heldLocalRotation = transform.localRotation;
        //
        transform.SetParent(null);
        tr.enabled = true;
        Physics.IgnoreCollision(axeCollider, thrower);
        transform.position += direction * 0.5f;
        
        rigidbody.isKinematic = false;
        axeCollider.enabled = true;
        
        
        rigidbody.AddForce(direction * impulse, ForceMode.Impulse);
        rigidbody.AddRelativeTorque(transform.forward * (spinspeed * -1 * Mathf.Deg2Rad), ForceMode.VelocityChange);
        
    }

    public void AttachToHand()
    {
        transform.SetParent(_hand);
        transform.SetLocalPositionAndRotation(_heldLocalPosition, _heldLocalRotation);
        rigidbody.isKinematic = true;
        axeCollider.enabled = false;
        //_stuck = false;
        tr.enabled = false;
        
        // Check: throw and recall both spin. Two full cycles end with the original held look.
        // Next: polish, networking, and your showcase video. </> end of Slice 8
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_stuck) return;
        
        rigidbody.isKinematic = true;
        tr.enabled = false;
        axeParticles.Play();
        
    }
}
