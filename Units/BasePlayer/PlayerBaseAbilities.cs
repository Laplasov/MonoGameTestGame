using Project1.Abilities;
using Project1.Units.UnitProfilePlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Units.BasePlayer
{
    public static class PlayerBaseAbilities
    {
        public static UnitAbilities GetAbilities()
        {
            var unitAbilities = new UnitAbilities();

            unitAbilities.Abilities[0] = new Ability
            {
                Name = "Base Attack",
                Description = "Simple physical attack.",
                Range = TargetRange.Melee,
                Target = Target.Enemy,
                Scales = new() { new ScaleEntry { Stat = StatType.Physic, Percentage = 80 } },
                Costs = new(),
                StatusEffects = new()
            };

            return unitAbilities;
        }
    }
}
