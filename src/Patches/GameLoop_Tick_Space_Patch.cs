using HarmonyLib;
using MGSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_PauseOnProduction.Patches
{

    /// <summary>
    /// If a production line has completed during the space tick, sets a flag to show the production screen
    /// </summary>
    [HarmonyPatch(typeof(GameLoop), nameof(GameLoop.Tick_Space))]
    public static class GameLoop_Tick_Space_Patch
    {

        /// <summary>
        /// The list of production lines that had an order processing before the production loop
        /// </summary>
        private static List<bool> ProductionLinesStatus { get; set; }


        //[HarmonyBefore("NBKRedSpy_ProduceAsReady")]
        public static void Prefix()
        {
            try
            {
                MagnumCargo magnumCargo = Plugin.State.Get<MagnumCargo>();
                //Get list of items with only one item in the queue
                ProductionLinesStatus = magnumCargo.ItemProduceOrders.Values.Select(x => x.Count == 1).ToList();
            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in ItemProductionSystem__Update_Patch.Prefix", ex);
            }
        }

        public static void Postfix()
        {
            try
            {
                MagnumCargo magnumCargo = Plugin.State.Get<MagnumCargo>();

                bool queueCompleted = magnumCargo.ItemProduceOrders.Values
                    .Zip(ProductionLinesStatus, (current, previous) => new { current, previous })
                    .Any(x => x.current.Count == 0 && x.previous == true);

                if (queueCompleted)
                {
                    Plugin.OpenScreenTarget = OpenScreenTarget.Production;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log($"Exception in ItemProductionSystem__Update_Patch.Postfix", ex);
            }
        }
    }


}