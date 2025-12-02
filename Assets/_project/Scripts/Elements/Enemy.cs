using UnityEngine;

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

    public void StartEnemy(Level level, Player player)
    {
        _level = level;
        _player = player;
        _currentHealth = startHealth;
    }

    private void Update()
    {
        var distanceToPlayer = (_player.transform.position - transform.position).magnitude;

        //Decider Logic
        if (distanceToPlayer < 2)
        {
            actionState = ActionState.Attack;
        }
        else if (distanceToPlayer < 10)
        {
            actionState = ActionState.WalkTowardsPlayer;
        }



        //Actions
        if (actionState == ActionState.Idle)
        {

        }
        if (actionState == ActionState.WalkTowardsPlayer)
        {
            var direction = (_player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        if (actionState == ActionState.Attack && !_isAttackInProgress)
        {
            print("Start Attack");
            _isAttackInProgress = true;
            Invoke(nameof(FinalizeAttack), 2); 
        }
    }

    void FinalizeAttack()
    {
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