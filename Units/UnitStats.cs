using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Units
{
    public class UnitStats
    {
        // Core combat stats
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxSpellPoints { get; set; }
        public int CurrentSpellPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }

        // Derived properties
        public bool IsAlive => CurrentHealth > 0;
        public UnitStats() { }
        public UnitStats Clone()
        {
            return new UnitStats
            {
                MaxHealth = MaxHealth,
                CurrentHealth = MaxHealth,
                Attack = Attack,
                Magic = Magic,
                Defense = Defense,
                MaxSpellPoints = MaxSpellPoints,
                CurrentSpellPoints = MaxSpellPoints
            };
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - amount);
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }

        public void SpendSP(int amount)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - amount);
        }

        public void RestorSP(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }
    }
}
