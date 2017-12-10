using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreService.CustomModels
{
    public class MapDay_ProductEstConsumption
    {
        public Dictionary<DayOfWeek, float> ProductEstConsumption { get; set; }
        public MapDay_ProductEstConsumption()
        {
            ProductEstConsumption = new Dictionary<DayOfWeek, float>();
        }
    }
}
