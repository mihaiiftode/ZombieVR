using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    public float AttackDistance = 1.5f;
    public float MobHealth = 100;
    public float AttackDamage = 10;
    public float AttackTime = 3;
    public AudioClip AttackClip;
    public AudioClip WalkClip;
    public AudioClip HitClip;
    public AudioClip DeathClip;

    private Transform playerTransform;
    private Animator animator;
    private Player player;
    private float attackTimer;
    private float soundTimer;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        animator.SetFloat("Speed", MoveSpeed);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        animator.SetFloat("PlayerHealth", player.Health);

        if (!IsDead())
        {
            if (!InRange())
            {
                Walk();
            }
            else
            {
                Attack();
            }
        }
    }

    private bool InRange()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        return distance <= AttackDistance;
    }
    private bool IsDead()
    {
        return MobHealth <= 0;
    }

    private void Walk()
    {
        soundTimer += Time.deltaTime;

        if (soundTimer >= 5)
        {
            soundTimer = 0f;
            PlayAudio(WalkClip);
        }

        transform.LookAt(playerTransform);
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }


    public void Attack()
    {
        if (player.Health <= 0) return;

        attackTimer += Time.deltaTime;

        animator.SetBool("InRange", true);
        if (attackTimer >= AttackTime)
        {
            attackTimer = 0f;
            player.TakeDamage(AttackDamage);

            PlayAudio(AttackClip);
        }
    }

    public void TakeDamage(float damage)
    {
        MobHealth -= damage;
        animator.SetFloat("Health", MobHealth);
        PlayAudio(HitClip);

        if (MobHealth <= 0)
        {
           PlayAudio(DeathClip);
           Destroy(gameObject, 8);
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
