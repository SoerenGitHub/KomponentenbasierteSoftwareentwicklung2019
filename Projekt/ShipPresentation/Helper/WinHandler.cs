using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.Helper
{
    internal static class WinHandler
    {

        //** DISABLE MINIMIZE&MAXIMIZE BUTTON *** 
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static void DisableMinimizeAndMaximizeButton(IntPtr windowHandle)
        {
            const int GWL_STYLE = -16;
            const int WS_MAXIMIZEBOX = 0x10000; //maximize button
            const int WS_MINIMIZEBOX = 0x20000; //minimize button

            if (windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            // Disable minimize button
            // SetWindowLong( windowHandle, GWL_STYLE, GetWindowLong( windowHandle, GWL_STYLE) & ~WS_MINIMIZEBOX);
            // Disable maximize button
            // SetWindowLong( windowHandle, GWL_STYLE, GetWindowLong( windowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
            // Disable minimize&Maximize button and hide them 
            SetWindowLong(windowHandle, GWL_STYLE,
            GetWindowLong(windowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX);
        }


        //** DISABLE CLOSE BUTTON *** 
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [DllImport("user32.dll")]
        private static extern IntPtr DestroyMenu(IntPtr hWnd);


        public static void DisableCloseButton(IntPtr windowHandle)
        {
            const uint MF_BYCOMMAND = 0x00000000;
            const uint MF_GRAYED = 0x00000001;
            const uint SC_CLOSE = 0xF060;

            if (windowHandle == null)
                throw new InvalidOperationException("The window has not yet been completely initialized");
            IntPtr menuHandle;
            menuHandle = GetSystemMenu(windowHandle, false);
            if (menuHandle != IntPtr.Zero)
            {
                EnableMenuItem(menuHandle, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
            }
        }

    }
}
