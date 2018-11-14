using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private GameObject shot;
        [SerializeField] private Transform shotSpawn;
        [SerializeField] private float fireRate = 0.25f;
        [SerializeField] private LayerMask m_WhatIsGround;

        private bool m_Grounded;            // Whether or not the player is grounded.
        private Rigidbody2D m_Rigidbody2D;
        private Vector2 lastPosition;
        private Animator animator;
        private float nextFire;
        private Transform healthBar;
        private bool fire = false;
        private GameObject bottomTrigger;

        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            healthBar = FindObjectInChilds(gameObject, "HealthBar").GetComponent<Transform>();
            bottomTrigger = FindObjectInChilds(gameObject, "Bottom Trigger");
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomTrigger.GetComponent<Transform>().position, bottomTrigger.GetComponent<CircleCollider2D>().radius- bottomTrigger.GetComponent<CircleCollider2D>().offset.y, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                //Debug.Log(colliders[i].gameObject);
                if (colliders[i].gameObject.tag == "Platform")
                    m_Grounded = true;
            }
        }


        public void Move(float move, bool jump, bool fire)
        {
            animator.SetBool("Run", move != 0 ? true : false);
            if (m_Grounded || m_AirControl)
            {
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                Flip(move);
            }
            if (m_Grounded && jump)
                Jump();
            bool anim = false;
            
            if (fire)
                anim = Fire();
            
            //if (anim ==false)
                //animator.SetBool("Shoot", false);
        }

        IEnumerator StartAnimationForShoot()
        {
            animator.SetBool("Shoot", true);
            yield return new WaitForSeconds(0.125f);
            animator.SetBool("Shoot", false);
        }



        private void Jump()
        {
            //animator.speed = 0;
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        private bool Fire()
        {
            if (Time.time > nextFire)
            {
                StartCoroutine(StartAnimationForShoot());
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                return true;
            }
            return false;
        }


        private void Flip(float move)
        {
            Quaternion target = GetComponent<Transform>().rotation;
            if (move < 0)
            {
                target = Quaternion.Euler(0, 180, 0);
                healthBar.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (move > 0)
            {
                target = Quaternion.Euler(0, 0, 0);
                healthBar.localRotation = Quaternion.Euler(0, 0, 0);
            }
            GetComponentInChildren<Transform>().rotation = target;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            //if (FindObjectInChilds(gameObject, "Bottom Trigger").GetComponent<CircleCollider2D>().IsTouching(collider) && collider.tag == "Platform")
            //    m_Grounded = true;
        }

        public static GameObject FindObjectInChilds(GameObject gameObject, string gameObjectName)
        {
            Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform item in children)
            {
                if (item.name == gameObjectName)
                {
                    return item.gameObject;
                }
            }

            return null;
        }
    }
}
