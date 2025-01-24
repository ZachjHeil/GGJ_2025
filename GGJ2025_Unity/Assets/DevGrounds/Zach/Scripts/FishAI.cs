using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishAI : MonoBehaviour
{
    // Reference to the player's Transform to follow and target
    public Transform player;

    // Detection range within which the fish starts moving towards the player
    public float detectionRange = 30f;

    // Attack range within which the fish begins charging an attack
    public float attackRange = 10f;

    // Speed of the fish during a dash attack
    public float dashSpeed = 25f;

    // Time required to fully charge an attack
    public float summonAttackChargeTime = 5f;

    // Grace period after an attack during which the fish cannot attack again
    public float gracePeriod = 15f;

    // Radius for random movement when the player is out of range
    public float randomMovementRadius = 20f;

    // Prefab for summoned fish attacks
    public GameObject summonedFishPrefab;

    // Number of fish to summon during a summon attack
    public int summonCount = 5;

    private NavMeshAgent agent; // NavMeshAgent component for movement
    private bool isAttacking = false; // Flag to check if the fish is currently attacking
    private bool isGracePeriod = false; // Flag to check if the fish is in its grace period
    private Vector3 startPosition; // Starting position of the fish

    private void Start()
    {
        // Initialize the NavMeshAgent and set the starting position
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        StartCoroutine(GracePeriodCoroutine()); // Start the grace period coroutine
    }

    private void Update()
    {
        // If the fish is in grace period or currently attacking, do nothing
        if (isGracePeriod || isAttacking) return;

        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Move towards the player if within detection range but outside attack range
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        // Start charging an attack if within attack range
        else if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(ChargeAttack());
        }
        // Perform random movement if the player is out of range
        else
        {
            RandomMovement();
        }
    }

    // Moves the fish towards the player's position
    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.position);
    }

    // Coroutine to handle the charge-up phase of an attack
    private IEnumerator ChargeAttack()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop the NavMeshAgent during the charge-up

        float chargeTime = 0f;
        while (chargeTime < summonAttackChargeTime)
        {
            chargeTime += Time.deltaTime;

            // Rotate the fish during the charge-up
            transform.Rotate(Vector3.up, 360 * Time.deltaTime * summonAttackChargeTime);

            // Perform an attack if the charge time reaches two-thirds of the full charge time
            if (chargeTime >= summonAttackChargeTime * 2f / 3f)
            {
                // Randomly choose between dash or summon attack
                if (Random.value > 0.5f)
                {
                    DashAttack();
                }
                else
                {
                    SummonAttack();
                }
                yield break;
            }
            yield return null;
        }

        isAttacking = false;
        agent.isStopped = false;
    }

    // Initiates the dash attack behavior
    private void DashAttack()
    {
        StartCoroutine(DashCoroutine());
    }

    // Coroutine to handle the dash attack movement
    private IEnumerator DashCoroutine()
    {
        Vector3 dashDirection = (player.position - transform.position).normalized;
        float dashDuration = 1f; // Duration of the dash
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        EndAttack();
    }

    // Spawns summoned fish attacks around the main fish
    private void SummonAttack()
    {
        for (int i = 0; i < summonCount; i++)
        {
            Vector3 summonPosition = transform.position + Random.insideUnitSphere * 5f;
            summonPosition.y = transform.position.y;
            Instantiate(summonedFishPrefab, summonPosition, Quaternion.identity);
        }
        EndAttack();
    }

    // Ends the current attack and starts the grace period
    private void EndAttack()
    {
        isAttacking = false;
        StartCoroutine(GracePeriodCoroutine()); // Start the grace period
        MoveTowardsPlayer();
        
    }

    // Coroutine to enforce a grace period after an attack
    private IEnumerator GracePeriodCoroutine()
    {
        isGracePeriod = true;
        yield return new WaitForSeconds(gracePeriod);
        isGracePeriod = false;
    }

    // Handles random movement of the fish when the player is out of range
    private void RandomMovement()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
            randomDirection += startPosition;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, 1))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    // Moves the fish to a random far position after an attack
    private void MoveToRandomFarPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
        randomDirection += startPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}
