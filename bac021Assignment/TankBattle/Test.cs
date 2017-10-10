using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankBattle;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TankBattleTestSuite {
    class RequirementException : Exception {
        public RequirementException() {
        }

        public RequirementException(string message) : base(message) {
        }

        public RequirementException(string message, Exception inner) : base(message, inner) {
        }
    }

    class Test {
        #region Testing Code

        private delegate bool TestCase();

        private static string ErrorDescription = null;

        private static void SetErrorDescription(string desc) {
            ErrorDescription = desc;
        }

        private static bool FloatEquals(float a, float b) {
            if (Math.Abs(a - b) < 0.01) return true;
            return false;
        }

        private static Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

        private static void Passed(string name, string comment) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[passed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "") {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null) {
                throw new Exception("ErrorDescription found for passing test case");
            }
            Console.WriteLine();
        }
        private static void Failed(string name, string comment) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "") {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null) {
                Console.Write("\n{0}", ErrorDescription);
                ErrorDescription = null;
            }
            Console.WriteLine();
        }
        private static void FailedToMeetRequirement(string name, string comment) {
            Console.Write("[      ] ");
            Console.Write("{0}", name);
            if (comment != "") {
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("{0}", comment);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void DoTest(TestCase test) {
            // Have we already completed this test?
            if (unitTestResults.ContainsKey(test.Method.ToString())) {
                return;
            }

            bool passed = false;
            bool metRequirement = true;
            string exception = "";
            try {
                passed = test();
            } catch (RequirementException e) {
                metRequirement = false;
                exception = e.Message;
            } catch (Exception e) {
                exception = e.GetType().ToString();
            }

            string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
            string fnName = test.Method.ToString().Split('0')[1];

            if (metRequirement) {
                if (passed) {
                    unitTestResults[test.Method.ToString()] = "Passed";
                    Passed(string.Format("{0}.{1}", className, fnName), exception);
                } else {
                    unitTestResults[test.Method.ToString()] = "Failed";
                    Failed(string.Format("{0}.{1}", className, fnName), exception);
                }
            } else {
                unitTestResults[test.Method.ToString()] = "Failed";
                FailedToMeetRequirement(string.Format("{0}.{1}", className, fnName), exception);
            }
            Cleanup();
        }

        private static Stack<string> errorDescriptionStack = new Stack<string>();


        private static void Requires(TestCase test) {
            string result;
            bool wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

            if (!wasTested) {
                // Push the error description onto the stack (only thing that can change, not that it should)
                errorDescriptionStack.Push(ErrorDescription);

                // Do the test
                DoTest(test);

                // Pop the description off
                ErrorDescription = errorDescriptionStack.Pop();

                // Get the proper result for out
                wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

                if (!wasTested) {
                    throw new Exception("This should never happen");
                }
            }

            if (result == "Failed") {
                string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
                string fnName = test.Method.ToString().Split('0')[1];

                throw new RequirementException(string.Format("-> {0}.{1}", className, fnName));
            } else if (result == "Passed") {
                return;
            } else {
                throw new Exception("This should never happen");
            }

        }

        #endregion

        #region Test Cases
        private static Battle InitialiseGame() {
            Requires(TestBattle0Battle);
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);
            Requires(TestBattle0SetPlayer);

            Battle game = new Battle(2, 1);
            TankType tank = TankType.CreateTank(1);
            GenericPlayer player1 = new HumanOpponent("player1", tank, Color.Orange);
            GenericPlayer player2 = new HumanOpponent("player2", tank, Color.Purple);
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);
            return game;
        }
        private static void Cleanup() {
            while (Application.OpenForms.Count > 0) {
                Application.OpenForms[0].Dispose();
            }
        }
        private static bool TestBattle0Battle() {
            Battle game = new Battle(2, 1);
            return true;
        }
        private static bool TestBattle0PlayerCount() {
            Requires(TestBattle0Battle);

            Battle game = new Battle(2, 1);
            return game.PlayerCount() == 2;
        }
        private static bool TestBattle0GetRounds() {
            Requires(TestBattle0Battle);

            Battle game = new Battle(3, 5);
            return game.GetRounds() == 5;
        }
        private static bool TestBattle0SetPlayer() {
            Requires(TestBattle0Battle);
            Requires(TestTankType0CreateTank);

            Battle game = new Battle(2, 1);
            TankType tank = TankType.CreateTank(1);
            GenericPlayer player = new HumanOpponent("playerName", tank, Color.Orange);
            game.SetPlayer(1, player);
            return true;
        }
        private static bool TestBattle0GetPlayerNumber() {
            Requires(TestBattle0Battle);
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);

            Battle game = new Battle(2, 1);
            TankType tank = TankType.CreateTank(1);
            GenericPlayer player = new HumanOpponent("playerName", tank, Color.Orange);
            game.SetPlayer(1, player);
            return game.GetPlayerNumber(1) == player;
        }
        private static bool TestBattle0PlayerColour() {
            Color[] arrayOfColours = new Color[8];
            for (int i = 0; i < 8; i++) {
                arrayOfColours[i] = Battle.PlayerColour(i + 1);
                for (int j = 0; j < i; j++) {
                    if (arrayOfColours[j] == arrayOfColours[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0CalculatePlayerPositions() {
            int[] positions = Battle.CalculatePlayerPositions(8);
            for (int i = 0; i < 8; i++) {
                if (positions[i] < 0) return false;
                if (positions[i] > 160) return false;
                for (int j = 0; j < i; j++) {
                    if (positions[j] == positions[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0Shuffle() {
            int[] ar = new int[100];
            for (int i = 0; i < 100; i++) {
                ar[i] = i;
            }
            Battle.Shuffle(ar);
            for (int i = 0; i < 100; i++) {
                if (ar[i] != i) {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0NewGame() {
            Battle game = InitialiseGame();
            game.NewGame();

            foreach (Form f in Application.OpenForms) {
                if (f is GameplayForm) {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0GetBattlefield() {
            Requires(TestTerrain0Terrain);
            Battle game = InitialiseGame();
            game.NewGame();
            Terrain battlefield = game.GetBattlefield();
            if (battlefield != null) return true;

            return false;
        }
        private static bool TestBattle0CurrentPlayerTank() {
            Requires(TestBattle0Battle);
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);
            Requires(TestBattle0SetPlayer);
            Requires(TestControlledTank0GetPlayerNumber);

            Battle game = new Battle(2, 1);
            TankType tank = TankType.CreateTank(1);
            GenericPlayer player1 = new HumanOpponent("player1", tank, Color.Orange);
            GenericPlayer player2 = new HumanOpponent("player2", tank, Color.Purple);
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);

            game.NewGame();
            ControlledTank ptank = game.CurrentPlayerTank();
            if (ptank.GetPlayerNumber() != player1 && ptank.GetPlayerNumber() != player2) {
                return false;
            }
            if (ptank.CreateTank() != tank) {
                return false;
            }

            return true;
        }

        private static bool TestTankType0CreateTank() {
            TankType tank = TankType.CreateTank(1);
            if (tank != null) return true;
            else return false;
        }
        private static bool TestTankType0DisplayTank() {
            Requires(TestTankType0CreateTank);
            TankType tank = TankType.CreateTank(1);

            int[,] tankGraphic = tank.DisplayTank(45);
            if (tankGraphic.GetLength(0) != 12) return false;
            if (tankGraphic.GetLength(1) != 16) return false;
            // We don't really care what the tank looks like, but the 45 degree tank
            // should at least look different to the -45 degree tank
            int[,] tankGraphic2 = tank.DisplayTank(-45);
            for (int y = 0; y < 12; y++) {
                for (int x = 0; x < 16; x++) {
                    if (tankGraphic2[y, x] != tankGraphic[y, x]) {
                        return true;
                    }
                }
            }

            SetErrorDescription("Tank with turret at -45 degrees looks the same as tank with turret at 45 degrees");

            return false;
        }
        private static void DisplayLine(int[,] array) {
            string report = "";
            report += "A line drawn from 3,0 to 0,3 on a 4x4 array should look like this:\n";
            report += "0001\n";
            report += "0010\n";
            report += "0100\n";
            report += "1000\n";
            report += "The one produced by TankType.DrawLine() looks like this:\n";
            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++) {
                    report += array[y, x] == 1 ? "1" : "0";
                }
                report += "\n";
            }
            SetErrorDescription(report);
        }
        private static bool TestTankType0DrawLine() {
            int[,] ar = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 } };
            TankType.DrawLine(ar, 3, 0, 0, 3);

            // Ideally, the line we want to see here is:
            // 0001
            // 0010
            // 0100
            // 1000

            // However, as we aren't that picky, as long as they have a 1 in every row and column
            // and nothing in the top-left and bottom-right corners

            int[] rows = new int[4];
            int[] cols = new int[4];
            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++) {
                    if (ar[y, x] == 1) {
                        rows[y] = 1;
                        cols[x] = 1;
                    } else if (ar[y, x] > 1 || ar[y, x] < 0) {
                        // Only values 0 and 1 are permitted
                        SetErrorDescription(string.Format("Somehow the number {0} got into the array.", ar[y, x]));
                        return false;
                    }
                }
            }

            for (int i = 0; i < 4; i++) {
                if (rows[i] == 0) {
                    DisplayLine(ar);
                    return false;
                }
                if (cols[i] == 0) {
                    DisplayLine(ar);
                    return false;
                }
            }
            if (ar[0, 0] == 1) {
                DisplayLine(ar);
                return false;
            }
            if (ar[3, 3] == 1) {
                DisplayLine(ar);
                return false;
            }

            return true;
        }
        private static bool TestTankType0GetTankArmour() {
            Requires(TestTankType0CreateTank);
            // As long as it's > 0 we're happy
            TankType tank = TankType.CreateTank(1);
            if (tank.GetTankArmour() > 0) return true;
            return false;
        }
        private static bool TestTankType0ListWeapons() {
            Requires(TestTankType0CreateTank);
            // As long as there's at least one result and it's not null / a blank string, we're happy
            TankType tank = TankType.CreateTank(1);
            if (tank.ListWeapons().Length == 0) return false;
            if (tank.ListWeapons()[0] == null) return false;
            if (tank.ListWeapons()[0] == "") return false;
            return true;
        }

        private static GenericPlayer CreateTestingPlayer() {
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);

            TankType tank = TankType.CreateTank(1);
            GenericPlayer player = new HumanOpponent("player1", tank, Color.Aquamarine);
            return player;
        }

        private static bool TestGenericPlayer0HumanOpponent() {
            Requires(TestTankType0CreateTank);

            TankType tank = TankType.CreateTank(1);
            GenericPlayer player = new HumanOpponent("player1", tank, Color.Aquamarine);
            if (player != null) return true;
            return false;
        }
        private static bool TestGenericPlayer0CreateTank() {
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);

            TankType tank = TankType.CreateTank(1);
            GenericPlayer p = new HumanOpponent("player1", tank, Color.Aquamarine);
            if (p.CreateTank() == tank) return true;
            return false;
        }
        private static bool TestGenericPlayer0GetName() {
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);

            const string PLAYER_NAME = "kfdsahskfdajh";
            TankType tank = TankType.CreateTank(1);
            GenericPlayer p = new HumanOpponent(PLAYER_NAME, tank, Color.Aquamarine);
            if (p.GetName() == PLAYER_NAME) return true;
            return false;
        }
        private static bool TestGenericPlayer0PlayerColour() {
            Requires(TestTankType0CreateTank);
            Requires(TestGenericPlayer0HumanOpponent);

            Color playerColour = Color.Chartreuse;
            TankType tank = TankType.CreateTank(1);
            GenericPlayer p = new HumanOpponent("player1", tank, playerColour);
            if (p.PlayerColour() == playerColour) return true;
            return false;
        }
        private static bool TestGenericPlayer0AddPoint() {
            GenericPlayer p = CreateTestingPlayer();
            p.AddPoint();
            return true;
        }
        private static bool TestGenericPlayer0GetVictories() {
            Requires(TestGenericPlayer0AddPoint);

            GenericPlayer p = CreateTestingPlayer();
            int wins = p.GetVictories();
            p.AddPoint();
            if (p.GetVictories() == wins + 1) return true;
            return false;
        }
        private static bool TestHumanOpponent0BeginRound() {
            GenericPlayer p = CreateTestingPlayer();
            p.BeginRound();
            return true;
        }
        private static bool TestHumanOpponent0CommenceTurn() {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Battle game = InitialiseGame();

            game.NewGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms) {
                if (f is GameplayForm) {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null) {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls) {
                if (c is Panel) {
                    foreach (Control cc in c.Controls) {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar) {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null) {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that NewTurn enables it
            controlPanel.Enabled = false;

            game.GetPlayerNumber(1).CommenceTurn(gameplayForm, game);

            if (!controlPanel.Enabled) {
                SetErrorDescription("Control panel is still disabled after HumanPlayer.NewTurn()");
                return false;
            }
            return true;

        }
        private static bool TestHumanOpponent0ProjectileHitPos() {
            GenericPlayer p = CreateTestingPlayer();
            p.ProjectileHitPos(0, 0);
            return true;
        }

        private static bool TestControlledTank0ControlledTank() {
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            return true;
        }
        private static bool TestControlledTank0GetPlayerNumber() {
            Requires(TestControlledTank0ControlledTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            if (playerTank.GetPlayerNumber() == p) return true;
            return false;
        }
        private static bool TestControlledTank0CreateTank() {
            Requires(TestControlledTank0ControlledTank);
            Requires(TestGenericPlayer0CreateTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            if (playerTank.CreateTank() == playerTank.GetPlayerNumber().CreateTank()) return true;
            return false;
        }
        private static bool TestControlledTank0GetTankAngle() {
            Requires(TestControlledTank0ControlledTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            float angle = playerTank.GetTankAngle();
            if (angle >= -90 && angle <= 90) return true;
            return false;
        }
        private static bool TestControlledTank0Aim() {
            Requires(TestControlledTank0ControlledTank);
            Requires(TestControlledTank0GetTankAngle);
            float angle = 75;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.Aim(angle);
            if (FloatEquals(playerTank.GetTankAngle(), angle)) return true;
            return false;
        }
        private static bool TestControlledTank0GetPower() {
            Requires(TestControlledTank0ControlledTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);

            playerTank.GetPower();
            return true;
        }
        private static bool TestControlledTank0SetPower() {
            Requires(TestControlledTank0ControlledTank);
            Requires(TestControlledTank0GetPower);
            int power = 65;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.SetPower(power);
            if (playerTank.GetPower() == power) return true;
            return false;
        }
        private static bool TestControlledTank0GetWeapon() {
            Requires(TestControlledTank0ControlledTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);

            playerTank.GetWeapon();
            return true;
        }
        private static bool TestControlledTank0SetWeaponIndex() {
            Requires(TestControlledTank0ControlledTank);
            Requires(TestControlledTank0GetWeapon);
            int weapon = 3;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.SetWeaponIndex(weapon);
            if (playerTank.GetWeapon() == weapon) return true;
            return false;
        }
        private static bool TestControlledTank0Display() {
            Requires(TestControlledTank0ControlledTank);
            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.Display(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++) {
                for (int x = 0; x < bitmapSize.Width; x++) {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0)) {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestControlledTank0XPos() {
            Requires(TestControlledTank0ControlledTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, x, y, game);
            if (playerTank.XPos() == x) return true;
            return false;
        }
        private static bool TestControlledTank0Y() {
            Requires(TestControlledTank0ControlledTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, x, y, game);
            if (playerTank.Y() == y) return true;
            return false;
        }
        private static bool TestControlledTank0Launch() {
            Requires(TestControlledTank0ControlledTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.Launch();
            return true;
        }
        private static bool TestControlledTank0DamageArmour() {
            Requires(TestControlledTank0ControlledTank);
            GenericPlayer p = CreateTestingPlayer();

            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            playerTank.DamageArmour(10);
            return true;
        }
        private static bool TestControlledTank0IsAlive() {
            Requires(TestControlledTank0ControlledTank);
            Requires(TestControlledTank0DamageArmour);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
            if (!playerTank.IsAlive()) return false;
            playerTank.DamageArmour(playerTank.CreateTank().GetTankArmour());
            if (playerTank.IsAlive()) return false;
            return true;
        }
        private static bool TestControlledTank0CalculateGravity() {
            Requires(TestBattle0GetBattlefield);
            Requires(TestTerrain0DestroyGround);
            Requires(TestControlledTank0ControlledTank);
            Requires(TestControlledTank0DamageArmour);
            Requires(TestControlledTank0IsAlive);
            Requires(TestControlledTank0CreateTank);
            Requires(TestTankType0GetTankArmour);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            // Unfortunately we need to rely on DestroyTerrain() to get rid of any terrain that may be in the way
            game.GetBattlefield().DestroyGround(Terrain.WIDTH / 2.0f, Terrain.HEIGHT / 2.0f, 20);
            ControlledTank playerTank = new ControlledTank(p, Terrain.WIDTH / 2, Terrain.HEIGHT / 2, game);
            int oldX = playerTank.XPos();
            int oldY = playerTank.Y();

            playerTank.CalculateGravity();

            if (playerTank.XPos() != oldX) {
                SetErrorDescription("Caused X coordinate to change.");
                return false;
            }
            if (playerTank.Y() != oldY + 1) {
                SetErrorDescription("Did not cause Y coordinate to increase by 1.");
                return false;
            }

            int initialArmour = playerTank.CreateTank().GetTankArmour();
            // The tank should have lost 1 armour from falling 1 tile already, so do
            // (initialArmour - 2) damage to the tank then drop it again. That should kill it.

            if (!playerTank.IsAlive()) {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.DamageArmour(initialArmour - 2);
            if (!playerTank.IsAlive()) {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.CalculateGravity();
            if (playerTank.IsAlive()) {
                SetErrorDescription("Tank survived despite taking enough falling damage to destroy it");
                return false;
            }

            return true;
        }
        private static bool TestTerrain0Terrain() {
            Terrain battlefield = new Terrain();
            return true;
        }
        private static bool TestTerrain0IsTileAt() {
            Requires(TestTerrain0Terrain);

            bool foundTrue = false;
            bool foundFalse = false;
            Terrain battlefield = new Terrain();
            for (int y = 0; y < Terrain.HEIGHT; y++) {
                for (int x = 0; x < Terrain.WIDTH; x++) {
                    if (battlefield.IsTileAt(x, y)) {
                        foundTrue = true;
                    } else {
                        foundFalse = true;
                    }
                }
            }

            if (!foundTrue) {
                SetErrorDescription("IsTileAt() did not return true for any tile.");
                return false;
            }

            if (!foundFalse) {
                SetErrorDescription("IsTileAt() did not return false for any tile.");
                return false;
            }

            return true;
        }
        private static bool TestTerrain0CheckTankCollide() {
            Requires(TestTerrain0Terrain);
            Requires(TestTerrain0IsTileAt);

            Terrain battlefield = new Terrain();
            for (int y = 0; y <= Terrain.HEIGHT - TankType.HEIGHT; y++) {
                for (int x = 0; x <= Terrain.WIDTH - TankType.WIDTH; x++) {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankType.HEIGHT; iy++) {
                        for (int ix = 0; ix < TankType.WIDTH; ix++) {

                            if (battlefield.IsTileAt(x + ix, y + iy)) {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0) {
                        if (battlefield.CheckTankCollide(x, y)) {
                            SetErrorDescription("Found collision where there shouldn't be one");
                            return false;
                        }
                    } else {
                        if (!battlefield.CheckTankCollide(x, y)) {
                            SetErrorDescription("Didn't find collision where there should be one");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool TestTerrain0TankVerticalPosition() {
            Requires(TestTerrain0Terrain);
            Requires(TestTerrain0IsTileAt);

            Terrain battlefield = new Terrain();
            for (int x = 0; x <= Terrain.WIDTH - TankType.WIDTH; x++) {
                int lowestValid = 0;
                for (int y = 0; y <= Terrain.HEIGHT - TankType.HEIGHT; y++) {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankType.HEIGHT; iy++) {
                        for (int ix = 0; ix < TankType.WIDTH; ix++) {

                            if (battlefield.IsTileAt(x + ix, y + iy)) {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0) {
                        lowestValid = y;
                    }
                }

                int placedY = battlefield.TankVerticalPosition(x);
                if (placedY != lowestValid) {
                    SetErrorDescription(string.Format("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid));
                    return false;
                }
            }
            return true;
        }
        private static bool TestTerrain0DestroyGround() {
            Requires(TestTerrain0Terrain);
            Requires(TestTerrain0IsTileAt);

            Terrain battlefield = new Terrain();
            for (int y = 0; y < Terrain.HEIGHT; y++) {
                for (int x = 0; x < Terrain.WIDTH; x++) {
                    if (battlefield.IsTileAt(x, y)) {
                        battlefield.DestroyGround(x, y, 0.5f);
                        if (battlefield.IsTileAt(x, y)) {
                            SetErrorDescription("Attempted to destroy terrain but it still exists");
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            }
            SetErrorDescription("Did not find any terrain to destroy");
            return false;
        }
        private static bool TestTerrain0CalculateGravity() {
            Requires(TestTerrain0Terrain);
            Requires(TestTerrain0IsTileAt);
            Requires(TestTerrain0DestroyGround);

            Terrain battlefield = new Terrain();
            for (int x = 0; x < Terrain.WIDTH; x++) {
                if (battlefield.IsTileAt(x, Terrain.HEIGHT - 1)) {
                    if (battlefield.IsTileAt(x, Terrain.HEIGHT - 2)) {
                        // Seek up and find the first non-set tile
                        for (int y = Terrain.HEIGHT - 2; y >= 0; y--) {
                            if (!battlefield.IsTileAt(x, y)) {
                                // Do a gravity step and make sure it doesn't slip down
                                battlefield.CalculateGravity();
                                if (!battlefield.IsTileAt(x, y + 1)) {
                                    SetErrorDescription("Moved down terrain even though there was no room");
                                    return false;
                                }

                                // Destroy the bottom-most tile
                                battlefield.DestroyGround(x, Terrain.HEIGHT - 1, 0.5f);

                                // Do a gravity step and make sure it does slip down
                                battlefield.CalculateGravity();

                                if (battlefield.IsTileAt(x, y + 1)) {
                                    SetErrorDescription("Terrain didn't fall");
                                    return false;
                                }

                                // Otherwise this seems to have worked
                                return true;
                            }
                        }


                    }
                }
            }
            SetErrorDescription("Did not find any appropriate terrain to test");
            return false;
        }
        private static bool TestAttackEffect0ConnectGame() {
            Requires(TestBlast0Blast);
            Requires(TestBattle0Battle);

            AttackEffect weaponEffect = new Blast(1, 1, 1);
            Battle game = new Battle(2, 1);
            weaponEffect.ConnectGame(game);
            return true;
        }
        private static bool TestBullet0Bullet() {
            Requires(TestBlast0Blast);
            GenericPlayer player = CreateTestingPlayer();
            Blast explosion = new Blast(1, 1, 1);
            Bullet projectile = new Bullet(25, 25, 45, 30, 0.02f, explosion, player);
            return true;
        }
        private static bool TestBullet0ProcessTimeEvent() {
            Requires(TestBattle0NewGame);
            Requires(TestBlast0Blast);
            Requires(TestBullet0Bullet);
            Requires(TestAttackEffect0ConnectGame);
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Blast explosion = new Blast(1, 1, 1);

            Bullet projectile = new Bullet(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.ConnectGame(game);
            projectile.ProcessTimeEvent();

            // We can't really test this one without a substantial framework,
            // so we just call it and hope that everything works out

            return true;
        }
        private static bool TestBullet0Display() {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBlast0Blast);
            Requires(TestBullet0Bullet);
            Requires(TestAttackEffect0ConnectGame);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the projectile
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Blast explosion = new Blast(1, 1, 1);

            Bullet projectile = new Bullet(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.ConnectGame(game);
            projectile.Display(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++) {
                for (int x = 0; x < bitmapSize.Width; x++) {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0)) {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestBlast0Blast() {
            GenericPlayer player = CreateTestingPlayer();
            Blast explosion = new Blast(1, 1, 1);

            return true;
        }
        private static bool TestBlast0Activate() {
            Requires(TestBlast0Blast);
            Requires(TestAttackEffect0ConnectGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Blast explosion = new Blast(1, 1, 1);
            explosion.ConnectGame(game);
            explosion.Activate(25, 25);

            return true;
        }
        private static bool TestBlast0ProcessTimeEvent() {
            Requires(TestBlast0Blast);
            Requires(TestAttackEffect0ConnectGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestBlast0Activate);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Blast explosion = new Blast(1, 1, 1);
            explosion.ConnectGame(game);
            explosion.Activate(25, 25);
            explosion.ProcessTimeEvent();

            // Again, we can't really test this one without a full framework

            return true;
        }
        private static bool TestBlast0Display() {
            Requires(TestBlast0Blast);
            Requires(TestAttackEffect0ConnectGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestBlast0Activate);
            Requires(TestBlast0ProcessTimeEvent);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the explosion
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Blast explosion = new Blast(10, 10, 10);
            explosion.ConnectGame(game);
            explosion.Activate(25, 25);
            // Step it for a bit so we can be sure the explosion is visible
            for (int i = 0; i < 10; i++) {
                explosion.ProcessTimeEvent();
            }
            explosion.Display(graphics, bitmapSize);

            for (int y = 0; y < bitmapSize.Height; y++) {
                for (int x = 0; x < bitmapSize.Width; x++) {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0)) {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }

        private static GameplayForm InitialiseGameplayForm(out NumericUpDown angleCtrl, out TrackBar powerCtrl, out Button fireCtrl, out Panel controlPanel, out ListBox weaponSelect) {
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();

            angleCtrl = null;
            powerCtrl = null;
            fireCtrl = null;
            controlPanel = null;
            weaponSelect = null;

            game.NewGame();
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms) {
                if (f is GameplayForm) {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null) {
                SetErrorDescription("Battle.NewGame() did not create a GameplayForm and that is the only way GameplayForm can be tested");
                return null;
            }

            bool foundDisplayPanel = false;
            bool foundControlPanel = false;

            foreach (Control c in gameplayForm.Controls) {
                // The only controls should be 2 panels
                if (c is Panel) {
                    // Is this the control panel or the display panel?
                    Panel p = c as Panel;

                    // The display panel will have 0 controls.
                    // The control panel will have separate, of which only a few are mandatory
                    int controlsFound = 0;
                    bool foundFire = false;
                    bool foundAngle = false;
                    bool foundAngleLabel = false;
                    bool foundPower = false;
                    bool foundPowerLabel = false;


                    foreach (Control pc in p.Controls) {
                        controlsFound++;

                        // Mandatory controls for the control panel are:
                        // A 'Fire!' button
                        // A NumericUpDown for controlling the angle
                        // A TrackBar for controlling the power
                        // "Power:" and "Angle:" labels

                        if (pc is Label) {
                            Label lbl = pc as Label;
                            if (lbl.Text.ToLower().Contains("angle")) {
                                foundAngleLabel = true;
                            } else
                            if (lbl.Text.ToLower().Contains("power")) {
                                foundPowerLabel = true;
                            }
                        } else
                        if (pc is Button) {
                            Button btn = pc as Button;
                            if (btn.Text.ToLower().Contains("fire")) {
                                foundFire = true;
                                fireCtrl = btn;
                            }
                        } else
                        if (pc is TrackBar) {
                            foundPower = true;
                            powerCtrl = pc as TrackBar;
                        } else
                        if (pc is NumericUpDown) {
                            foundAngle = true;
                            angleCtrl = pc as NumericUpDown;
                        } else
                        if (pc is ListBox) {
                            weaponSelect = pc as ListBox;
                        }
                    }

                    if (controlsFound == 0) {
                        foundDisplayPanel = true;
                    } else {
                        if (!foundFire) {
                            SetErrorDescription("Control panel lacks a \"Fire!\" button OR the display panel incorrectly contains controls");
                            return null;
                        } else
                        if (!foundAngle) {
                            SetErrorDescription("Control panel lacks an angle NumericUpDown OR the display panel incorrectly contains controls");
                            return null;
                        } else
                        if (!foundPower) {
                            SetErrorDescription("Control panel lacks a power TrackBar OR the display panel incorrectly contains controls");
                            return null;
                        } else
                        if (!foundAngleLabel) {
                            SetErrorDescription("Control panel lacks an \"Angle:\" label OR the display panel incorrectly contains controls");
                            return null;
                        } else
                        if (!foundPowerLabel) {
                            SetErrorDescription("Control panel lacks a \"Power:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }

                        foundControlPanel = true;
                        controlPanel = p;
                    }

                } else {
                    SetErrorDescription(string.Format("Unexpected control ({0}) named \"{1}\" found in GameplayForm", c.GetType().FullName, c.Name));
                    return null;
                }
            }

            if (!foundDisplayPanel) {
                SetErrorDescription("No display panel found");
                return null;
            }
            if (!foundControlPanel) {
                SetErrorDescription("No control panel found");
                return null;
            }
            return gameplayForm;
        }

        private static bool TestGameplayForm0GameplayForm() {
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            return true;
        }
        private static bool TestGameplayForm0EnableTankControls() {
            Requires(TestGameplayForm0GameplayForm);
            Battle game = InitialiseGame();
            game.NewGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms) {
                if (f is GameplayForm) {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null) {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls) {
                if (c is Panel) {
                    foreach (Control cc in c.Controls) {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar) {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null) {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that EnableControlPanel enables it
            controlPanel.Enabled = false;

            gameplayForm.EnableTankControls();

            if (!controlPanel.Enabled) {
                SetErrorDescription("Control panel is still disabled after GameplayForm.EnableTankControls()");
                return false;
            }
            return true;

        }
        private static bool TestGameplayForm0Aim() {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            float testAngle = 27;

            gameplayForm.Aim(testAngle);
            if (FloatEquals((float)angle.Value, testAngle)) return true;

            else {
                SetErrorDescription(string.Format("Attempted to set angle to {0} but angle is {1}", testAngle, (float)angle.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SetPower() {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            int testPower = 71;

            gameplayForm.SetPower(testPower);
            if (power.Value == testPower) return true;

            else {
                SetErrorDescription(string.Format("Attempted to set power to {0} but power is {1}", testPower, power.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SetWeaponIndex() {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            gameplayForm.SetWeaponIndex(0);

            // WeaponSelect is optional behaviour, so it's okay if it's not implemented here, as long as the method works.
            return true;
        }
        private static bool TestGameplayForm0Launch() {
            Requires(TestGameplayForm0GameplayForm);
            // This is something we can't really test properly without a proper framework, so for now we'll just click
            // the button and make sure it disables the control panel
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            controlPanel.Enabled = true;
            fire.PerformClick();
            if (controlPanel.Enabled) {
                SetErrorDescription("Control panel still enabled immediately after clicking fire button");
                return false;
            }

            return true;
        }
        private static void UnitTests() {
            DoTest(TestBattle0Battle);
            DoTest(TestBattle0PlayerCount);
            DoTest(TestBattle0GetRounds);
            DoTest(TestBattle0SetPlayer);
            DoTest(TestBattle0GetPlayerNumber);
            DoTest(TestBattle0PlayerColour);
            DoTest(TestBattle0CalculatePlayerPositions);
            DoTest(TestBattle0Shuffle);
            DoTest(TestBattle0NewGame);
            DoTest(TestBattle0GetBattlefield);
            DoTest(TestBattle0CurrentPlayerTank);
            DoTest(TestTankType0CreateTank);
            DoTest(TestTankType0DisplayTank);
            DoTest(TestTankType0DrawLine);
            DoTest(TestTankType0GetTankArmour);
            DoTest(TestTankType0ListWeapons);
            DoTest(TestGenericPlayer0HumanOpponent);
            DoTest(TestGenericPlayer0CreateTank);
            DoTest(TestGenericPlayer0GetName);
            DoTest(TestGenericPlayer0PlayerColour);
            DoTest(TestGenericPlayer0AddPoint);
            DoTest(TestGenericPlayer0GetVictories);
            DoTest(TestHumanOpponent0BeginRound);
            DoTest(TestHumanOpponent0CommenceTurn);
            DoTest(TestHumanOpponent0ProjectileHitPos);
            DoTest(TestControlledTank0ControlledTank);
            DoTest(TestControlledTank0GetPlayerNumber);
            DoTest(TestControlledTank0CreateTank);
            DoTest(TestControlledTank0GetTankAngle);
            DoTest(TestControlledTank0Aim);
            DoTest(TestControlledTank0GetPower);
            DoTest(TestControlledTank0SetPower);
            DoTest(TestControlledTank0GetWeapon);
            DoTest(TestControlledTank0SetWeaponIndex);
            DoTest(TestControlledTank0Display);
            DoTest(TestControlledTank0XPos);
            DoTest(TestControlledTank0Y);
            DoTest(TestControlledTank0Launch);
            DoTest(TestControlledTank0DamageArmour);
            DoTest(TestControlledTank0IsAlive);
            DoTest(TestControlledTank0CalculateGravity);
            DoTest(TestTerrain0Terrain);
            DoTest(TestTerrain0IsTileAt);
            DoTest(TestTerrain0CheckTankCollide);
            DoTest(TestTerrain0TankVerticalPosition);
            DoTest(TestTerrain0DestroyGround);
            DoTest(TestTerrain0CalculateGravity);
            DoTest(TestAttackEffect0ConnectGame);
            DoTest(TestBullet0Bullet);
            DoTest(TestBullet0ProcessTimeEvent);
            DoTest(TestBullet0Display);
            DoTest(TestBlast0Blast);
            DoTest(TestBlast0Activate);
            DoTest(TestBlast0ProcessTimeEvent);
            DoTest(TestBlast0Display);
            DoTest(TestGameplayForm0GameplayForm);
            DoTest(TestGameplayForm0EnableTankControls);
            DoTest(TestGameplayForm0Aim);
            DoTest(TestGameplayForm0SetPower);
            DoTest(TestGameplayForm0SetWeaponIndex);
            DoTest(TestGameplayForm0Launch);
        }

        #endregion

        #region CheckClasses

        private static bool CheckClasses() {
            string[] classNames = new string[] { "Program", "AIOpponent", "Terrain", "Blast", "GameplayForm", "Battle", "HumanOpponent", "Bullet", "GenericPlayer", "ControlledTank", "TankType", "AttackEffect" };
            string[][] classFields = new string[][] {
                new string[] { "Main" }, // Program
                new string[] { }, // AIOpponent
                new string[] { "IsTileAt","CheckTankCollide","TankVerticalPosition","DestroyGround","CalculateGravity","WIDTH","HEIGHT"}, // Terrain
                new string[] { "Activate" }, // Blast
                new string[] { "EnableTankControls","Aim","SetPower","SetWeaponIndex","Launch","InitialiseBuffer"}, // GameplayForm
                new string[] { "PlayerCount","GetRound","GetRounds","SetPlayer","GetPlayerNumber","GetPlayerTank","PlayerColour","CalculatePlayerPositions","Shuffle","NewGame","CommenceRound","GetBattlefield","DisplayPlayerTanks","CurrentPlayerTank","AddEffect","WeaponEffectStep","DrawWeaponEffects","EndEffect","CheckCollidedTank","DamageArmour","CalculateGravity","TurnOver","CheckWinner","NextRound","Wind"}, // Battle
                new string[] { }, // HumanOpponent
                new string[] { }, // Bullet
                new string[] { "CreateTank","GetName","PlayerColour","AddPoint","GetVictories","BeginRound","CommenceTurn","ProjectileHitPos"}, // GenericPlayer
                new string[] { "GetPlayerNumber","CreateTank","GetTankAngle","Aim","GetPower","SetPower","GetWeapon","SetWeaponIndex","Display","XPos","Y","Launch","DamageArmour","IsAlive","CalculateGravity"}, // ControlledTank
                new string[] { "DisplayTank","DrawLine","CreateBMP","GetTankArmour","ListWeapons","ActivateWeapon","CreateTank","WIDTH","HEIGHT","NUM_TANKS"}, // TankType
                new string[] { "ConnectGame","ProcessTimeEvent","Display"} // AttackEffect
            };

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Checking classes for public methods...");
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsPublic) {
                    if (type.Namespace != "TankBattle") {
                        Console.WriteLine("Public type {0} is not in the TankBattle namespace.", type.FullName);
                        return false;
                    } else {
                        int typeIdx = -1;
                        for (int i = 0; i < classNames.Length; i++) {
                            if (type.Name == classNames[i]) {
                                typeIdx = i;
                                classNames[typeIdx] = null;
                                break;
                            }
                        }
                        foreach (MemberInfo memberInfo in type.GetMembers()) {
                            string memberName = memberInfo.Name;
                            bool isInherited = false;
                            foreach (MemberInfo parentMemberInfo in type.BaseType.GetMembers()) {
                                if (memberInfo.Name == parentMemberInfo.Name) {
                                    isInherited = true;
                                    break;
                                }
                            }
                            if (!isInherited) {
                                if (typeIdx != -1) {
                                    bool fieldFound = false;
                                    if (memberName[0] != '.') {
                                        foreach (string allowedFields in classFields[typeIdx]) {
                                            if (memberName == allowedFields) {
                                                fieldFound = true;
                                                break;
                                            }
                                        }
                                    } else {
                                        fieldFound = true;
                                    }
                                    if (!fieldFound) {
                                        Console.WriteLine("The public field \"{0}\" is not one of the authorised fields for the {1} class.\n", memberName, type.Name);
                                        Console.WriteLine("Remove it or change its access level.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    Console.WriteLine("{0} passed.", type.FullName);
                }
            }
            for (int i = 0; i < classNames.Length; i++) {
                if (classNames[i] != null) {
                    Console.WriteLine("The class \"{0}\" is missing.", classNames[i]);
                    return false;
                }
            }
            Console.WriteLine("All public methods okay.");
            return true;
        }

        #endregion

        public static void Main() {
            Terrain map = new Terrain();
            TitleForm newFormlel = new TitleForm();

            newFormlel.Enabled = true;
            newFormlel.Show();


            if (1 == 2) {
                bool foundTrue = false;
                bool foundFalse = false;
                Terrain battlefield = new Terrain();
                for (int y = 0; y < Terrain.HEIGHT; y++) {
                    for (int x = 0; x < Terrain.WIDTH; x++) {
                        if (battlefield.IsTileAt(x, y)) {
                            foundTrue = true;
                        } else {
                            foundFalse = true;
                        }
                    }
                }

                if (!foundTrue) {
                    Console.WriteLine("We failed! IsTileAt didn't return true for anything");
                }

                if (!foundFalse) {
                    Console.WriteLine("We failed! IsTileAt didn't return false for anything");
                }
                Console.WriteLine("We passed!");
            }

            if (10 == 11)
            {
                TankType tank2 = new SmallTank();
                GenericPlayer P = new HumanOpponent("tristan", tank2, Color.AliceBlue);
                GenericPlayer P2 = new HumanOpponent("tristan2", tank2, Color.Chocolate);
                Battle game2 = new Battle(2, 5);
                game2.SetPlayer(1, P);
                game2.SetPlayer(2, P2);
                ControlledTank tristanTank = new ControlledTank(P, 32, 32, game2);
                ControlledTank tristankTank2 = new ControlledTank(P2, 80, 32, game2);
                game2.CommenceRound();
                //GameplayForm gPlayForm = new GameplayForm(game2); // something in stuck in a loop somewhere perhaps, the screens just freeze.
                float angle = 50;
                tristanTank.Aim(angle);
                tristankTank2.Aim(angle);
                if (FloatEquals(tristanTank.GetTankAngle(), angle)) {
                    Console.WriteLine("TRUE");
                } else {
                    Console.WriteLine("FALSE");
                }
                Console.WriteLine("current player tank Vertical is {0}", game2.CurrentPlayerTank().Y());
                Console.WriteLine("current player tank horizopntal is {0}", game2.CurrentPlayerTank().XPos());

            }

            map.DestroyGround(20, Terrain.HEIGHT - 30, 10.5f);
            if (2 == 2) { //map output
            for (int down = 0; down < Terrain.HEIGHT; down++) {
                for (int across = 0; across < Terrain.WIDTH; across++) {
                    if (across == Terrain.WIDTH-1) {
                        if (map.IsTileAt(across, down))
                        {
                            Console.WriteLine("#: {0}", down);
                        }
                        else
                        {
                            Console.WriteLine(".: {0}", down);
                        }
                    } else {
                        if (map.IsTileAt(across, down)) {
                            Console.Write("#");
                        } else {
                            Console.Write(".");
                        }
                    }

                }
            }
        }

            
            map.CalculateGravity();

            for (int down = 0; down < Terrain.HEIGHT; down++) {
                for (int across = 0; across < Terrain.WIDTH; across++) {
                    if (across == Terrain.WIDTH - 1) {
                        if (map.IsTileAt(across, down)) {
                            Console.WriteLine("#: {0}", down);
                        } else {
                            Console.WriteLine(".: {0}", down);
                        }
                    } else {
                        if (map.IsTileAt(across, down)) {
                            Console.Write("#");
                        } else {
                            Console.Write(".");
                        }
                    }

                }
            }

            if (4 == 3) //check collision
            {
                Terrain battlefield = new Terrain();
                for (int y = 0; y <= Terrain.WIDTH - TankType.HEIGHT; y++) {
                    for (int x = 0; x <= Terrain.HEIGHT - TankType.WIDTH; x++) {
                        int colTiles = 0;
                        for (int iy = 0; iy < TankType.HEIGHT; iy++) {
                            for (int ix = 0; ix < TankType.WIDTH; ix++) {

                                if (battlefield.IsTileAt(x + ix, y + iy)) {
                                    colTiles++;
                                }
                            }
                        }
                        if (colTiles == 0) {
                            if (battlefield.CheckTankCollide(x, y)) {
                                Console.WriteLine("Collided! shouldn't have for some reason");
                                Console.WriteLine("We checked {0} down and {1} across which was {2}",x,y,battlefield.IsTileAt(x,y));
                                Console.WriteLine();
                            }
                        } else {
                            if (!battlefield.CheckTankCollide(x, y)) {
                                Console.WriteLine("DIdn't collide! should have though?");
                                Console.WriteLine("We checked {0} down and {1} across which was {2}", x, y, battlefield.IsTileAt(x, y));
                                Console.WriteLine();

                            }
                        }
                    }
                }
            }

            if (5 == 6) //tank vertical position
            {
                Terrain battlefield = new Terrain();

                for (int x = 0; x <= Terrain.WIDTH - TankType.WIDTH; x++) {
                    int lowestValid = 0;

                    for (int y = 0; y <= Terrain.HEIGHT - TankType.HEIGHT; y++) {
                        int colTiles = 0;

                        for (int iy = 0; iy < TankType.HEIGHT; iy++) {
                            for (int ix = 0; ix < TankType.WIDTH; ix++) {
                                if (battlefield.IsTileAt(x + ix, y + iy)) {
                                    colTiles++;
                                }
                            }
                        }
                        if (colTiles == 0) {
                            lowestValid = y;
                        }
                    }

                    int placedY = battlefield.TankVerticalPosition(x);
                    if (placedY != lowestValid) {
                        Console.WriteLine("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid);
                    } else { Console.WriteLine("Looking good?");}
                }
            }

            if (7 == 8) {
                Battle game = new Battle(2, 1);
                TankType tank = TankType.CreateTank(1);
                GenericPlayer player1 = new HumanOpponent("player1", tank, Color.Orange);
                GenericPlayer player2 = new HumanOpponent("player2", tank, Color.Purple);

                game.SetPlayer(1, player1);
                game.SetPlayer(2, player2);
                Console.WriteLine("This should be the currentplayer tank?: {0}", game.GetPlayerTank(0));
            }

            if (9 == 10) //IsTileAt
            {
                bool foundTrue = false;
                bool foundFalse = false;

                for (int y = 0; y < Terrain.HEIGHT-1; y++) {
                    for (int x = 0; x < Terrain.WIDTH-1; x++) {
                        if (map.IsTileAt(x, y)) {
                            foundTrue = true;
                        } else {
                            foundFalse = true;
                        }
                    }
                }

                if (!foundTrue) {
                    Console.WriteLine("IsTileAt() did not return true for any tile.");
                }

                if (!foundFalse) {
                    Console.WriteLine("IsTileAt() did not return false for any tile.");
                }

                if (foundTrue && foundFalse)
                {
                    Console.WriteLine("Congrats, we found both A OK!");
                }
            }

            if (11 == 12) //place tank or some shit
            {
                int[] positions = Battle.CalculatePlayerPositions(8);
                for (int i = 0; i < 8; i++) {
                    if (positions[i] < 0) Console.WriteLine("Your position is off the screen, under 0!");
                    if (positions[i] > 160) Console.WriteLine("Your position is off the screen, past 160!!");
                    Console.WriteLine("Position no {0} is position {1}",i,positions[i]);
                    for (int j = 0; j < i; j++) {
                        if (positions[j] == positions[i]) Console.WriteLine("You've got 2 people ontop of each other!! possy {0} and {1}",positions[j],positions[i]);
                    }
                }
                Console.WriteLine("All good");
            }

            if (12 == 13)
            {
                float angle = 75;
                GenericPlayer p = CreateTestingPlayer();
                Battle game = InitialiseGame();
                ControlledTank playerTank = new ControlledTank(p, 32, 32, game);
                playerTank.Aim(angle);
                if (FloatEquals(playerTank.GetTankAngle(), angle))
                {
                    Console.WriteLine("TRUE");
                }
                else
                {
                    Console.WriteLine("FALSE");
                }
                
            }
            if (CheckClasses())
            {
                UnitTests();

                int passed = 0;
                int failed = 0;
                foreach (string key in unitTestResults.Keys)
                {
                    if (unitTestResults[key] == "Passed")
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                }

                Console.WriteLine("\n{0}/{1} unit tests passed", passed, passed + failed);
                if (failed == 0)
                {
                    Console.WriteLine("Starting up TankBattle...");
                    Program.Main();
                    return;
                }
            }
          

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();




            ////now do current player tank
        }

    }
}
