using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using SS3D.Core.Behaviours;
using SS3D.Systems.PlayerControl;
using UnityEngine;

namespace SS3D.Systems.Entities
{
    /// <summary>
    /// Base class for all things that can be controlled by a player.
    /// </summary>
    [Serializable]
    public class Entity : NetworkActor
    {
        public event Action<Mind> OnMindChanged;

        [SerializeField]
        [SyncVar(OnChange = nameof(SyncMind))]
        private Mind _mind = Mind.Empty;

        public Mind Mind
        {
            get => _mind;
            set => _mind = value;
        }

        public string Ckey => _mind.Player.Ckey;

        protected override void OnStart()
        {
            base.OnStart();

            OnSpawn();
        }

        private void OnSpawn()
        {
            OnMindChanged?.Invoke(Mind);
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();

            if (IsOwner)
            {
                LocalPlayer.SetPlayerObject(null);
            }
        }

        private void OnLocalPlayerObjectChanged()
        {
            if (Mind == null || Mind.Player == null)
            {
                return;
            }

            if (!Mind.Player.IsLocalConnection)
            {
                return;
            }

            LocalPlayer.SetPlayerObject(GameObject);
        }

        /// <summary>
        /// Called by FishNet when the value of _mind is synced.
        /// </summary>
        /// <param name="oldMind">Value before sync</param>
        /// <param name="newMind">Value after sync</param>
        /// <param name="asServer">Is the sync is being called as the server (host and server only)</param>
        public void SyncMind(Mind oldMind, Mind newMind, bool asServer)
        {
            if (!asServer && IsHost)
            {
                return;
            }

            OnMindChanged?.Invoke(_mind);
            OnLocalPlayerObjectChanged();
        }

        /// <summary>
        /// Updates the mind of this entity.
        /// </summary>
        /// <param name="mind">The new mind.</param>
        [Server]
        public void SetMind(Mind mind)
        {
            this._mind = mind;
            if(mind == null) return;
            GiveOwnership(mind.Owner);
        }

		public virtual void Kill()
		{
			throw new NotImplementedException();
		}

        public virtual void DeactivateComponents()
        {
            throw new NotImplementedException();
        }
    }
}