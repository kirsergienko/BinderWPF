using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using InputManager;

namespace BinderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bind> binds = new List<Bind>();

        public SaveBinds saveBinds = new SaveBinds();

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                binds = saveBinds.GetBinds();

                foreach (var bind in binds)
                {
                    string outputKeys = "";

                    foreach (var key in bind.OutputKeys)
                    {
                        outputKeys += outputKeys == "" ? key.ToString() : $"+ {key}";
                    }

                    listBox.Items.Add($"Input: {bind.InputKey}. Output: {outputKeys}. Delay: {bind.Delay}");
                }
            }
            catch
            {

            }

        }

        private void addButton_Click_1(object sender, RoutedEventArgs e)
        {
            AddKeys addKeys = new AddKeys(binds, this);

            addKeys.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (binds.Count > 0)
            {
                int i = listBox.SelectedIndex;

                listBox.Items.Remove(listBox.SelectedItem);

                binds.Remove(binds[i]);

                saveBinds.Save(binds);
            }

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;

            KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler(PressKeys);

            KeyboardHook.InstallHook();
        }

        private void PressKeys(int x)
        {
            KeyboardHook.UninstallHook();

            foreach (var bind in binds)
            {
                if ((Keys)x == bind.InputKey)
                {
                    foreach (var key in bind.OutputKeys)
                    {
                        Keyboard.KeyDown(key);

                        Keyboard.KeyUp(key);

                        Thread.Sleep(bind.Delay);
                    }
                }
            }
            KeyboardHook.InstallHook();

        }

    }
}
