using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public InputActionReference TurnInput;
        public InputActionReference AccelerateButton;
        public InputActionReference BrakeButton;

        private void OnEnable()
        {
            TurnInput.action.Enable();
            AccelerateButton.action.Enable();
            BrakeButton.action.Enable();
        }

        private void OnDisable()
        {
            TurnInput.action.Disable();
            AccelerateButton.action.Disable();
            BrakeButton.action.Disable();
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
