﻿using System;
using System.Windows;
using System.Windows.Threading;
using System.Globalization;
using System.Windows.Documents;
using System.IO;

namespace wt_betty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// NEW BETTY
    /// 
    //todo: fuel warning
    //todo: eula, help,

    public partial class MainWindow : Window
    {

        private indicator myIndicator = new indicator();
        private state myState = new state();
        string indicatorsurl = "http://localhost:8111/indicators";
        string statesurl = "http://localhost:8111/state";
        DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
        DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
        CultureInfo culture = new CultureInfo("en-US");
        FlowDocument myFlowDoc = new FlowDocument();
        Paragraph par = new Paragraph();

        public MainWindow()
        {
            InitializeComponent();
            cbx_a.IsChecked = User.Default.EnableA;
            cbx_g.IsChecked = User.Default.EnableG;
            cbx_gear.IsChecked = User.Default.EnableGear;
            slider_A.Value = Convert.ToDouble(User.Default.AoA);
            slider_G.Value = Convert.ToDouble(User.Default.GForce);
            textBox_aSlider.Text = slider_A.Value.ToString();
            textBox_gSlider.Text = slider_G.Value.ToString();
            tbx_gearup.Text = User.Default.GearUp.ToString();
            tbx_geardown.Text = User.Default.GearDown.ToString();
            
            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer1_Tick);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 200);
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 5);

        }

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            WTConnect();
        }

        public void WTConnect()
        {
            try
            {
                if (BaglantiVarmi("localhost", 8111))
                {



                    myState = JsonSerializer._download_serialized_json_data<state>(statesurl);
                    if (myState.valid == "true")
                    {
                        dispatcherTimer2.Stop();
                        dispatcherTimer1.Start();
                        tbx_msgs.Text = ("Running");
                        button_start.IsEnabled = false;
                        button_stop.IsEnabled = true;

                    }
                    else if (myState.valid == "false")
                    {
                        dispatcherTimer2.Start();
                        dispatcherTimer1.Stop();
                        tbx_msgs.Text = "Waiting for a flight...";
                        button_start.IsEnabled = false;
                        button_stop.IsEnabled = true;

                    }

                }
                else
                {
                    //Dinlemeye geç
                    dispatcherTimer2.Start();
                    dispatcherTimer1.Stop();
                    tbx_msgs.Text = ("War Thunder is not running...");

                    button_start.IsEnabled = true;
                    button_stop.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {

                tbx_msgs.Text = ex.Message;
                dispatcherTimer1.Stop();
                dispatcherTimer2.Start();
                button_start.IsEnabled = true;
                button_stop.IsEnabled = false;

            }



        }

        private void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            getData();

        }

        private bool BaglantiVarmi(string adres, int port)
        {
            try
            {

                System.Net.Sockets.TcpClient baglanti = new System.Net.Sockets.TcpClient(adres, port);
                baglanti.Close();

                return true;
            }
            catch
            {
                return false;
            }

        }

        private void getData()
        {
            try
            {
                myIndicator = JsonSerializer._download_serialized_json_data<indicator>(indicatorsurl);
                myState = JsonSerializer._download_serialized_json_data<state>(statesurl);



                if (myState.valid == "true")
                {



                    decimal G = Convert.ToDecimal(myState.Ny, culture);
                    decimal AoA = Convert.ToDecimal(myState.AoA, culture);
                    decimal Alt = Convert.ToDecimal(myIndicator.altitude_hour, culture);
                    int gear = Convert.ToInt32(myState.gear);
                    int IAS = Convert.ToInt32(myState.IAS);
                    int flaps = Convert.ToInt32(myState.flaps);
                    label.Content = myIndicator.type;

                    if (cbx_g.IsChecked == true && G > User.Default.GForce )
                    {
                        System.Media.SoundPlayer myPlayer;
                        myPlayer = new System.Media.SoundPlayer(Properties.Resources.breaths);
                        myPlayer.PlaySync();
                    }

                   

                    if (AoA > User.Default.AoA && myIndicator.gears_lamp == "1" && cbx_a.IsChecked == true)
                    {
                        System.Media.SoundPlayer myPlayer;
                        myPlayer = new System.Media.SoundPlayer(Properties.Resources.carflt_stallhorn02);
                        myPlayer.PlaySync();

                    }
                    if (User.Default.EnableGear == true && gear == 100 && IAS > User.Default.GearUp)
                    {
                        System.Media.SoundPlayer myPlayer;
                        myPlayer = new System.Media.SoundPlayer(Properties.Resources.gearwarn);
                        myPlayer.PlaySync();
                    }

                    if (User.Default.EnableGear == true && gear == 0 && IAS < User.Default.GearDown && Alt < 500 && flaps > 20)
                    {
                        System.Media.SoundPlayer myPlayer;
                        myPlayer = new System.Media.SoundPlayer(Properties.Resources.gearwarn);
                        myPlayer.PlaySync();
                    }

                }
                else
                {
                    dispatcherTimer1.Stop();
                    dispatcherTimer2.Start();


                }
            }
            catch (Exception ex)
            {
                tbx_msgs.Text = ex.Message;
                dispatcherTimer1.Stop();
                dispatcherTimer2.Start();


            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer1.Stop();
            dispatcherTimer2.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WTConnect();

        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer2.Start();
            if (dispatcherTimer2.IsEnabled)
            {
                button_start.IsEnabled = false;
                button_stop.IsEnabled = true;
            }
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer1.Stop();
            dispatcherTimer2.Stop();
            button_start.IsEnabled = true;
            button_stop.IsEnabled = false;

        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User.Default.EnableA = cbx_a.IsChecked.Value;
                User.Default.EnableG = cbx_g.IsChecked.Value;
                User.Default.EnableGear = cbx_gear.IsChecked.Value;
                User.Default.GForce = Convert.ToInt32(slider_G.Value);
                User.Default.AoA = Convert.ToInt32(slider_A.Value);
                User.Default.GearDown = Convert.ToInt32(tbx_geardown.Text);
                User.Default.GearUp = Convert.ToInt32(tbx_gearup.Text);
                User.Default.Save();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User.Default.EnableA = true;
                User.Default.EnableG = true;
                User.Default.EnableGear = true;
                User.Default.GForce = 6;
                User.Default.AoA = 12;
                User.Default.GearDown = 210;
                User.Default.GearUp = 250;
                User.Default.Save();

                cbx_a.IsChecked = User.Default.EnableA;
                cbx_g.IsChecked = User.Default.EnableG;
                cbx_gear.IsChecked = User.Default.EnableGear;
                slider_A.Value = Convert.ToDouble(User.Default.AoA);
                slider_G.Value = Convert.ToDouble(User.Default.GForce);
                textBox_aSlider.Text = slider_A.Value.ToString();
                textBox_gSlider.Text = slider_G.Value.ToString();
                tbx_gearup.Text = User.Default.GearUp.ToString();
                tbx_geardown.Text = User.Default.GearDown.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void btn_help_Click(object sender, RoutedEventArgs e)
        {
            var helpFile = Path.Combine(Path.GetTempPath(), "wt-betty-help.txt");
            File.WriteAllText(helpFile, Properties.Resources.wt_betty_help);
            System.Diagnostics.Process.Start(helpFile);
        }
    }
}
