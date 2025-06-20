using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using CapuchinTemplate.Patches;
using UnityEngine;
using Caputilla;
using Locomotion;

namespace RealZeroG
{
    [BepInPlugin("insteal.realzerog", "RealZeroG", "1.0.0")]
    public class Init : BasePlugin
    {
        public static Init instance;
        public Harmony harmonyInstance;

        public override void Load()
        {
            harmonyInstance = HarmonyPatcher.Patch("insteal.realzerog");
            instance = this;

            AddComponent<Plugin>();
        }

        public override bool Unload()
        {
            if (harmonyInstance != null)
                HarmonyPatcher.Unpatch(harmonyInstance);

            return true;
        }
    }

    public class Plugin : MonoBehaviour
    {
        private bool inModded = false;

        void Start()
        {
            CaputillaManager.Instance.OnModdedJoin += OnModdedJoin;
            CaputillaManager.Instance.OnModdedLeave += OnModdedLeave;
        }

        void OnModdedJoin() => inModded = true;
        void OnModdedLeave() => inModded = false;

        void FixedUpdate()
        {
            if (inModded && GameObject.Find("Global/Levels/Zero Core").activeInHierarchy)
                Player.Instance.playerRigidbody.AddForce(-Physics.gravity * Player.Instance.playerRigidbody.mass);
        }
    }
}
