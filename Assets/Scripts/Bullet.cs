using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _bouceForce;
    [SerializeField] private float _bounceRadiuse;
    [SerializeField] private float _timeDestroy;


    private Vector3 _moveDirection;
    public event UnityAction<Bullet> BulletBounce;


    private void Start()
    {
        _moveDirection = Vector3.forward;
        StartCoroutine(DestroyCorontine());
    }

    private void Update()
    {
        transform.Translate(_moveDirection * (_speed * -1) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Block block))
        {
            block.Break();
            Destroy(gameObject);
        }

        if(other.TryGetComponent(out Obstacle obstacle))
        {
            Bounce(); 
        }
    }

    private void Bounce()
    {
        BulletBounce?.Invoke(this);
        _moveDirection = Vector3.back + Vector3.down;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(_bouceForce, transform.position + new Vector3(0, -1, 1), _bounceRadiuse);
        
    }

    private IEnumerator DestroyCorontine()
    {
        yield return new WaitForSeconds(_timeDestroy);
        Destroy(gameObject);
    }

}
