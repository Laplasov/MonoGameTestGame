using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Units.BasePlayer;
using Project1.Units.UnitProfilePlace;

namespace Project1.Units
{
    public class PlayerManager : UnitBaseManager
    {
        protected override string PlayerAtlasXML { set; get; } = "Atlases/CharacterAtlas.xml";
        public override string PlayerName { set; get; } = "Player";
        public PlayerManager() { }
        public PlayerManager WithPosition(Vector2 vec)
        {
            Position = vec;
            return this;
        }
        public PlayerManager WithName(string name)
        {
            PlayerName = name;
            return this;
        }
        public override void CreateUnits()
        {
            foreach (var unit in UnitPositions)
            {
                var baseStats = PlayerBaseStats.GetStats();
                var baseAbility = PlayerBaseAbilities.GetAbilities();
                var playerUnit = new UnitProfile(PlayerName, unit.Value, baseStats, baseAbility, unit.Key, IsAlly);
                UnitList.Add(playerUnit);
            }
        }
        protected override Vector2 InputHandel(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) movement.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) movement.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) movement.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) movement.Y += 1;
            if (movement.LengthSquared() > 1f)
                movement.Normalize();
            return movement;
        }
    }
}
