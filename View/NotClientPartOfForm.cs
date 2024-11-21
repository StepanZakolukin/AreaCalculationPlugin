using System.Runtime.InteropServices;

namespace AreaCalculationPlugin.View;

internal static class NotClientPartOfForm
{
    private static string ToBgr(Color c) => $"{c.B:X2}{c.G:X2}{c.R:X2}";

    [DllImport("DwmApi")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

    const int DWWMA_CAPTION_COLOR = 35;
    public static void CustomWindow(Color captionColor, IntPtr handle)
    {
        IntPtr hWnd = handle;
        //Change caption color
        int[] caption = [int.Parse(ToBgr(captionColor), System.Globalization.NumberStyles.HexNumber)];
        DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, caption, 4);
    }
}
