using HarmonyLib;
using MGSC;

namespace QM_PauseOnProduction.Patches.MercenaryTrainingComplete
{
    /// <summary>
    /// Sets the "open training screen" flag when a mercenary is done training.  The flag is processed in the SpaceshipScreen update patch.
    /// </summary>
    [HarmonyPatch(typeof(MercenarySystem), nameof(MercenarySystem.CanTrainMercenary))]
    public class MercenarySystem_CanTrainMercenary_Patch
    {
        public static void Postfix(MagnumProgression magnumSpaceship, Mercenary mercenary, bool __result)
        {
            //Merc can no longer train.  Show the training screen.
            if(mercenary.State == MercenaryState.Training && !__result)
            {
                Plugin.OpenScreenTarget = OpenScreenTarget.Training;
            }
        }
    }
}
