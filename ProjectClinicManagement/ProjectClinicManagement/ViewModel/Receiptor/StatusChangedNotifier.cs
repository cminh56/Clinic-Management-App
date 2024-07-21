using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.ViewModel.Receiptor
{
    public class StatusChangedNotifier
    {
        public static event Action StatusChanged;

        public static void NotifyStatusChanged()
        {
            StatusChanged?.Invoke();
        }
    }
}
