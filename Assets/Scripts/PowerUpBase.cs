using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField] float powerupDuration = 3f;

    bool powerupActive = false;

    protected abstract void PowerUp();
    protected abstract void PowerDown();

    [SerializeField] ParticleSystem _powerupParticles;
    [SerializeField] AudioClip _powerupSound;

    [SerializeField] Collider _collider;
    [SerializeField] MeshRenderer _meshRenderer;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUp();
        powerupActive = true;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        Feedback();
        
    }

    private void Update()
    {
        if(powerupActive)
        {
            powerupDuration -= Time.deltaTime;

            if (powerupDuration <= 0f)
            {
                durationEnded();
            }
        }
    }

    private void durationEnded()
    {
        powerupDuration = 3f;
        PowerDown();
        powerupActive = false;
    }

    private void Feedback()
    {
        //particles
        if (_powerupParticles != null)
        {
            _powerupParticles = Instantiate(_powerupParticles, transform.position, Quaternion.identity);
            _powerupParticles.Play();
        }
        //audio. TODO - consider Object Pooling for performance
        if (_powerupSound != null)
        {
            AudioHelper.PlayClip2D(_powerupSound, 1f);
        }
    }
}