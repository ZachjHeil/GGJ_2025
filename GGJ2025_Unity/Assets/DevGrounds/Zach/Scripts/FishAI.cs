using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishAI : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float detectionRange = 30f; // Detection range to start moving towards the player
    public float attackRange = 10f; // Attack range to charge an attack
    public float dashSpeed = 25f; // Speed during a dash attack
    public float summonAttackChargeTime = 5f; // Time to charge an attack
    public float gracePeriod = 15f; // Grace period after an attack
    public float randomMovementRadius = 20f; // Radius for random movement
    public GameObject summonedFishPrefab; // Prefab for summoned fish
    public int summonCount = 5; // Number of summoned fish during an attack

    [SerializeField]
    private Transform[] spawnPOS;

    private NavMeshAgent agent; // NavMeshAgent component for movement
    private bool isAttacking = false; // Is the fish currently attacking?
    private bool isGracePeriod = false; // Is the fish in grace period?
    private Vector3 startPosition; // Initial position of the fish

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        StartCoroutine(GracePeriodCoroutine()); // Start grace period
    }

    private void Update()
    {
        if (isGracePeriod || isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(ChargeAttack());
        }
        else
        {
            RandomMovement();
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.isStopped = false; // Ensure agent is active
        agent.SetDestination(player.position);
    }

    private IEnumerator ChargeAttack()
    {
        isAttacking = true;
        agent.isStopped = true;

        float chargeTime = 0f;
        while (chargeTime < summonAttackChargeTime)
        {
            chargeTime += Time.deltaTime;

            // Rotate during charge-up for visual feedback
            transform.Rotate(Vector3.up, 360 * Time.deltaTime * summonAttackChargeTime);
            

            // Trigger attack at two-thirds of the charge time
            if (chargeTime >= summonAttackChargeTime * 2f / 3f)
            {
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

    private void DashAttack()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        Vector3 dashDirection = (player.position - transform.position).normalized;
        float dashTimer = 0f;
        float dashDuration = 1f;

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dashDirection, out hit, dashSpeed * Time.deltaTime))
            {
                // Stop dash if collision is detected
                break;
            }

            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        EndAttack();
    }

    private void SummonAttack()
    {
        for (int i = 0; i < summonCount; i++)
        {
            
// Loop through spawn positions (or wherever you're getting the positions from)
Vector3 summonPosition = spawnPOS[i].position;

// Instantiate the fish at the specified position
GameObject spawnedFish = Instantiate(summonedFishPrefab, summonPosition, Quaternion.identity);  

// Calculate the direction from the spawned fish to the player
Vector3 directionToPlayer = player.position - spawnedFish.transform.position;

// Calculate the rotation that looks towards the player
Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

// Apply the rotation to the newly instantiated fish
spawnedFish.transform.rotation = lookRotation;

          
           
        }
        EndAttack();
    }

    private void EndAttack()
    {
        isAttacking = false;
        agent.isStopped = false; // Resume NavMesh movement
        StartCoroutine(GracePeriodCoroutine());
        MoveToRandomFarPosition();
    }

    private IEnumerator GracePeriodCoroutine()
    {
        isGracePeriod = true;
        yield return new WaitForSeconds(gracePeriod);
        isGracePeriod = false;
    }

    private void RandomMovement()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
            randomDirection += startPosition;
            randomDirection.y = startPosition.y; // Ensure movement on same Y level

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    private void MoveToRandomFarPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMovementRadius;
        randomDirection += startPosition;
        randomDirection.y = startPosition.y; // Ensure movement on same Y level

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomMovementRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
