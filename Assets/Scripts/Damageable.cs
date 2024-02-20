using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    Animator animator;

    [SerializeField]
    private int _maxHealth;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            // If health drops below 0, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
                InvokeGameOver();
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvicible = false;

    

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if(value == false) 
            {
                damageableDeath.Invoke();
            }
        }
    }

    // The velocity should not be changed while this is true but needs to be respected by
    // other physics components like the player controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvicible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvicible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    // Return whether the damagable took damage or not
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;

            // Notify other subscribed component that the damagable was hit to handle the knockback and such
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        // Unable to be hit
        return false;

    }


    // Returns whether the character was healed or not
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }

    private void InvokeGameOver()
    {
        Invoke("LoadGameOverScene", 5f);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
