using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace BlocDeNotas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Metodos.setpath(string.Empty);
        }
        private void wordw_Checked(object sender, RoutedEventArgs e)
        {
            mainbox.TextWrapping = TextWrapping.Wrap;
        }

        private void wordw_Unchecked(object sender, RoutedEventArgs e)
        {
            mainbox.TextWrapping = TextWrapping.NoWrap;
        }
        #region archivo
        private void AbrirArch_Click(object sender, RoutedEventArgs e)
        {
            Metodos.FileExists(mainbox, GuardarComo);
            Microsoft.Win32.OpenFileDialog openarch = new Microsoft.Win32.OpenFileDialog();
            openarch.Filter = "Text file (*.txt)|*.txt|c# file (*.cs)|*cs|Html file (*.html, *.htm)|*.html|css file (*.css)|*.css|scss file (*.scss)|*scss|JavaScript file (*js)|*.js|All files (*.*)|*.*";
            openarch.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openarch.ShowDialog() == true)
            {
                mainbox.Text = File.ReadAllText(openarch.FileName);
            }
            Metodos.setpath(openarch.FileName);
            
        }

        private void GuardarComo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog savearch = new Microsoft.Win32.SaveFileDialog();
            savearch.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            savearch.DefaultExt = "txt";
            savearch.Title = "Guardar como";
            savearch.Filter = "Text file (*.txt)|*.txt|c# file (*.cs)|*cs|Html file (*.html, *.htm)|*.htm|css file (*.css)|*.css|scss file (*.scss)|*scss|JavaScript file (*js)|*.js|All files (*.*)|*.*";
            savearch.OverwritePrompt = true;
            if (savearch.ShowDialog() == true)
            {
                File.WriteAllText(savearch.FileName, mainbox.Text);
            }
            Metodos.setpath(savearch.FileName.ToString());
        }

        private void nuevoarch_Click(object sender, RoutedEventArgs e)
        {
            Metodos.FileExists(mainbox, GuardarComo);
            mainbox.Clear();
        }

        private void imprimirpage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog print = new System.Windows.Controls.PrintDialog();
            print.PageRangeSelection = PageRangeSelection.AllPages;
            print.UserPageRangeEnabled = true;
            if (print.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument();
                foreach (string line in mainbox.Text.Split('\n'))
                {
                    Paragraph mypaph = new Paragraph();
                    mypaph.Margin = new Thickness(0);
                    mypaph.Inlines.Add(new Run(line));
                    document.Blocks.Add(mypaph);

                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
                print.PrintDocument(paginator, "Printer");
            }
            
        }

        private void exitapp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void guardarbtn_Click(object sender, RoutedEventArgs e)
        {
            string path2 = Metodos.getpath();
            if ((path2 == string.Empty) || (path2 == null))
            {
                GuardarComo.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.MenuItem.ClickEvent));
            }
            else
            {
                File.WriteAllText(path2, mainbox.Text);
            }
        }
        #endregion


        #region editar
        private void copiarbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.Clear();
            System.Windows.Clipboard.SetText(mainbox.SelectedText);
        }

        private void cortarbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.Clear();
            System.Windows.Clipboard.SetText(mainbox.SelectedText);
            mainbox.SelectedText = "";
        }

        private void pegarbtn_Click(object sender, RoutedEventArgs e)
        {
            string paste = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.Text);
            mainbox.Text = mainbox.Text.Insert(mainbox.SelectionStart, paste);

        }

        private void eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            mainbox.SelectedText = "";
        }

        private void selectallbtn_Click(object sender, RoutedEventArgs e)
        {
            mainbox.SelectAll();
        }

        private void date_time_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
            mainbox.Text = mainbox.Text.Insert(mainbox.SelectionStart, date);
        }
        #endregion
        private void fuentebtn_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fntbox = new FontDialog();
            fntbox.ShowColor = true;
            fntbox.ShowApply = true;
            if (fntbox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mainbox.FontFamily = new System.Windows.Media.FontFamily(fntbox.Font.FontFamily.Name.ToString());
                mainbox.Foreground = Metodos.Rcolor(fntbox.Color);
            }
        }

        private void color_fbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorbox = new System.Windows.Forms.ColorDialog();
            colorbox.AllowFullOpen = true;
            if (colorbox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                mainbox.Background = Metodos.Rcolor(colorbox.Color);
          
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Metodos.FileExists(mainbox, GuardarComo);
            e.Cancel = false;
            base.OnClosing(e);
        }

        private void Abourbtn_Click(object sender, RoutedEventArgs e)
        {
            Form1 w = new Form1();
            w.ShowDialog();
        }

        

      
    }
}
