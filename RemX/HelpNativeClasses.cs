using System;
using System.Drawing;
using System.Runtime.InteropServices;    
using System.ComponentModel;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}

// Delegate that is called for each child window of the ListView. 
public delegate bool EnumWinCallBack(IntPtr hwnd, IntPtr lParam);

public static class Interaction
{
    // Callers require Unmanaged permission
    public static void Beep(int frec, int duration)
    {
        // No need to demand a permission as callers of Interaction.Beep            
        // will require UnmanagedCode permission           
        if (!NativeMethods.Beep(frec, duration))                
            throw new Win32Exception();
    }

    //public static void WindowsLogOff()
    //{
    //    if (!NativeMethods.WindowsLogOff())
    //        throw new Win32Exception();
    //}

    public static void SetForegroundWindow(IntPtr hWnd)
    {
        if (!NativeMethods.SetForegroundWindow(hWnd))
            throw new Win32Exception();
    }

    public static IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam)
    {
        try
        {
            return NativeMethods.SendMessage(hWnd, Msg, new IntPtr(wParam), new IntPtr(lParam));
        }
        catch
        {
            throw new Win32Exception();
        }
    }

    // The area occupied by the ListView header. 
    public static Rectangle _headerRect;

    // This should get called with the only child window of the ListView,
    // which should be the header bar.
    public static bool EnumWindowCallBack(IntPtr hwnd, IntPtr lParam)
    {
        // Determine the rectangle of the ListView header bar and save it in _headerRect.
        RECT rct;
        if (!NativeMethods.GetWindowRect(hwnd, out rct))
        {
            _headerRect = Rectangle.Empty;
        }
        else
        {
            _headerRect = new Rectangle(
            rct.Left, rct.Top, rct.Right - rct.Left, rct.Bottom - rct.Top);
        }
        return false; // Stop the enum
    }

    public static int EnumChildWindows(IntPtr hWndParent, EnumWinCallBack callBackFunc, IntPtr lParam)
    {
        return NativeMethods.EnumChildWindows(hWndParent, callBackFunc, lParam);
    }

}

internal static class NativeMethods    
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]        
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool Beep(int frec, int duration);

    //[DllImport("ShellLogoff.dll", SetLastError = true)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //internal static extern bool WindowsLogOff();

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern Boolean SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    // Calls EnumWinCallBack for each child window of hWndParent (i.e. the ListView).
    [DllImport("user32.dll")]
    internal static extern int EnumChildWindows(
        IntPtr hWndParent,
        EnumWinCallBack callBackFunc,
        IntPtr lParam);

    // Gets the bounding rectangle of the specified window (ListView header bar). 
    [DllImport("user32.dll")]
    internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

}


                