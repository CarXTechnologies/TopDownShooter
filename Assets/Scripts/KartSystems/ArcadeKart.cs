using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;

namespace KartGame.KartSystems
{
    public class ArcadeKart : MonoBehaviour
    {

        [System.Serializable]
        public struct Stats
        {
            [Tooltip("How quickly the kart reaches top speed.")]
            public float Acceleration;
            
            [Tooltip("How quickly the kart slows down when the brake is applied.")]
            public float Braking;
            

            // allow for stat adding for powerups.
            public static Stats operator +(Stats a, Stats b)
            {
                return new Stats
                {
                    Acceleration        = a.Acceleration + b.Acceleration,
                    Braking             = a.Braking + b.Braking,
                };
            }
        }

        public Rigidbody Rigidbody { get; private set; }
        public InputData Input     { get; private set; }


        public ArcadeKart.Stats baseStats = new ArcadeKart.Stats
        {
            Acceleration        = 5f,
            Braking             = 10f,
        };

   
        [Header("Vehicle Physics")]
        [Tooltip("The transform that determines the position of the kart's mass.")]
        public Transform CenterOfMass;
        
        [Header("Physical Wheels")]
        [Tooltip("The physical representations of the Kart's wheels.")]
        public WheelCollider FrontLeftWheel;
        public WheelCollider FrontRightWheel;
        public WheelCollider RearLeftWheel;
        public WheelCollider RearRightWheel;
        
        // the input sources that can control the kart
        IInput[] m_Inputs;
        
        // can the kart move?
        bool m_CanMove = true;

        Quaternion m_LastValidRotation;
        bool m_HasCollision;
        bool m_InAir = false;

        public void SetCanMove(bool move) => m_CanMove = move;
        

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            m_Inputs = GetComponents<IInput>();
        }

        void FixedUpdate()
        {
            GatherInputs();
            
            // apply our physics properties
            Rigidbody.centerOfMass = transform.InverseTransformPoint(CenterOfMass.position);


            // apply vehicle physics
            if (m_CanMove)
            {
                // MoveVehicle(Input.Accelerate, Input.Brake, Input.TurnInput);
                DebugControl();
                Move();
            }
        }

        void GatherInputs()
        {
            // reset input
            Input = new InputData();

            // gather nonzero input from our sources
            for (int i = 0; i < m_Inputs.Length; i++)
            {
                Input = m_Inputs[i].GenerateInput();
            }
        }

        public void Reset()
        {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.x = euler.z = 0f;
            transform.rotation = Quaternion.Euler(euler);
        }

        public float LocalSpeed()
        {
            if (m_CanMove)
            {
                float dot = Vector3.Dot(transform.forward, Rigidbody.velocity);
                if (Mathf.Abs(dot) > 0.1f)
                {
                    float speed = Rigidbody.velocity.magnitude;
                    return dot < 0 ? -(speed) : (speed);
                }
                return 0f;
            }
            else
            {
                return Input.Accelerate ? 1.0f : 0.0f;
            }
        }
        private float m_currentAcceleration = 0.0f;
        private float m_currentBreakForce = 0.0f;
        private void Move()
        {
            // apply acceleration => All wheelss
            RearLeftWheel.motorTorque = m_currentAcceleration;
            RearRightWheel.motorTorque = m_currentAcceleration;
            // FrontLeftWheel.motorTorque = _currentAcceleration;
            // FrontRightWheel.motorTorque = _currentAcceleration;

            // break force => all wheels
            RearLeftWheel.brakeTorque = m_currentBreakForce;
            RearRightWheel.brakeTorque = m_currentBreakForce;
            FrontLeftWheel.brakeTorque = m_currentBreakForce;
            FrontRightWheel.brakeTorque = m_currentBreakForce;
        }

        private int m_gear = 0;
        private float m_lastBrakeValue = 0f;
        private void DebugControl()
        {
            var accel = Input.Accelerate ? 1f : 0f;
            var brake = Input.Brake ? 1f : 0f;
            CheckAutoReverse(ref accel, ref brake, ref m_gear);
            
            m_currentAcceleration = m_gear == -1 ?  -accel : accel;
            m_currentAcceleration *= baseStats.Acceleration;
            
            m_currentBreakForce = m_gear == -1 ?  -brake : brake;
            m_currentBreakForce *= baseStats.Braking;
        }

        private void CheckAutoReverse(ref float acceleration, ref float brake, ref int gear)
        {
            var velocity = Rigidbody.velocity;
            var forward = transform.forward;
			
            if (Vector3.Dot(forward, velocity) < 0f || velocity.sqrMagnitude < (0.1f * 0.1f))
            {
                if (brake > 0f && acceleration < 0.7f && m_lastBrakeValue < 1f)
                {
                    gear = -1;
                }
			
                if (acceleration > 0.1f && gear < 0)
                {
                    gear = 1;
                }
            }
			
            m_lastBrakeValue = brake;
			
            if (gear != -1)
            {
                return;
            }
            (acceleration, brake) = (brake, acceleration);
        }
    }
}
