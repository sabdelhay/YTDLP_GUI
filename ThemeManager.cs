using System.Drawing;
using System.Windows.Forms;

namespace YTDLP_GUI
{
    public static class ThemeManager
    {
        public static void ApplyDark(Form form)
        {
            Apply(form, Color.FromArgb(30, 30, 30), Color.White);
        }

        public static void ApplyLight(Form form)
        {
            Apply(form, SystemColors.Control, Color.Black);
        }

        private static void Apply(Form form, Color back, Color text)
        {
            form.BackColor = back;

            foreach (Control c in form.Controls)
            {
                bool input = c is TextBox || c is ListBox;
                c.BackColor = input
                    ? (back == Color.FromArgb(30, 30, 30) ? Color.FromArgb(45, 45, 45) : Color.White)
                    : back;
                c.ForeColor = text;
            }
        }
    }
}
