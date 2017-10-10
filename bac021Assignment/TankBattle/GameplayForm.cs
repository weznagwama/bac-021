﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle {
    public partial class GameplayForm : Form {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Battle currentGame;

        private string currentPlayer = "player1";
        private  int currentWind = 0;
        //private static ControlledTank controlledTank;
        private TankType theTank;

        private string playerName;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        decimal aimAngle;
        int thePower;

        string[] imageFilenames = { "Images\\background1.jpg",
            "Images\\background2.jpg",
            "Images\\background3.jpg",
            "Images\\background4.jpg"};

        Color[] landscapeColours = { Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 73, 58, 47),
            Color.FromArgb(255, 148, 116, 93),
            Color.FromArgb(255, 133, 119, 109) };

        private Color landscape;
        //private Image image;

        public GameplayForm(Battle game) {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            currentGame = game;
            int rand = rng.Next(1, 4);
           
            backgroundImage = new Bitmap(imageFilenames[rand]);//doesn't load
            landscapeColour = landscapeColours[rand]; //
            playerName = currentGame.playerArray[currentGame.currentPlayer].GetName();
           

           

            InitializeComponent();
            backgroundGraphics = InitialiseBuffer();
            gameplayGraphics = InitialiseBuffer();
            DrawGameplay();
            NewTurn();

            DrawBackground();
            label10.Text = "0";



        }

        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void EnableTankControls() {
            controlPanel.Enabled = true;
        }

        public void Aim(float angle) {
            currentGame.controlledTankArray[currentGame.currentPlayer].Aim(angle);
            numericUpDown1.Value = (decimal) angle;
        }

        public void SetPower(int power) {
            currentGame.controlledTankArray[currentGame.currentPlayer].SetPower(power);
            trackBar1.Value = power;
        }
        public void SetWeaponIndex(int weapon) {
            currentGame.CurrentPlayerTank().SetWeaponIndex(weapon);
        }

        public void Launch() {
            var tank = currentGame.CurrentPlayerTank();
            tank.Launch();
            controlPanel.Enabled = false;
            timer1.Enabled = true;

        }

        private void DrawBackground() {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Terrain battlefield = currentGame.GetBattlefield();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Terrain.HEIGHT; y++) {
                for (int x = 0; x < Terrain.WIDTH; x++) {
                    if (battlefield.IsTileAt(x, y)) {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitialiseBuffer() {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void DrawGameplay() {
            //Renders the backgroundGraphics buffer to the gameplayGraphics buffer: backgroundGraphics.Render(gameplayGraphics.Graphics);
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            //Calls currentGame.DisplayPlayerTanks(), passing in gameplayGraphics.Graphics and displayPanel.Size
            currentGame.DisplayPlayerTanks(gameplayGraphics.Graphics, displayPanel.Size);
            //Calls currentGame.DrawWeaponEffects(), passing in gameplayGraphics.Graphics and displayPanel.Size
            currentGame.DrawWeaponEffects(gameplayGraphics.Graphics, displayPanel.Size);
            //This currently breaks about 5 units tests
        }

        private void NewTurn() {
            //First, get a reference to the current ControlledTank with currentGame.CurrentPlayerTank()
            ControlledTank currentTank = currentGame.CurrentPlayerTank();
            GenericPlayer currentPlayer = currentTank.GetPlayerNumber();
            string direction;
            //Likewise, get a reference to the current GenericPlayer by calling the ControlledTank's GetPlayerNumber()
            //Set the form caption to "Tank Battle - Round ? of ?", using methods in currentGame to get the current and total rounds.
            displayPanel.Text = string.Format("Tank Battle - Round {0} of {1}", currentGame.GetRound(), currentGame.GetRounds());

            controlPanel.BackColor = currentPlayer.PlayerColour();
            //Set the BackColor property of controlPanel to the current GenericPlayer's colour.
            label9.Text = currentPlayer.GetName();
            //Set the player name label to the current GenericPlayer's name.

            currentTank.Aim(currentTank.GetTankAngle());
            //Call Aim() to set the current angle to the current ControlledTank's angle.
            currentTank.SetPower(currentTank.GetPower());
            //Call SetPower() to set the current turret power to the current ControlledTank's power.
            if (currentGame.Wind() < 0) {
                direction = "W";
            } else {
                direction = "E";
            }

            label3.Text = String.Format("{0} {1}", currentGame.Wind(), direction);
            //Update the wind speed label to show the current wind speed, retrieved from currentGame.Positive values should be shown as E winds, negative values as W winds.For example, 
            //50 would be displayed as "50 E" while -38 would be displayed as "38 W".
            //Clear the current weapon names from the ComboBox.

            //Get a reference to the current TankType with ControlledTank's CreateTank() method, then get a list of weapons available to that TankType.
            TankType currentTankType = currentTank.CreateTank();
            var weapons = currentTankType.ListWeapons();
            comboBox1.DataSource = weapons;

            //Call SetWeaponIndex() to set the current weapon to the current ControlledTank's weapon.
            this.SetWeaponIndex(currentTank.GetWeapon());
            //Call the current GenericPlayer's CommenceTurn() method, passing in this and currentGame.
            currentPlayer.CommenceTurn(this, currentGame);
        }

        private void timer1_Tick(object sender, EventArgs e) {
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {
            //label3.Text = "windspeed here";
        }

        private void label4_Click(object sender, EventArgs e) {

        }

        private void label5_Click(object sender, EventArgs e) {

        }

        private void label6_Click(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            //Next, create ValueChanged(or SelectedIndexChanged) events for the combo control on the control panel.
            //The methods tied to each of these events should call the appropriate ControlledTank method(SetWeaponindex)
            currentGame.controlledTankArray[currentGame.currentPlayer].SetWeaponIndex(0);
        }

        private void button1_Click(object sender, EventArgs e) {
            
            var currentOne = currentGame.CurrentPlayerTank();
            currentOne.Launch();
            controlPanel.Enabled = false;
            timer1.Enabled = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            //Next, create ValueChanged(or SelectedIndexChanged) events for the numericUpDown control on the control panel.
            //The methods tied to each of these events should call the appropriate ControlledTank method(Aim())
            currentGame.controlledTankArray[currentGame.currentPlayer].Aim((int)numericUpDown1.Value);


        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            //Next, create ValueChanged(or SelectedIndexChanged) events for the TrackBar control on the control panel.
            //The methods tied to each of these events should call the appropriate ControlledTank SetPower(). 
            label10.Text = "" + trackBar1.Value;
            currentGame.controlledTankArray[currentGame.currentPlayer].SetPower(trackBar1.Value);
        }

        private void controlPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void GameplayForm_Load(object sender, EventArgs e) {

        }

        private void label9_Click(object sender, EventArgs e) {
            label9.Text = this.playerName;
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }
    }
}
