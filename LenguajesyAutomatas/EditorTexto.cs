using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public partial class EditorTexto : UserControl
    {
        int caracter;
        public EditorTexto()
        {
            InitializeComponent();
        }

        public void Guardar()
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.txt";
            saveFile1.Filter = "Text Files|*.txt";
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                rtxTexto.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        public  void Abir()
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "Text Files|*.txt";

            if (openFile1.ShowDialog() == DialogResult.OK)
            {
                rtxTexto.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        public string Texto()
        {
            return rtxTexto.Text;
        }

            private void EditorTexto_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            picNumeracion.Refresh();
        }

        private void picNumeracion_Paint(object sender, PaintEventArgs e)
        {
            caracter = 0;
            int altura = rtxTexto.GetPositionFromCharIndex(0).Y;
            if (rtxTexto.Lines.Length > 0)
            {
                for (int i = 0; i <= rtxTexto.Lines.Length - 1; i++)
                {
                    e.Graphics.DrawString(Convert.ToString(i + 1), rtxTexto.Font, Brushes.Red, picNumeracion.Width - (e.Graphics.MeasureString(Convert.ToString(i + 1), rtxTexto.Font).Width + 10), altura);
                    caracter += rtxTexto.Lines[i].Length + 1;
                    altura = rtxTexto.GetPositionFromCharIndex(caracter).Y;
                }
            }
            else
            {
                e.Graphics.DrawString(Convert.ToString(1), rtxTexto.Font, Brushes.Red, picNumeracion.Width - (e.Graphics.MeasureString(Convert.ToString(1), rtxTexto.Font).Width + 10), altura);
            }
        }
    }
}
