using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xaml;
using System.Windows.Controls;
using System.Drawing;

namespace BlocDeNotas
{
    class Metodos
    {
        private static string path1;
        public static void setpath(string _path)
        {
            path1 = _path;
        }
        public static string getpath()
        {
            return path1;
        }
        public static System.Windows.Media.SolidColorBrush Rcolor(System.Drawing.Color color)
        {
            System.Windows.Media.Color colorb = new System.Windows.Media.Color();
            colorb.R = color.R;
            colorb.A = color.A;
            colorb.G = color.G;
            colorb.B = color.B;
            System.Windows.Media.SolidColorBrush solid = new System.Windows.Media.SolidColorBrush(colorb);
            return solid;
        }
        public static void FileExists(TextBox mainbox, MenuItem GuardarComo)
        {
            string path2 = Metodos.getpath();
            if (mainbox.Text != string.Empty)
            {
                if (File.Exists(path2))
                {
                    string oldtext = File.ReadAllText(path2);
                    string newtext = mainbox.Text;
                    if (oldtext != newtext)
                    {
                        if (System.Windows.Forms.MessageBox.Show("El archivo ha recivido cambios. ¿Desea guardarlo?", "Notepad 2", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            File.WriteAllText(path2, mainbox.Text);
                        }

                    }

                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show("El archivo no ha sido guardado. ¿Desea guardarlo?", "Notepad 2", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        GuardarComo.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.MenuItem.ClickEvent));
                    }

                }
            }
            Metodos.setpath(null);
        }

    }
}
    