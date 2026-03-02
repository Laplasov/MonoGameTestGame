using Project1.Units.UnitProfilePlace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes.CumulativeSystem
{
    public class CumulativeTurnSystem
    {
        private List<UnitProfile> _units = new List<UnitProfile>();
        private int _depth;

        private List<TurnState> _possibleUnits = new List<TurnState>();
        private List<UnitProfile> _result = new List<UnitProfile>();
        private int _maxTicks = 100;

        private class TurnState
        {
            public UnitProfile Unit;
            public int Speed;
            public int SpeedThreshold;
            public int CumulativeSpeed;

            public TurnState(UnitProfile unit)
            {
                Unit = unit;
                Speed = unit.Stats.Speed;
                SpeedThreshold = unit.SpeedThreshold;
                CumulativeSpeed = unit.CumulativeSpeed;
            }
        }

        public CumulativeTurnSystem(List<UnitProfile> units, int depth = 10)
        {
            _units = units;
            _depth = depth;
        }
        public List<UnitProfile> CalculatePossibleUnitsTurns()
        {
            _possibleUnits.Clear();
            _result.Clear();

            foreach (var unit in _units.Where(u => u.Stats.Speed > 0)) 
            {
                _possibleUnits.Add(new TurnState(unit));
            }

            if (_possibleUnits.Count == 0) return _result;

            int tick = 0;

            while (_result.Count < _depth)
            {
                foreach (var state in _possibleUnits)
                {
                    state.CumulativeSpeed += state.Speed;  
                }

                var readyUnits = _possibleUnits
                    .Where(s => s.CumulativeSpeed >= s.SpeedThreshold)
                    .ToList();

                if (readyUnits.Count > 0 && tick < _maxTicks)
                {
                    tick++;
                    var sorted = readyUnits
                        .OrderByDescending(s => s.Speed)                 // Higher speed breaks ties
                        .ThenBy(s => s.Unit.GridIndex)                  // Grid position
                        .ThenBy(s => s.Unit.IsAlly ? 0 : 1)             // Ally priority
                        .ToList();

                    foreach (var state in sorted)
                    {
                        if (_result.Count >= _depth) break;

                        _result.Add(state.Unit);
                        state.CumulativeSpeed -= state.SpeedThreshold; 
                    }
                }
            }
            _possibleUnits.Clear();
            return _result;
        }

    }
}
