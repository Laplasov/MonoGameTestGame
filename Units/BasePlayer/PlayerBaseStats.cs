using Project1.Units.UnitProfilePlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Units.BasePlayer
{
    public static class PlayerBaseStats
    {
        public static UnitStats GetStats()
        {
            return new UnitStats()
            {
                MaxHealth = 100,
                CurrentHealth = 100,
                MaxSpellPoints = 10,
                CurrentSpellPoints = 10,
                Physic = 10,
                Magic = 10,
                Defense = 10,
                Speed = 10,
            };
        }
    }
}
