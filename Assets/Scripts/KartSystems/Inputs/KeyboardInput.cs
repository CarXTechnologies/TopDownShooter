using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public InputActionReference TurnInput;
        public InputActionReference AccelerateButton;
        public InputActionReference BrakeButton;

        private void Awake()
        {
            TurnInput.action.Enable();
            AccelerateButton.action.Enable();
            BrakeButton.action.Enable();
        }

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = AccelerateButton.action.IsPressed(),
                Brake = BrakeButton.action.IsPressed(),
                TurnInput = TurnInput.action.ReadValue<float>()
            };
        }
    }
}
