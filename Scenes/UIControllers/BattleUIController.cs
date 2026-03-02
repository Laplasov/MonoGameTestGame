using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame_Game_Library;
using MonoGameGum.GueDeriving;
using Project1.Abilities;
using Project1.Components.Castom;
using Project1.Save;
using Project1.Screens;
using Project1.UI;
using Project1.Units.UnitProfilePlace;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using Button = Gum.Forms.Controls.Button;

namespace Project1.Scenes
{
    public class BattleUIController
    {
        CameraViewManager _cameraManage;
        List<UnitProfile> _units;
        UnitProfile _currentUnit;
        GraphicalUiElement _currentButton;

        BattleMenuRuntime _screen;
        List<Button> _buttons;

        private float _timer = 0f;
        List<IVisible> HUD;

        public BattleUIController(CameraViewManager cameraManage, List<UnitProfile> units)
        {
            _cameraManage = cameraManage;
            _units = units;
            _currentUnit = null;
            HUD = new List<IVisible>();
        }

        public void Initialize(string screenName)
        {
            _screen = GameCore.SetScreenGum(screenName) as BattleMenuRuntime;

            var ui = GameCore.GumElement;

            var skill = ui.BindButton(nameof(_screen.SkillButton), SkillBuitton);
            var item = ui.BindButton(nameof(_screen.ItemButton), ItemButton);
            var wait = ui.BindButton(nameof(_screen.WaitButton), WaitButton);
            var run = ui.BindButton(nameof(_screen.RunButton), RunButton);

            _screen.DescriptionWindow.Visible = false;
            _screen.UnitBar.Visible = false;
            _buttons = new List<Button>();

            SetRoll(skill);
            SetRoll(item);
            SetRoll(wait);
            SetRoll(run);

            HUD.Add(_screen.EnemySlots);
            HUD.Add(_screen.AllaySlots);
            HUD.Add(_screen.ActionsBar);

            ShowWelcome();

        }

        public void Resolve()
        {
            foreach (var button in _buttons)
            {
                button.Visual.RollOn -= ShowDescription;
                button.Visual.RollOff -= HideDescription;
            }
            _buttons.Clear();
            GameCore.UnloadCurrentUI();
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_screen.EnteringTextWindow.Visible == true)
                UpdateWelcome();
            else
                CheckUnitHover();
        }

        private void CheckUnitHover()
        {
            Ray mouseRay = _cameraManage.GetMouseRay();

            UnitProfile newUnit = null;

            foreach (var unit in _units)
            {
                if (unit.UnitView?.Sprite != null)
                {
                    Vector3 basePos = unit.UnitView.Sprite.Position;
                    Vector2 size = unit.UnitView.Sprite.Size;

                    // Unit center (sprite is centered horizontally, bottom at basePos.Y)
                    Vector3 unitCenter = new Vector3(
                        basePos.X,
                        basePos.Y + (size.Y / 2),
                        basePos.Z
                    );

                    // Radius based on sprite dimensions
                    float radius = (size.X + size.Y) / 4;

                    if (mouseRay.Intersects(new BoundingSphere(unitCenter, radius)).HasValue)
                    {
                        newUnit = unit;
                        break; 
                    }
                }
            }

            if (newUnit != _currentUnit)
            {
                _currentUnit = newUnit;
                if (newUnit == null)
                {
                    _screen.UnitBar.Visible = false;
                }
                else
                {
                    _screen.UnitBar.Visible = true;
                    _screen.UnitBarText.Text = SetUnitInfo(newUnit);
                }
            }
        }

        string SetUnitInfo(UnitProfile profile)
        {
            var info = $"{profile.Name} \n\n" +
                $"Health {profile.Stats.MaxHealth}/" + $"{profile.Stats.CurrentHealth}  \n" +
                $"SpellPoints {profile.Stats.MaxSpellPoints}/" + $"{profile.Stats.CurrentSpellPoints}  \n\n" +
                $"PYS {profile.Stats.Physic} " + $"MAG {profile.Stats.Magic} \n" + 
                $"DEF {profile.Stats.Defense} " + $"SPD {profile.Stats.Speed}  \n\n" +
                $"Abilities:  \n";

            Ability[] abilities = profile.Abilities.Abilities;

            for (int i = 0; i < abilities.Length; i++)
            {
                var ability = abilities[i];
                if (ability != null)
                {
                    info += $"  • {ability.Name}\n";
                }
            }


            return info;
        }

        void SetRoll(Button button)
        {
            button.Visual.RollOn += ShowDescription;
            button.Visual.RollOff += HideDescription;
            _buttons.Add(button);
        }

        void ShowDescription(object sender, EventArgs e)
        {
            _screen.DescriptionWindow.Visible = true;
            var button = sender as GraphicalUiElement;
            _currentButton = button;
            _screen.TextDescription.Text = GetButtonDescription(button.Name);

        }
        private string GetButtonDescription(string buttonName)
        {
            return buttonName switch
            {
                nameof(_screen.SkillButton) => "Use a special skill",
                nameof(_screen.ItemButton) => "Use an item from inventory",
                nameof(_screen.WaitButton) => "Wait and restore some MP",
                nameof(_screen.RunButton) => "Attempt to flee from battle",
                _ => ""
            };
        }
        public void ShowUnit(string name)
        {

            _screen.UnitBar.Visible = true;
            _screen.UnitBarText.Text = name;

        }

        void HideDescription(object sender, EventArgs e)
        {
            var button = sender as GraphicalUiElement;
            if (_currentButton == button)
                _screen.DescriptionWindow.Visible = false;
        }

        public void ShowWelcome()
        {
            foreach (var element in HUD) 
                element.Visible = false;

            _screen.EnteringTextWindow.Visible = true;
        }
        public void HideWelcome()
        {
            foreach (var element in HUD)
                element.Visible = true;

            _screen.EnteringTextWindow.Visible = false;
        }
        public void UpdateWelcome()
        {
            float cycleDuration = 2.0f;
            float alphaMin = 0.2f;
            float alphaMax = 1.0f;

            float cycleProgress = (_timer % cycleDuration) / cycleDuration;

            float t = cycleProgress < 0.5f? cycleProgress * 2f: 2f - cycleProgress * 2f;

            float alphaFloat = alphaMin + t * (alphaMax - alphaMin);
            int alpha = (int)(alphaFloat * 255);

            _screen.EnteringTextWindow.NineSliceInstance.Alpha = alpha;
            _screen.EnteringText.Alpha = alpha;

        }

        void SkillBuitton(object sender, EventArgs e) { }
        void ItemButton(object sender, EventArgs e) { }
        void WaitButton(object sender, EventArgs e) { }
        void RunButton(object sender, EventArgs e) { }

    }
}