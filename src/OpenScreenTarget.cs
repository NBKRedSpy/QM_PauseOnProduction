using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_PauseOnProduction
{
    public enum OpenScreenTarget
    {
        None, 
        /// <summary>
        /// Open the cargo screen to the recycler tab.
        /// </summary>
        Recycler,

        /// <summary>
        /// Open the recycler tab.  This can only be set after the Recycler state has been processed.
        /// Required since the recycler screen patch is used by the arsenal screen as well.
        /// </summary>
        RecyclerTab,

        /// <summary>
        /// Open the production screen.
        /// </summary>
        Production
    }
}
