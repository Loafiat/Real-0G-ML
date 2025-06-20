using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using CapuchinTemplate.Patches;
using UnityEngine;
using Caputilla;
using Locomotion;

namespace CapuchinTemplate
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Init : BasePlugin
    {
        public static Init instance;
        public Harmony harmonyInstance;

        public override void Load()
        {
            harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
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
        static bool inModded = false;

        void Start()
        {
            CaputillaManager.Instance.OnModdedJoin += OnModdedJoin;
            CaputillaManager.Instance.OnModdedLeave += OnModdedLeave;
        }

        void OnModdedJoin()
        {
            inModded = true;
        }

        void OnModdedLeave()
        {
            inModded = false;
        }

        void FixedUpdate()
        {
            if (inModded)
            {
                if (GameObject.Find("Global/Levels/Zero Core").activeInHierarchy)
                {
                    if (inModded)
                    {
                        Player.Instance.playerRigidbody.useGravity = false;
                    }
                    else
                    {
                        Player.Instance.playerRigidbody.useGravity = true;
                    }
                }
                else
                {
                    Player.Instance.playerRigidbody.useGravity = true;
                }
            }
            else 
            {
                Player.Instance.playerRigidbody.useGravity = true;
            }
        }
    }
}
