using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1.Abilities
{
    public enum DamageType { Physical, Magical, Almighty }
    public enum TargetRange { Melee, Range, Self, Area, RowMelee, RowRange, Piercing, All }
    public enum Target { Enemy, Ally, All }
    public enum CostType { HP, SP, Item, Cooldown }
    public enum StatType{ Physic, Defense, Magic, Speed }
    public enum StatusEffectType { None }
    public class Ability
    {
        [XmlElement] public string Name { get; set; }
        [XmlElement] public string Description { get; set; }
        [XmlElement] public TargetRange Range { get; set; }
        [XmlElement] public Target Target { get; set; }

        [XmlArray("Scales")]
        [XmlArrayItem("ScaleEntry")]
        public List<ScaleEntry> Scales { get; set; } = new();

        [XmlArray("Costs")]
        [XmlArrayItem("CostEntry")]
        public List<CostEntry> Costs { get; set; } = new();

        [XmlArray("StatusEffects")]
        [XmlArrayItem("StatusEntry")]
        public List<StatusEntry> StatusEffects { get; set; } = new();
    }

    public struct ScaleEntry
    {
        [XmlAttribute] public StatType Stat { get; set; }
        [XmlAttribute] public float Percentage { get; set; }
    }
    public struct CostEntry
    {
        [XmlAttribute] public CostType Type { get; set; }
        [XmlAttribute] public int Value { get; set; }
    }
    public struct StatusEntry
    {
        [XmlAttribute] public StatusEffectType Type { get; set; }
        [XmlAttribute] public int Value { get; set; }
    }
}
