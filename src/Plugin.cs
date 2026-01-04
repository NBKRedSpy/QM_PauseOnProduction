using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_PauseOnProduction
{
    public static class Plugin
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, ModAssemblyName + ".json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);

        public static State State { get; set; }

        /// <summary>
        /// The target
        /// </summary>
        public static OpenScreenTarget OpenScreenTarget { get; set; } = OpenScreenTarget.None;

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            State = context.State;
            new Harmony("NBK_RedSpy_" + ModAssemblyName).PatchAll();
        }


        public static void Log(string message, Exception ex)
        {
            Debug.Log($"[{ModAssemblyName}] {message}");
            Debug.LogException(ex);
        }
    }
}
