using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace 仙五前难度修改器
{
    unsafe class Memory
    {
        private static int hProcess;


        #region API
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory
           (
            int hProcess, 
            int lpBaseAddress, 
            void* lpBuffer, 
            int nSize, 
            int *lpNumberOfBytesRead
           );
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory
           (
            int hProcess,
            int lpBaseAddress,
            char[] lpBuffer,
            int nSize,
            int* lpNumberOfBytesRead
           );
        [DllImportAttribute("kernel32.dll")]
        public static extern bool WriteProcessMemory
            (
            int hProcess,
            int lpBaseAddress,
            void *lpBuffer,
            int nSize,
            int* lpNumberOfBytesWritten
            );
        [DllImportAttribute("kernel32.dll")]
        public static extern bool WriteProcessMemory
            (
            int hProcess,
            int lpBaseAddress,
            char[] lpBuffer,
            int nSize,
            int* lpNumberOfBytesWritten
            );
        [DllImportAttribute("kernel32.dll")]
        public static extern int OpenProcess
            (
            int dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId
            );
        #endregion

        public static bool GetProcessByName(string processName)
        {
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            foreach (Process p in arrayProcess)
            {
                hProcess = OpenProcess(0x1F0FFF, false, p.Id);
                return true;
            }
            return false;

        }

        public static int ReadInt32(int lpBaseAddress)
        {
            int num;
            int temp;
            ReadProcessMemory(hProcess, lpBaseAddress, &num, sizeof(int), &temp);
            return num;
        }

        public static float ReadSingle(int lpBaseAddress)
        {
            float num;
            int temp;
            ReadProcessMemory(hProcess, lpBaseAddress, &num, sizeof(float), &temp);
            return num;
        }

        public static double ReadDouble(int lpBaseAddress)
        {
            double num;
            int temp;
            ReadProcessMemory(hProcess, lpBaseAddress, &num, sizeof(double), &temp);
            return num;
        }

        public static string ReadString(int lpBaseAddress, int bytenum)
        {
            char[] s = new char[bytenum];
            int temp;
            ReadProcessMemory(hProcess, lpBaseAddress, s, bytenum, &temp);
            return new string(s);
        }

        public static void WriteMemory(int lpBaseAddress, int num)
        {
            int temp;
            WriteProcessMemory(hProcess, lpBaseAddress, &num, sizeof(int), &temp);
        }

        public static void WriteMemory(int lpBaseAddress, float num)
        {
            int temp;
            WriteProcessMemory(hProcess, lpBaseAddress, &num, sizeof(float), &temp);
        }
        public static void WriteMemory(int lpBaseAddress, double num)
        {
            int temp;
            WriteProcessMemory(hProcess, lpBaseAddress, &num, sizeof(double), &temp);
        }
        public static void WriteMemory(int lpBaseAddress, string s)
        {
            int temp;
            char[] cs = s.ToArray();
            WriteProcessMemory(hProcess, lpBaseAddress,cs, sizeof(int), &temp);
        }
    }
}
