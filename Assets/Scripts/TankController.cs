using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = .25f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    [SerializeField] float _turnSpeed = 2f;

    [SerializeField] Missile _missile;
    [SerializeField] ParticleSystem _shootParticles;
    [SerializeField] AudioClip _shootSound;

    Rigidbody _rb = null;

    bool reloading = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            FlipTank();
        }

        if(reloading == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                _shootParticles.Play();
                AudioHelper.PlayClip2D(_shootSound, 1f);
                StartCoroutine(Reload());
            }
        }
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _maxSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void FlipTank()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Shoot()
    {
        if(_missile != null)
        {
            Instantiate(_missile, this.gameObject.transform.TransformPoint(Vector3.forward * 1.5f), 
                new Quaternion(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z, this.gameObject.transform.rotation.w));
        }
    }

    private IEnumerator Reload()
    {
        float reloadTime = 0.5f;
        reloading = true;

        while(reloadTime >= 0)
        {
            reloadTime -= Time.deltaTime;
            yield return null;
        }
        reloading = false;
    }
    
}
