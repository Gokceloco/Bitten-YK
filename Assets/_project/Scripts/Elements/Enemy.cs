using DG.Tweening;
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

    public void StartEnemy(Level level, Player player)
    {
        _level = level;
        _player = player;
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = startHealth;
    }

    private void Update()
    {
        var distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        var angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);

        //Decider Logic
        if (distanceToPlayer < 2)
        {
            actionState = ActionState.Attack;
        }
        else if (distanceToPlayer < 10 && !_isAttackInProgress)
        {
            actionState = ActionState.WalkTowardsPlayer;
        }



        //Actions
        if (actionState == ActionState.Idle)
        {

        }
        if (actionState == ActionState.WalkTowardsPlayer)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
        }
        if (actionState == ActionState.Attack && !_isAttackInProgress)
        {
            print("Start Attack");
            _agent.isStopped = true;
            _isAttackInProgress = true;
            transform.DOScaleY(1.5f, 1.7f);
            transform.DOScaleY(1, .3f).SetDelay(1.7f);
            Invoke(nameof(FinalizeAttack), 2); 
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    void FinalizeAttack()
    {
        var distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        var angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
        if (distanceToPlayer < 2 && angleToPlayer < 45)
        {
            _player.GetHit(1);
        }        
        _isAttackInProgress = false;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetFillBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum ActionState
{
    Idle,
    WalkTowardsPlayer,
    Attack,
    Dead,
}