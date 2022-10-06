using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 5;
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] AudioClip damageSound;
    [SerializeField] ParticleSystem killParticles;
    [SerializeField] AudioClip killSound;
    [SerializeField] Material _bossBody;
    [SerializeField] Material _bossTread;
    [SerializeField] Material _bossTurret;
    [SerializeField] Image damageVignette;

    protected int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    private float pause = 0.15f;

    public event Action<int> Damaged;

    void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(gameObject.name + " took " + amount + " damage.");
        currentHealth -= amount;
        Debug.Log(gameObject.name + "'s current health is " + currentHealth);
        Damaged?.Invoke(amount);
        if (currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            DamageFeedback();
        }

    }

    public void Kill()
    {
        currentHealth = 0;
        KillFeedback();
        Destroy(gameObject);
    }

    private void DamageFeedback()
    {
        //audio. TODO - consider Object Pooling for performance
        if (damageSound != null)
        {
            AudioHelper.PlayClip2D(damageSound, 1f);
        }
        StartCoroutine(DamageFlash());
    }

    private void KillFeedback()
    {
        //particles
        if (killParticles != null)
        {
            Instantiate(killParticles, this.gameObject.transform.TransformPoint(Vector3.zero), Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (killSound != null)
        {
            AudioHelper.PlayClip2D(killSound, 1f);
        }
    }

    IEnumerator DamageFlash()
    {
        
        if(_bossBody != null && _bossTread != null && _bossTurret != null)
        {
            _bossBody.EnableKeyword("_EMISSION");
            _bossTread.EnableKeyword("_EMISSION");
            _bossTurret.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(pause);
            _bossBody.DisableKeyword("_EMISSION");
            _bossTread.DisableKeyword("_EMISSION");
            _bossTurret.DisableKeyword("_EMISSION");
            damageVignette.gameObject.SetActive(false);
        }
        if (damageVignette != null)
        {
            damageVignette.gameObject.SetActive(true);
            yield return new WaitForSeconds(pause);
            damageVignette.gameObject.SetActive(false);
        }
    }
}
