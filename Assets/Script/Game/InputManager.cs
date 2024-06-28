using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class InputManager 
    {
        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction swapAction;
        private TaskCompletionSource<InputData> taskCompletionSource = new TaskCompletionSource<InputData>();
        private Vector2Int lastInput;

        public Vector2Int LastInput => lastInput;

        public InputManager(PlayerInput playerInput)
        {
            this.playerInput = playerInput;
            lastInput = new Vector2Int();
            moveAction = playerInput.actions["Move"];
            swapAction = playerInput.actions["Swap"];
            moveAction.performed += callbackContext => OnMovePerformed(callbackContext.ReadValue<Vector2>());
            swapAction.performed += callbackContext => OnSwapPerformed(callbackContext.ReadValue<float>());
        }

        public async Task<InputData> GetInput()
        {
            taskCompletionSource = new TaskCompletionSource<InputData>();
            var data = await taskCompletionSource.Task;
            taskCompletionSource = null;
            return data;
        }

        public void ResetLastInput(Vector2Int updateInput)
        {
            lastInput = updateInput;
        }

        private Vector2Int ConvertDirection(Vector2 direction)
        {
            int x = 0;
            int y = 0;

            if (direction.x > 0)
                x = 1;
            else if (direction.x < 0)
                x = -1;
            if (direction.y > 0)
                y = 1;
            else if (direction.y < 0)
                y = -1;
            
            return new Vector2Int(x,y);
        }
        
        private void OnMovePerformed(Vector2 movement)
        {
            if (taskCompletionSource != null)
            {
                InputData direction = new InputData(EActionType.None, new Vector2Int());
                if (movement == -lastInput)
                {
                    taskCompletionSource.SetResult(direction);
                    return;
                }
                lastInput = ConvertDirection(movement);
                direction = new InputData(EActionType.Move, lastInput);
                taskCompletionSource.SetResult(direction);
            }
        }
        
        private void OnSwapPerformed(float value)
        {
            if (taskCompletionSource != null)
            {
                int result = 0;
                if (value > 0)
                    result = 1;
                else if (value < 0)
                    result = -1;
                InputData swap = new InputData(EActionType.Swap, new Vector2Int(result,0));
                taskCompletionSource.SetResult(swap);
            }
        }
    }
}


