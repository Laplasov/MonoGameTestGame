using Microsoft.Xna.Framework.Content;
using MonoGame_Game_Library.TileLogic;
using Project1.Logic;
using Project1.Save;
using Project1.Save.Bestiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project1.Units
{
    public class EnemySceneCollection
    {
        private PlayerManager _playerManager;
        private BestiaryLoader _bestiaryLoader;
        private List<EnemyManager> _enemies = new List<EnemyManager>();
        private TileLayer _spawnLayer;
        private SceneData _sceneData;

        public int Count => _enemies.Count;

        public EnemySceneCollection(PlayerManager playerManager, TileLayer tileLayer, SceneData sceneData, ContentManager content)
        {
            _playerManager = playerManager;
            _spawnLayer = tileLayer;
            _sceneData = sceneData;
            _bestiaryLoader = new BestiaryLoader(content);

            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (var spawn in _sceneData.EnemySpawns)
            {
                var position = TilePositionHelper.GetTileCenterPosition(
                    spawn.TileId,
                    _spawnLayer,
                    _sceneData.LayerScale
                );

                if (!position.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine($"Tile {spawn.TileId} not found for enemy {spawn.EnemyName}");
                    continue;
                }

                var template = _bestiaryLoader.GetEnemyTemplate(spawn.EnemyName);
                if (template == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Enemy template '{spawn.EnemyName}' not found!");
                    continue;
                }

                // Create the enemy manager
                var enemy = new EnemyManager(
                    _playerManager,
                    spawn,
                    template,
                    position.Value,
                    _sceneData
                );
                _enemies.Add(enemy);
            }
        }
        public IEnumerable<EnemyManager> GetEnemies() => _enemies;
        public void RemoveEnemy(EnemyManager enemy) => _enemies.Remove(enemy);
        public void Load(ContentManager content)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Load(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Draw();
            }
        }
    }
}
