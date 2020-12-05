﻿using System;
using Mirror;
using UnityEngine;

namespace SS3D.Content.Systems.Player
{
    [RequireComponent(typeof(AudioListener))]
    public class PlayerListener : MonoBehaviour
    {
        private AudioListener listener;
        private new CameraFollow camera;

        private void Start()
        {
            if (NetworkClient.active)
            {
                if (NetworkClient.connection.identity.gameObject != transform.parent.gameObject)
                {
                    // Destroy if listener of other player
                    Destroy(gameObject);
                }
            }
            else if (NetworkServer.active)
            {
                // Destroy if server only
                Destroy(gameObject);
            }

            listener = GetComponent<AudioListener>();

            Camera sceneCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
            // Get camera
            if (GameObject.FindGameObjectWithTag("PlayerCamera") != null && Camera.main != null)
            {
                Camera.main.gameObject.SetActive(false);
                camera = sceneCamera.GetComponent<CameraFollow>();
            }
            sceneCamera.enabled = true;
            camera.enabled = true;
            if (camera == null)
            {
                Debug.LogError("Can't find camera follow");
                Destroy(this);
            }
        }

        private void LateUpdate()
        {
           // transform.rotation = Quaternion.Euler(0, 0, camera.angle - 90);
        }
    }
}
