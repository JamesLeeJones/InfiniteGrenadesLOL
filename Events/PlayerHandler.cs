// -----------------------------------------------------------------------
// <copyright file="PlayerHandler.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Example.Events
{
    using CameraShaking;

    using CustomPlayerEffects;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs;

    using InventorySystem.Items.Usables;

    using MEC;

    using UnityEngine;

    using static Example;

    /// <summary>
    /// Handles player events.
    /// </summary>
    internal sealed class PlayerHandler
    {
        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDied(DiedEventArgs)"/>
        public void OnDied(DiedEventArgs ev)
        {
            Log.Info($"{ev.Target?.Nickname} ({ev.Target?.Role}) died from {ev.HitInformations.Tool}! {ev.Killer?.Nickname} ({ev.Killer?.Role}) killed him!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingRole(ChangingRoleEventArgs)"/>
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} ({ev.Player?.Role}) is changing his role! The new role will be {ev?.NewRole}!");
            if (ev.NewRole == RoleType.Tutorial)
            {
                ev.Items.Clear();
                ev.Items.Add(ItemType.Flashlight);
                ev.Items.Add(ItemType.Medkit);
                Timing.CallDelayed(0.5f, () => ev.Player.AddItem(ItemType.Radio));
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingItem(ChangingItemEventArgs)"/>
        public void OnChangingItem(ChangingItemEventArgs ev)
        {
            Timing.CallDelayed(2f, () =>
            {
                if (ev.Player.CurrentItem is Firearm firearm)
                {
                    Log.Info($"{ev.Player.Nickname} has a firearm!");
                    firearm.Recoil = new RecoilSettings(0, 0, 0, 0, 0, 0);
                }
            });
            Log.Info($"{ev.Player.Nickname} is changing his {(ev.Player.CurrentItem == null ? "NONE" : ev.Player.CurrentItem.Type.ToString())} item to {(ev.NewItem == null ? "NONE" : ev.NewItem.Type.ToString())}!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp106.OnTeleporting(TeleportingEventArgs)"/>
        public void OnTeleporting(TeleportingEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is teleporting to {ev.PortalPosition} as SCP-106!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp106.OnContaining(ContainingEventArgs)"/>
        public void OnContaining(ContainingEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is being contained as SCP-106!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp106.OnCreatingPortal(CreatingPortalEventArgs)"/>
        public void OnCreatingPortal(CreatingPortalEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is creating a portal as SCP-106, in the position: {ev.Position}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp914.OnActivating(ActivatingEventArgs)"/>
        public void OnActivating(ActivatingEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is activating SCP-914!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs)"/>
        public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is failing to escape from the pocket dimension!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnEscapingPocketDimension(EscapingPocketDimensionEventArgs)"/>
        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is escaping from the pocket dimension and will be teleported at {ev.TeleportPosition}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp914.OnChangingKnobSetting(ChangingKnobSettingEventArgs)"/>
        public void OnChangingKnobSetting(ChangingKnobSettingEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is changing the knob setting of SCP-914 to {ev.KnobSetting}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnJoined(JoinedEventArgs)"/>
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (!Instance.Config.JoinedBroadcast.Show)
                return;

            Log.Info($"{ev.Player.Nickname} has authenticated! Their Player ID is {ev.Player.Id} and UserId is {ev.Player.UserId}");
            ev.Player.Broadcast(Instance.Config.JoinedBroadcast.Duration, Instance.Config.JoinedBroadcast.Content, Instance.Config.JoinedBroadcast.Type, false);
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnUnlockingGenerator(UnlockingGeneratorEventArgs)"/>
        public void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            Log.Info($"{ev.Player?.Nickname} is trying to unlock a generator in {ev.Player?.CurrentRoom} room");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDestroying(DestroyingEventArgs)"/>
        public void OnDestroying(DestroyingEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) is leaving the server!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDying(DyingEventArgs)"/>
        public void OnDying(DyingEventArgs ev)
        {
            Log.Info($"{ev.Target.Nickname} ({ev.Target.Role}) is getting killed by {ev.Killer.Nickname} ({ev.Killer.Role})!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnPreAuthenticating(PreAuthenticatingEventArgs)"/>
        public void OnPreAuthenticating(PreAuthenticatingEventArgs ev)
        {
            Log.Info($"{ev.UserId} is pre-authenticating from {ev.Country} ({ev.Request.RemoteEndPoint}) with flags {ev.Flags}!");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnPickingUpItem(PickingUpItemEventArgs)"/>
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} has picked up a {ev.Pickup.Type}! Weight: {ev.Pickup.Weight} Serial: {ev.Pickup.Serial}.");
            Log.Warn($"{ev.Pickup.Base.Info.Serial} -- {ev.Pickup.Base.NetworkInfo.Serial}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnUsingItem(UsingItemEventArgs)"/>
        public void OnUsingItem(UsingItemEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} is trying to use {ev.Item.Type}.");
            if (ev.Item.Type == ItemType.Adrenaline)
            {
                Log.Info($"{ev.Player.Nickname} was stopped from using their {ev.Item.Type}!");
                ev.IsAllowed = false;
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnShooting(ShootingEventArgs)"/>
        public void OnShooting(ShootingEventArgs ev)
        {
            Log.Info($"{ev.Shooter.Nickname} is shooting a {ev.Shooter.CurrentItem.Type}! Target Pos: {ev.ShotPosition} Target object ID: {ev.TargetNetId} Allowed: {ev.IsAllowed}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnReloadingWeapon(ReloadingWeaponEventArgs)"/>
        public void OnReloading(ReloadingWeaponEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} is reloading their {ev.Firearm.Type}. They have {ev.Firearm.Ammo} ammo. Using ammo type {ev.Firearm.AmmoType}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnReceivingEffect(ReceivingEffectEventArgs)"/>
        public void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} is receiving effect {ev.Effect}. Duration: {ev.Duration} New Intensity: {ev.State} Old Intensity: {ev.CurrentState}");
            if (ev.Effect is Invigorated)
            {
                Log.Info($"{ev.Player.Nickname} is being rejected the {nameof(Invigorated)} effect!");
                ev.IsAllowed = false;
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp914.OnUpgradingPlayer(UpgradingPlayerEventArgs)"/>
        public void OnUpgradingPlayer(UpgradingPlayerEventArgs ev)
        {
            Log.Info($"SCP-914 is processing {ev.Player.Nickname} on {ev.KnobSetting}. Upgrade Items: {ev.UpgradeItems} Held Items only: {ev.HeldOnly}");
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDroppingItem(DroppingItemEventArgs)"/>
        public void OnDroppingItem(DroppingItemEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} is dropping {ev.Item.Type}!");
            if (ev.Item.Type == ItemType.Adrenaline)
                ev.IsAllowed = false;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnSpawning(SpawningEventArgs)"/>
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.RoleType == RoleType.Scientist)
            {
                ev.Position = new Vector3(53f, 1020f, -44f);
                Timing.CallDelayed(1f, () => ev.Player.CurrentItem = new Firearm(ItemType.GunCrossvec));
                Timing.CallDelayed(1f, () => ev.Player.AddItem(ItemType.GunLogicer));
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnEscaping(EscapingEventArgs)"/>
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scientist)
                ev.NewRole = RoleType.Tutorial;
            Log.Info($"{ev.Player.Nickname} is trying to escape! Their new role will be {ev.NewRole}");
        }
    }
}
