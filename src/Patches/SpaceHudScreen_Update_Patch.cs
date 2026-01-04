using HarmonyLib;
using MGSC;
using System;
using UnityEngine;

namespace QM_PauseOnProduction.Patches
{
    /// <summary>
    /// Relays the request to open the production screen when the space HUD is updated
    /// if production has completed.
    /// </summary>
    [HarmonyPatch(typeof(SpaceHudScreen), nameof(SpaceHudScreen.Update))]
    public static class SpaceHudScreen_Update_Patch
    {
        public static void Postfix(SpaceHudScreen __instance)
        {
            try
            {
                //Relaying with the flag since trying to use the UI class directly
                //  was causing issues with the after raid screen.
                if (Plugin.OpenScreenTarget != OpenScreenTarget.None)
                {
                    if (!__instance._shipButton.isActiveAndEnabled) return;

                    __instance.ShipButtonOnClick(null, 1); //Open the ship screen
                }
            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in SpaceHudScreen_Update_Patch.Postfix", ex);
            }
        }
    }


}