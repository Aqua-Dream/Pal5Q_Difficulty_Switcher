using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace 仙五前难度修改器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread threadRefresh;
        bool CanWrite = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,new Action(() =>
            {
                threadRefresh = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        try
                        {
                            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                            {
                                if (Memory.GetProcessByName("Pal5Q"))
                                {
                                    int Address = Memory.ReadInt32(0x00A48A40) +0x200;
                                    int Mode = Memory.ReadInt32(Address);
                                    switch (Mode)
                                    {
                                        case 1: label2.Content = "简单"; CanWrite = true; break;
                                        case 2: label2.Content = "普通"; CanWrite = true; break;
                                        case 3: label2.Content = "困难"; CanWrite = true; break;
                                        default: label2.Content = "未读档"; CanWrite = false; break;
                                    }
                                }
                                else
                                {
                                    label2.Content = "找不到游戏！";
                                    CanWrite = false;
                                }
                            }));
                        }
                        catch { }
                        Thread.Sleep(300);
                    }
                }));
                threadRefresh.IsBackground = true;
                threadRefresh.Start();

            }));
        }

        private void label5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tieba.baidu.com/p/3444438994");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (CanWrite)
            {
                int n = comboBox1.SelectedIndex;
                int Address = Memory.ReadInt32(0x00A48A40) + 0x200;
                if (n != -1)
                {
                    Memory.WriteMemory(Address, n + 1);
                    MessageBox.Show("修改成功！","提示");
                }
                else
                {
                    MessageBox.Show("请先选择要修改的难度！","提示");
                }
            }
            else
            {
                MessageBox.Show("请先打开游戏并读档！","错误");
            }
        }
    }
}
