using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_go_wallhack {
    public class Cheat {

        private static MemoryHacking memoryHacking;
        public static bool running = false;

        public static int clientAddress;
        public static int engineAddress;
        public static int localPlayerBase;
        public static int entityList;
        public static int glowPointer;

        public static Int32 m_iTeamNum = 0xF4;
        public static Int32 dwEntityList = 0x4CDB07C;
        public static Int32 dwLocalPlayer = 0xCCA6B4;
        public static Int32 dwGlowObjectManager = 0x521AFA8;
        public static Int32 m_iGlowIndex = 0xA3F8;
        public static Int32 m_iHealth = 0x100;

        private static Color enemyColor = new Color(1f, 0f, 0f);
        private static Color teamColor = new Color(0f, 1f, 0f);

        [STAThread]
        static void Main() {
            memoryHacking = new MemoryHacking("csgo");
            Thread update = new Thread(new ThreadStart(Update)) {
                IsBackground = true
            };
            update.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }

        private static void Update() {
            while (true) {
                if (running) {
                    localPlayerBase = memoryHacking.ReadInt(clientAddress + dwLocalPlayer);
                    glowPointer = memoryHacking.ReadInt(clientAddress + dwGlowObjectManager);
                    UpdateWallhack();
                }
                Thread.Sleep(1);
            }
        }

        private static void UpdateWallhack() {
            for (int i = 0; i != 32; i++) {
                int currentPlayer = memoryHacking.ReadInt(entityList + i * 0x10);
                int currentHealth = memoryHacking.ReadInt(currentPlayer + m_iHealth);
                if (currentHealth > 0 && currentPlayer != 0) {
                    int currentPlayerGlowIndex = memoryHacking.ReadInt(currentPlayer + m_iGlowIndex);
                    if (currentPlayerGlowIndex != 0) {
                        int ownTeam = memoryHacking.ReadInt(localPlayerBase + m_iTeamNum);
                        int currentTeam = memoryHacking.ReadInt(currentPlayer + m_iTeamNum);
                        if (currentTeam != ownTeam) {
                            Glow(currentPlayerGlowIndex, enemyColor);
                        } else {
                            Glow(currentPlayerGlowIndex, teamColor);
                        }
                    }
                }

            }
        }

        private static void Glow(int currentPlayerGlowIndex, Color color) {
            memoryHacking.WriteFloat(glowPointer + currentPlayerGlowIndex * 0x38 + 0x4, color.red);
            memoryHacking.WriteFloat(glowPointer + currentPlayerGlowIndex * 0x38 + 0x8, color.green);
            memoryHacking.WriteFloat(glowPointer + currentPlayerGlowIndex * 0x38 + 0xc, color.blue);
            memoryHacking.WriteFloat(glowPointer + currentPlayerGlowIndex * 0x38 + 0x10, 1);
            memoryHacking.WriteInt(glowPointer + currentPlayerGlowIndex * 0x38 + 0x24, 1);
            memoryHacking.WriteInt(glowPointer + currentPlayerGlowIndex * 0x38 + 0x25, 0);
            memoryHacking.WriteInt(glowPointer + currentPlayerGlowIndex * 0x38 + 0x26, 0);
        }

        public struct Color {
            public float red, green, blue;

            public Color(float red, float green, float blue) {
                this.red = red;
                this.green = green;
                this.blue = blue;
            }
        }
    }
}
