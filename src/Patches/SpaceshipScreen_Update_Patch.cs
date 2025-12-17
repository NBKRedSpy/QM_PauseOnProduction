using HarmonyLib;
using MGSC;
using System;
using UnityEngine;

namespace QM_PauseOnProduction.Patches
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
                switch (Plugin.OpenScreenTarget)
                {   
                    case OpenScreenTarget.None:
                        break;
                    case OpenScreenTarget.Recycler:
                        //Change the state from the Cargo screen to open the recyler tab.

                        //  This is required as the patch to change the tab is in an area that is used by several screens,
                        //  and our patch only wants to affect the SpaceshipScreen.
                        Plugin.OpenScreenTarget = OpenScreenTarget.RecyclerTab;  

                        //Opens the ship's cargo.  
                        __instance.ArsenalButtonOnClick(null,1);
                        break;
                    case OpenScreenTarget.Production:
                        Plugin.OpenScreenTarget = OpenScreenTarget.None;
                        __instance.ShowProductionWindow();
                        break;
                    default:
                        throw new ArgumentException($"Unknown OpenScreenTarget value. '{Plugin.OpenScreenTarget}'");
                }

            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in SpaceshipScreen_Update_Patch.Postfix", ex);
            }
        }
    }
}