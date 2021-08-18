using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using WindowsInput.Native;



namespace BinderWPF
{
    /// <summary>
    /// Interaction logic for AddKeys.xaml
    /// </summary>
    public partial class AddKeys : Window
    {
        private List<Bind> binds;

        private Keys input = new Keys();

        private List<Keys> output = new List<Keys>();

        private MainWindow Window = new MainWindow();

        public AddKeys(List<Bind> bind, MainWindow mainWindow)
        {
            Window = mainWindow;

            binds = bind;

            InitializeComponent();

            comboBox.ItemsSource = (Keys[])Enum.GetValues(typeof(Keys));

            comboBox2.ItemsSource = (Keys[])Enum.GetValues(typeof(Keys));

            textBox3.IsReadOnly = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox.Text != "" && textBox3.Text != "")
            {
                input = (Keys)comboBox.SelectedItem;
                int delay;
                try
                {
                    delay = int.Parse(textBox.Text) * 1000;
                    binds.Add(new Bind(input, output, delay)); 
                }
                catch
                {
                    delay = 0;
                    System.Windows.MessageBox.Show("Delay value error. Set to default(0)");
                    binds.Add(new Bind(input, output, delay));
                }

                Window.saveBinds.Save(binds);

                Window.listBox.Items.Add($"Input: {comboBox.Text}. Output: {textBox3.Text}. Delay: {delay}");

                Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Not all keys selected");
            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            if (comboBox2.Text != "")
            {
                output.Add((Keys)comboBox2.SelectedItem);
            }

            if (textBox3.Text != "")
            {
                textBox3.Text += $" + {comboBox2.SelectedItem}";
            }
            else
            {
                textBox3.Text += comboBox2.SelectedItem;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            textBox3.Clear();

            output.Clear();
        }
    }
}
