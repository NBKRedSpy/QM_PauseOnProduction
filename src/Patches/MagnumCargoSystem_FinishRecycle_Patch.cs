using HarmonyLib;
using MGSC;

namespace QM_PauseOnProduction.Patches
{
    /// <summary>
    /// Detects if the recycler is finished. If so, sets the flag to open the recycler screen.
    /// The flag is processed in the SpaceHudScreen update patch.
    /// </summary>
    [HarmonyPatch(typeof(MagnumCargoSystem), nameof(MagnumCargoSystem.FinishRecycle))]
    public class PatchTargetExample_TargetMethod_Patch
    {
        public static void Postfix()
        {
            if(Plugin.OpenScreenTarget == OpenScreenTarget.None)
            {
                //Prefer production over the recycler to preven the UI from trying to go back and forth
                //  between the two screens.
                Plugin.OpenScreenTarget = OpenScreenTarget.Recycler;
            }
        }
    }
}
