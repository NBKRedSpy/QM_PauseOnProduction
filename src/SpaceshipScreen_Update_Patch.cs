using HarmonyLib;
using MGSC;
using System;
using UnityEngine;

namespace QM_PauseOnProduction
{

    /// <summary>
    /// Shows the production window when a production line has completed.
    /// </summary>
    [HarmonyPatch(typeof(SpaceshipScreen), nameof(SpaceshipScreen.Update))]
    public static class SpaceshipScreen_Update_Patch
    {
        public static void Postfix(SpaceshipScreen __instance)
        {
            try
            {
                if (GameLoop_Tick_Space_Patch.ShowProductionWhenOnSpaceScreen)
                {
                    GameLoop_Tick_Space_Patch.ShowProductionWhenOnSpaceScreen = false;
                    __instance.ShowProductionWindow();
                }

            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in SpaceshipScreen_Update_Patch.Postfix", ex);
            }
        }
    }
}