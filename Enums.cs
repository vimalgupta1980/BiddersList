using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiddersList
{
    /// <summary>
    /// Bidder list application pages
    /// </summary>
    internal enum BidderListPages
    {
        MainPage = 0,
        DataEntryPage = 1
    }

    /// <summary>
    /// The filter criteria
    /// </summary>
    internal enum FilterCriteria
    {
        None = 0,
        VendorType = 1,
        Region = 2,
        CostCode = 3
    }

}
