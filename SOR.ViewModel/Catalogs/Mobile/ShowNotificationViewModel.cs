﻿using System.Collections.Generic;

namespace SOR.ViewModel.Catalogs.Mobile
{
    public class ShowNotificationViewModel
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public List<Results> results { get; set; }
    }
    public class Results
    {
        public string message_id { get; set; }
    }
}
