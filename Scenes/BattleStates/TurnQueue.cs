using Project1.Units.UnitProfilePlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes.BattleStates
{
    public class TurnQueue
    {
        private IEnumerator<UnitProfile> _turnEnumerator;
        private List<UnitProfile> _units;
        public UnitProfile CurrentUnit => _turnEnumerator?.Current;
        public int CurrentRound { get; private set; } = 1;

        public TurnQueue(List<UnitProfile> units) => _units = units;
        public void InitializeTurnOrder(List<UnitProfile> units)
        {
            var sorted = units
                .OrderByDescending(u => u.Stats.Speed)      // 1. Speed: high → low
                .ThenBy(u => u.GridIndex)                    // 2. Grid index: low → high
                .ThenBy(u => u.IsAlly ? 0 : 1)               // 3. Ally priority: allies first
                .ToList();

            _turnEnumerator = sorted.GetEnumerator();
        }
        public UnitProfile Next()
        {
            if (NextTurn() == false)
            {
                ResetTurnOrder(_units);
                CurrentRound++;
                return CurrentUnit;
            }
            return CurrentUnit;
        }
        public bool NextTurn() => _turnEnumerator?.MoveNext() ?? false;
        public void ResetTurnOrder(List<UnitProfile> units)
        {
            _turnEnumerator?.Dispose();
            InitializeTurnOrder(units);
        }
    }
}
