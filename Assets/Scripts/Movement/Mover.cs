using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent navMeshAgent;
        Health health;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            
            navMeshAgent.enabled = !health.IsDead();
            
            
            //else if (!health.IsDead())
            //{
            //    navMeshAgent.enabled = true;
            //}
            UpdateAnimator();
            //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination,speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            //Get the global velocity from Nav Mesh Agent
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            //Convert this into a local value relative to the character
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            //Set the Animator's blend value to be equal to our desired forward speed (on the Z axis )
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        //Save 
      
        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }
        //Call after Awake but before Start
        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            //khi upate transform position thi phai disable NavMeshAgent;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
