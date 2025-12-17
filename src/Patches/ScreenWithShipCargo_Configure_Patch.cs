using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QM_PauseOnProduction
{
    /// <summary>
    /// Opens the recycling tab if the open screen target is set.
    /// </summary>
    [HarmonyPatch(typeof(ScreenWithShipCargo), nameof(ScreenWithShipCargo.Configure))]
    public static class ScreenWithShipCargo_Configure_Patch
    {
        public static void Postfix(ScreenWithShipCargo __instance)
        {
            try
            {
                if (Plugin.OpenScreenTarget != OpenScreenTarget.RecyclerTab) return;

                Plugin.OpenScreenTarget = OpenScreenTarget.None;

                //Get the recycling tab index.
                if (__instance._magnumCargo.RecyclingStorage == null) return;

                ItemTabsView tabsView = __instance._tabsView;

                //Get the tab and select it.
                //  The game doesn't have a way to set the tab by storage, but can by tab index.
                //  Getting the index to be able to use the game's select tab by index.
                //
                //  While selecting the tab is trivial, I would rather use the game's logic instead.
                KeyValuePair<int, object> recyclerEntry = tabsView._idsToContent
                    .Where(x => x.Value == __instance._magnumCargo.RecyclingStorage)
                    .FirstOrDefault();

                //.NET framework doesn't support a default value.  While the tab indexes
                //  are actually one based, I don't want to make assumptions for future changes.
                if (recyclerEntry.Equals(default(KeyValuePair<int, object>)))
                {
                    //Couldn't find the recycler tab.
                    return;
                }

                tabsView.TrySelectTabByIndex(recyclerEntry.Key);
            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in ScreenWithShipCargo_Configure_Patch Postfix", ex);
            }
        }
    }


}