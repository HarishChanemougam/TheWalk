using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] Transform target;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float catchDistance;
    [SerializeField] float waitTime;
    [SerializeField] float groundDistance;
    [SerializeField] GameOver _gameover;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float _lookRadius;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] string targetTag = "Player";

    private int currentWaypointIndex = 0;
    private float currentSpeed;
    private bool isPatrolling = true;
    private bool isChasing = false;
    private bool isGrounded = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentSpeed = patrolSpeed;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        SetDestinationToWaypoint();
    }
    
    private void Update()
    {
        isGrounded = CheckGrounded();

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (isPatrolling)
        {
            Patrol();
        }

        Vector3 direction = target.position - transform.position;
        Ray ray = new Ray(transform.position, direction);

        RaycastHit hit;
        if (Physics.SphereCast(ray, _lookRadius, out hit))
        {
            /*if (hit.collider.CompareTag("Player"))
            {
                _gameover.EndGame();
                Debug.Log("Game Over");
            }*/
        }

        if(isPatrolling == true) 
        {
            PlaySound();
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            _gameover.EndGame();
            Debug.Log("Game Over");
        }
    }*/

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (waitTime > 0f)
            {
                waitTime -= Time.deltaTime;              
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                SetDestinationToWaypoint();
                waitTime = Random.Range(1f, 5f);
                
                /*_animator.SetBool("Running", false);*/
            }            
        }
        /*_animator.SetBool("Running", true);*/
    }

    private void SetDestinationToWaypoint()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) <= catchDistance)
        {
            CatchPlayer();
        }
    }

    private void CatchPlayer()
    {
        /*_gameover.EndGame();*/
        Debug.Log("Player caught!");
    }

    public void StartChasing()
    {
        isPatrolling = false;
        isChasing = true;
        _animator.SetBool("Running", true);
    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance))
        {
            return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            _gameover.EndGame();
            Debug.Log("Game Over");
            Debug.Log("Player detected!");
           
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
   

    public void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}

