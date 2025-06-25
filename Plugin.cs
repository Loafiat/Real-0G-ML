using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using CapuchinTemplate.Patches;
using UnityEngine;
using Caputilla;
using Locomotion;

[assembly: MelonInfo(typeof(Plugin), "RealZeroG", "1.0.0", "Insteal")]

namespace RealZeroG
{
    public class Plugin : MelonMod
    {
        private bool inModded = false;

        public override void OnMelonInitialize()
        {
            // MelonLoader automatically
            CaputillaManager.Instance.OnModdedJoin += OnModdedJoin;
            CaputillaManager.Instance.OnModdedLeave += OnModdedLeave;
        }

        void OnModdedJoin() => inModded = true;
        void OnModdedLeave() => inModded = false;

        override void OnFixedUpdate()
        {
            if (inModded && GameObject.Find("Global/Levels/Zero Core").activeInHierarchy)
                Player.Instance.playerRigidbody.AddForce(-Physics.gravity * Player.Instance.playerRigidbody.mass);
        }
    }
}
