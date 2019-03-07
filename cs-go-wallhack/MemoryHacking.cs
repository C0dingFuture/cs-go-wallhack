using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_go_wallhack {
    public class MemoryHacking {
        private Process process;
        private readonly IntPtr hProc;

        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("Kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAdress, byte[] lpBufer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("Kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAdress, byte[] lpBufer, int nSize, out int lpNumberOfBytesRead);

        public MemoryHacking(string processName) {
            try {
                process = Process.GetProcessesByName(processName)[0];
                hProc = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
                Cheat.clientAddress = GetModuleBase("client_panorama.dll");
                Cheat.engineAddress = GetModuleBase("engine.dll");
                Cheat.entityList = Cheat.clientAddress + Cheat.dwEntityList;
            } catch (Exception) {
                MessageBox.Show("CS:GO process not found!", "Error", MessageBoxButtons.OK);
                Environment.Exit(1);
            }
        }

        public int GetModuleBase(string moduleName) {
            ProcessModuleCollection modules = process.Modules;
            ProcessModule dllBaseAdress;
            foreach (ProcessModule processModule in modules) {
                if (processModule.ModuleName == moduleName) {
                    dllBaseAdress = processModule;
                    return (int)dllBaseAdress.BaseAddress;
                }
            }
            return 0;
        }

        public int ReadInt(int adress) {
            byte[] tempBuffer = new byte[sizeof(int)];
            ReadProcessMemory(hProc, (uint)adress, tempBuffer, tempBuffer.Length, out int read);
            return BitConverter.ToInt32(tempBuffer, 0);
        }

        public short ReadShort(int adress) {
            byte[] tempBuffer = new byte[sizeof(short)];
            ReadProcessMemory(hProc, (uint)adress, tempBuffer, tempBuffer.Length, out int read);
            return BitConverter.ToInt16(tempBuffer, 0);
        }

        public string ReadString(int address, int bufferSize, Encoding enc) {
            byte[] buffer = new byte[bufferSize];
            bool success = ReadProcessMemory(hProc, (uint)address, buffer, bufferSize, out int read);
            string text = enc.GetString(buffer);
            if (text.Contains('\0'))
                text = text.Substring(0, text.IndexOf('\0'));
            return text;
        }

        public float ReadFloat(int adress) {
            byte[] tempBuffer = new byte[sizeof(float)];
            ReadProcessMemory(hProc, (uint)adress, tempBuffer, tempBuffer.Length, out int read);
            return BitConverter.ToSingle(tempBuffer, 0);
        }

        public void WriteInt(int adress, int value) {
            byte[] data = BitConverter.GetBytes(value);
            WriteProcessMemory(hProc, (uint)adress, data, data.Length, out int read);
        }

        public void WriteByte(int adress, byte value) {
            byte[] data = BitConverter.GetBytes(value);
            WriteProcessMemory(hProc, (uint)adress, data, data.Length, out int read);
        }

        public void WriteFloat(int adress, float value) {
            byte[] data = BitConverter.GetBytes(value);
            WriteProcessMemory(hProc, (uint)adress, data, data.Length, out int read);
        }
    }
}
