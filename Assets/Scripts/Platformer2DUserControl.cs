using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField] private int keyboardConfiguration = 1;

        private string ButtonToJump, ButtonToFire;


        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Fire;
        private bool can_Controll = true;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            switch(keyboardConfiguration)
            {
                case 1:
                    keyboard1();break;
                case 2:
                    keyboard2();break;
                case 3:
                    keyboard3(); break;
                default:
                    keyboard1();break;
            }
        }

        private float horizontalAxis()
        {
            switch (keyboardConfiguration)
            {
                case 1:
                    return keyboard1Horizontal();
                case 2:
                    return keyboard2Horizontal();
                case 3:
                    return keyboard3Horizontal();
                default:
                    return keyboard1Horizontal();
            }
        }

        private void keyboard1()
        {
            ButtonToJump = "Jump";
            ButtonToFire = "Fire1";
        }

        private void keyboard2()
        {
            ButtonToJump = "JumpPlayer1";
            ButtonToFire = "FirePlayer1";
        }

        private void keyboard3()
        {
            ButtonToJump = "JumpPlayer2";
            ButtonToFire = "FirePlayer2";
        }

        private float keyboard1Horizontal()
        {
            return CrossPlatformInputManager.GetAxis("Horizontal");
        }

        private float keyboard2Horizontal()
        {
            return CrossPlatformInputManager.GetAxis("HorizontalPlayer1");
        }

        private float keyboard3Horizontal()
        {
            return CrossPlatformInputManager.GetAxis("HorizontalPlayer2");
        }

        public void blockControlls()
        {
            can_Controll = false;
        }

        public void unblockControlls()
        {
            can_Controll = true;
        }

        private void Update()
        {
            if (can_Controll)
            {
                if (!m_Jump)
                    m_Jump = CrossPlatformInputManager.GetButtonDown(ButtonToJump);
                if (!m_Fire)
                    m_Fire = CrossPlatformInputManager.GetButtonDown(ButtonToFire);
            }
        }


        private void FixedUpdate()
        {
            float h = 0;
            if (can_Controll)
                h = horizontalAxis();
            m_Character.Move(h, m_Jump, m_Fire);
            m_Jump = false;
            m_Fire = false;
        }
    }
}
