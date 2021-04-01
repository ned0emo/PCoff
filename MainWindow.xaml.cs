using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCOff
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CheckBox> list102 = new List<CheckBox>();
        private List<CheckBox> list103 = new List<CheckBox>();
        private List<CheckBox> list104 = new List<CheckBox>();
        private List<CheckBox> list105 = new List<CheckBox>();
        private List<CheckBox> list106 = new List<CheckBox>();
        private int checkProgress;

        public MainWindow()
        {
            InitializeComponent();

            checkProgress = 0;

            #region lists
            list102.Add(checkBox1021);
            list102.Add(checkBox1022);
            list102.Add(checkBox1023);
            list102.Add(checkBox1024);
            list102.Add(checkBox1025);
            list102.Add(checkBox1026);
            list102.Add(checkBox1027);
            list102.Add(checkBox1028);
            
            list103.Add(checkBox31);
            list103.Add(checkBox32);
            list103.Add(checkBox33);
            list103.Add(checkBox34);
            list103.Add(checkBox35);
            list103.Add(checkBox36);
            list103.Add(checkBox37);
            list103.Add(checkBox38);

            list104.Add(checkBox41);
            list104.Add(checkBox42);
            list104.Add(checkBox43);
            list104.Add(checkBox44);
            list104.Add(checkBox45);
            list104.Add(checkBox46);
            list104.Add(checkBox47);
            list104.Add(checkBox48);

            list105.Add(checkBox51);
            list105.Add(checkBox52);
            list105.Add(checkBox53);
            list105.Add(checkBox54);
            list105.Add(checkBox55);
            list105.Add(checkBox56);
            list105.Add(checkBox57);
            list105.Add(checkBox58);
            list105.Add(checkBox59);
            list105.Add(checkBox510);

            list106.Add(checkBox61);
            list106.Add(checkBox62);
            list106.Add(checkBox63);
            list106.Add(checkBox64);
            list106.Add(checkBox65);
            list106.Add(checkBox66);
            list106.Add(checkBox67);
            list106.Add(checkBox68);
            list106.Add(checkBox69);
            list106.Add(checkBox610);
            #endregion

            PingCheckThreads();
        }        

        private void PingCheckThreads()
        {
            checkProgress = 0;
            buttonRefresh.IsEnabled = false;
            button102.IsEnabled = false;
            button103.IsEnabled = false;
            button104.IsEnabled = false;
            button105.IsEnabled = false;
            button106.IsEnabled = false;

            new Thread(new ParameterizedThreadStart(PingCheck))
                .Start(new Tuple<List<CheckBox>, CheckBox>(list102, all102CheckBox));
            new Thread(new ParameterizedThreadStart(PingCheck))
                .Start(new Tuple<List<CheckBox>, CheckBox>(list103, all103CheckBox));
            new Thread(new ParameterizedThreadStart(PingCheck))
                .Start(new Tuple<List<CheckBox>, CheckBox>(list104, all104CheckBox));
            new Thread(new ParameterizedThreadStart(PingCheck))
                .Start(new Tuple<List<CheckBox>, CheckBox>(list105, all105CheckBox));
            new Thread(new ParameterizedThreadStart(PingCheck))
                .Start(new Tuple<List<CheckBox>, CheckBox>(list106, all106CheckBox));
        }

        private void PingCheck(object obj)
        {
            try
            {
                var list = (Tuple<List<CheckBox>, CheckBox>)obj;
                var isEnabledExist = false;

                foreach (CheckBox i in list.Item1)
                {
                    var compName = "";

                    Dispatcher.Invoke(() =>
                    {
                        compName = i.Content.ToString();
                    });

                    Ping pingSender = new Ping();
                    PingOptions options = new PingOptions();
                    PingReply reply;

                    try
                    {
                        reply = pingSender.Send(compName, 5);
                    }
                    catch
                    {
                        Dispatcher.Invoke(() =>
                        {
                            i.Foreground = Brushes.Red;
                        });

                        continue;
                    }

                    if (reply.Status == IPStatus.Success)
                    {
                        isEnabledExist = true;

                        Dispatcher.Invoke(() =>
                        {
                            i.IsEnabled = true;
                            i.IsChecked = true;
                            i.Foreground = Brushes.Green;
                        });
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            i.Foreground = Brushes.Red;
                        });
                    }
                }

                if (isEnabledExist)
                {
                    Dispatcher.Invoke(() =>
                    {
                        list.Item2.IsEnabled = true;
                        list.Item2.IsChecked = true;

                        switch (list.Item2.Name)
                        {
                            case "all102CheckBox":
                                button102.IsEnabled = true;
                                break;
                            case "all103CheckBox":
                                button103.IsEnabled = true;
                                break;
                            case "all104CheckBox":
                                button104.IsEnabled = true;
                                break;
                            case "all105CheckBox":
                                button105.IsEnabled = true;
                                break;
                            case "all106CheckBox":
                                button106.IsEnabled = true;
                                break;
                        }
                    });
                }

                try
                {
                    checkProgress++;

                    if (checkProgress > 4)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            buttonRefresh.IsEnabled = true;
                            buttonAll.IsEnabled = true;
                        });
                    }
                }
                catch
                {
                    Thread.Sleep(5);
                    checkProgress++;

                    if (checkProgress > 4)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            buttonRefresh.IsEnabled = true;
                            buttonAll.IsEnabled = true;
                        });
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void MakeCheckBoxBlack(List<CheckBox> list)
        {
            foreach(CheckBox i in list)
            {
                i.IsChecked = false;
                i.IsEnabled = false;
                i.Foreground = Brushes.Black;
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            all102CheckBox.IsEnabled = false;
            all102CheckBox.IsChecked = false;
            all103CheckBox.IsEnabled = false;
            all103CheckBox.IsChecked = false;
            all104CheckBox.IsEnabled = false;
            all104CheckBox.IsChecked = false;
            all105CheckBox.IsEnabled = false;
            all105CheckBox.IsChecked = false;
            all106CheckBox.IsEnabled = false;
            all106CheckBox.IsChecked = false;

            buttonAll.IsEnabled = false;
            button102.IsEnabled = false;
            button103.IsEnabled = false;
            button104.IsEnabled = false;
            button105.IsEnabled = false;
            button106.IsEnabled = false;

            MakeCheckBoxBlack(list102);
            MakeCheckBoxBlack(list103);
            MakeCheckBoxBlack(list104);
            MakeCheckBoxBlack(list105);
            MakeCheckBoxBlack(list106);

            PingCheckThreads();
        }

        private void PcOff(List<CheckBox> list)
        {
            foreach(CheckBox i in list)
            {
                if (i.IsChecked == true)
                {
                    System.Diagnostics.Process.Start(
                        "cmd.exe", "/C " + "shutdown /s /t 0 /m \\\\" + i.Content.ToString());
                }
            }
        }

        private void AllCheckBoxStatChange(List<CheckBox> list, CheckBox currentCheckBox)
        {
            foreach(CheckBox i in list)
            {
                if (i.IsEnabled)
                {
                    if (currentCheckBox.IsChecked == true)
                    {
                        i.IsChecked = true;
                    }
                    else
                    {
                        i.IsChecked = false;
                    }
                }
            }
        }

        #region pcOff
        private void Button102_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list102);
        }

        private void Button103_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list103);
        }

        private void Button104_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list104);
        }

        private void Button105_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list105);
        }

        private void Button106_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list106);
        }

        private void ButtonAll_Click(object sender, RoutedEventArgs e)
        {
            PcOff(list102);
            PcOff(list103);
            PcOff(list104);
            PcOff(list105);
            PcOff(list106);
        }
        #endregion

        private void All102CheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckBoxStatChange(list102, all102CheckBox);
        }

        private void All103CheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckBoxStatChange(list103, all103CheckBox);
        }

        private void All104CheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckBoxStatChange(list104, all104CheckBox);
        }

        private void All105CheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckBoxStatChange(list105, all105CheckBox);
        }

        private void All106CheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckBoxStatChange(list106, all106CheckBox);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
