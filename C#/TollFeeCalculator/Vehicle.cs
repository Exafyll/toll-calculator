using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Vehicle
    {
        public enum VehicleType
        {
            Car,
            Motorbike,
            Tractor,
            Emergency,
            Diplomat,
            Foreign,
            Military
        }
        private VehicleType _type;
        public VehicleType Type { get { return _type; } }
        public Vehicle(VehicleType type)
        {
            _type = type;
        }

    }
}