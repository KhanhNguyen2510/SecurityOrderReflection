using System.Collections.Generic;

namespace SOR.ViewModel.Catalogs.Mobile
{
    public class GetNotificationRequest
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
    }

    public class Notification
    {
        public string tille { get; set; }
        public string body { get; set; }
    }
}
