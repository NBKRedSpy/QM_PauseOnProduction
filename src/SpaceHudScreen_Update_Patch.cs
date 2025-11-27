using HarmonyLib;
using MGSC;
using System;

namespace QM_PauseOnProduction
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
                if (GameLoop_Tick_Space_Patch.ShowProductionWhenOnSpaceScreen)
                {
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