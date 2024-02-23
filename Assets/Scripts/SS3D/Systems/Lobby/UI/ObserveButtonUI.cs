using SS3D.Core;
using SS3D.Core.Behaviours;
using SS3D.Data.Generated;
using SS3D.Systems.Entities;
using SS3D.Systems.PlayerControl;
using UnityEngine;
using UnityEngine.UI;

namespace SS3D.Systems.Lobby.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class ObserveButtonUI : Actor
    {
        private Button _observeButton;

        protected override void OnAwake()
        {
            base.OnAwake();

            _observeButton = GetComponent<Button>();

            _observeButton.onClick.AddListener(HandleObserveButtonPressed);
        }

        private void HandleObserveButtonPressed()
        {
            EntityAssets.FreeCamera.CreateAs(out Entity entityPrefab);

            Subsystems.Get<EntitySystem>().SpawnEntity(LocalPlayer.Player, entityPrefab);
        }
    }
}
