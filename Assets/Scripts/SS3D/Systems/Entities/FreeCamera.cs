using Coimbra.Services.Events;
using Coimbra.Services.PlayerLoopEvents;
using FishNet.Connection;
using SS3D.Systems.PlayerControl;
using UnityEngine;

namespace SS3D.Systems.Entities
{
    public sealed class FreeCamera : Entity
    {
        public float MoveSpeed = 5f;
        public float LookSpeed = 2f;

        private float _rotationX;

        private float MouseX => Input.GetAxis("Mouse X") * LookSpeed;
        private float MouseY => Input.GetAxis("Mouse Y") * LookSpeed;

        public EventHandleTrackerComponent EventHandleTrackerComponent;

        public override void OnOwnershipClient(NetworkConnection prevOwner)
        {
            base.OnOwnershipClient(prevOwner);

            if (Owner == LocalPlayer.Connection)
            {
                EventHandleTrackerComponent.Add(UpdateEvent.AddListener(HandleUpdate));
            }
            else
            {
                EventHandleTrackerComponent.Clear(true);
            }
        }

        private void HandleUpdate(ref EventContext context, in UpdateEvent e)
        {
            ProcessMovement();
            ProcessRotation();
        }

        private void ProcessRotation()
        {
            _rotationX -= MouseY;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            LocalRotation = Quaternion.Euler(_rotationX, 0f, 0f);
            Parent.Rotate(Vector3.up * MouseX);
        }

        private void ProcessMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

            moveDirection = Transform.TransformDirection(moveDirection);
            Position += moveDirection * MoveSpeed * Time.deltaTime;
        }
    }
}