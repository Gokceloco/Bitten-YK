using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Level _level;
    private Player _player;

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public ActionState actionState;
    public float speed;

    private bool _isAttackInProgress;

    private NavMeshAgent _agent;

    private Vector3 _vel;

    private Coroutine _attackCoroutine;

    private EnemyAnimator _enemyAnimator;

    public Collider aliveCollider;

    private bool _forceWalkTowardsPlayer;

    public List<Light> lights;

    public void StartEnemy(Level level, Player player)
    {
        _level = level;
        _player = player;
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = startHealth;
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        if (actionState == ActionState.Dead || _player.isDead)
        {
            _agent.isStopped = true;
            return;
        }
        var distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        var angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);

        //Decider Logic
        if (distanceToPlayer < 2)
        {
            actionState = ActionState.Attack;
        }
        else if ((distanceToPlayer < 10 || _forceWalkTowardsPlayer) && !_isAttackInProgress)
        {
            actionState = ActionState.WalkTowardsPlayer;
        }

        //Actions
        if (actionState == ActionState.Idle)
        {
            _enemyAnimator.PlayIdleAnimation();
        }
        if (actionState == ActionState.WalkTowardsPlayer)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
            _enemyAnimator.PlayWalkAnimation();
        }
        if (actionState == ActionState.Attack && !_isAttackInProgress)
        {
            _attackCoroutine = StartCoroutine(AttackCoroutine());    
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    IEnumerator AttackCoroutine()
    {
        _agent.isStopped = true;
        _isAttackInProgress = true;
        _enemyAnimator.PlayAttackAnimaiton();
        yield return new WaitForSeconds(2);
        var distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        var angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
        if (distanceToPlayer < 2 && angleToPlayer < 45)
        {
            _player.GetHit(1);
        }
        _isAttackInProgress = false;
        if (angleToPlayer > 30)
        {
            var lookPos = _player.transform.position;
            lookPos.y = transform.position.y;
            transform.DOLookAt(lookPos, 1f);
        }
    }

    public void GetHit(int damage)
    {
        if (actionState == ActionState.Dead)
        {
            return;
        }
        _forceWalkTowardsPlayer = true;
        _currentHealth -= damage;
        healthBar.SetFillBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        aliveCollider.enabled = false;
        actionState = ActionState.Dead;
        _enemyAnimator.PlayDieAnimation();
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        foreach (var l in lights)
        {
            l.enabled = false;
        }
        Destroy(gameObject, 3);
    }
}

public enum ActionState
{
    Idle,
    WalkTowardsPlayer,
    Attack,
    Dead,
}