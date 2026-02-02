using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace ShowWinCursorLocation
{
    /// <summary>
    /// Creates a simple window that follows the mouse cursor.
    /// 
    /// The target audience for this program are content creators that want to record
    /// video game play while hiding the game cursor so that the content shows better in
    /// trailers and shorts. This application enables the player to see where the mouse
    /// cursor is despite the cursor not rendering in the game.
    /// 
    /// To use this program effectively, the video recording application must be recording
    /// the game window, not the display window.
    /// </summary>
    internal class Program
    {
        // The window that follows the mouse cursor
        static TransparentForm myForm;
        // The timer that manages when the window updates
        static System.Timers.Timer myTimer;

        [STAThread]
        static void Main(string[] args)
        {
            myForm = new TransparentForm
            {
                Text = "My Program Window",
                ClientSize = new System.Drawing.Size(8, 8),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true
            };

            myTimer = new System.Timers.Timer(100);
            myTimer.SynchronizingObject = myForm;
            myTimer.Elapsed += OnTimedEvent;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;

            Application.Run(myForm);

            myTimer.Dispose();
            myForm.Dispose();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Point CursorPos = Cursor.Position;

            // Move the window to just past the mouse cursor. Despite being
            // transparent, it may still pick up clicks or steal focus sometimes
            myForm.SetDesktopLocation(CursorPos.X + 3, CursorPos.Y + 3);
        }
    }
}
