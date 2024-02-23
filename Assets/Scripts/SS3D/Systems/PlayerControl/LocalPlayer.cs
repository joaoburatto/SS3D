using FishNet.Connection;
using SS3D.Systems.Entities;
using SS3D.Systems.Entities.Events;
using UnityEngine;

namespace SS3D.Systems.PlayerControl
{
    /// <summary>
    /// Holds data from the Hub to send the server when we connect
    /// </summary>
    public static class LocalPlayer
    {
        /// <summary>
        /// Unique client key, originally used in BYOND's user management, nostalgically used
        /// </summary>
        public static string Ckey { get; private set; }
        public static NetworkConnection Connection { get; set; }
        public static Player Player { get; set; }
        public static GameObject PlayerObject { get; set; }

        public static void SetPlayerObject(GameObject playerObject)
        {
            PlayerObject = playerObject;

            LocalPlayerObjectChanged localPlayerObjectChanged = new(PlayerObject, false);
            localPlayerObjectChanged.Invoke(PlayerObject);
        }

        public static void UpdateCkey(string ckey)
        {
            Ckey = ckey;
        }
    }
}
