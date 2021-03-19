using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ArduinoLED
{
    ///https://www.tweaking4all.com/hardware/arduino/adruino-led-strip-effects/
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
            dropdowncombo.IsEnabled = false;
            Restrat();
            Thread t = new Thread(incomming);
            t.Start();
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "The app has been minimised. Click the tray icon to show.";
            m_notifyIcon.BalloonTipTitle = "ArduinoRGB";
            m_notifyIcon.Text = "ArduinoRGB";
            m_notifyIcon.Icon = new System.Drawing.Icon("icon.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
        }
        private string writeout = "";
        int extraparameters = 0;
        int index = 0;
        SerialPort port = new SerialPort();

        private void incomming()
        {
           
                port.DataReceived += Port_DataReceived;
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dropdowncombo.SelectedIndex;
            DefaultBTN.IsChecked = true;
            DefaultFunc();

        }

        #region custom
        private void CustomFunc()
        {
            switch (index)
            {
                case 0:
                    writeout = "0";
                    sendBTN.IsEnabled = true;
                    break;
                case 1:
                    writeout = "1";
                    sendBTN.IsEnabled = true;
                    break;
                case 2:
                    writeout = "2";
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    TripleColorCanvas.Visibility = Visibility.Visible;

                    extraparameters = 9;
                    //3rgb
                    break;
                case 3:
                    writeout = "3";
                    //number of flashes, flash speed, end pause, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Canvas2.Visibility = Visibility.Visible;
                    Canvas3.Visibility = Visibility.Visible;
                    Label1.Content = "Flash Number:";
                    Label2.Content = "Flash speed:";
                    Label3.Content = "Delay:";
                    extraparameters = 7;
                    break;
                case 4:
                    writeout = "4";
                    //speed delay, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Speed Delay";
                    extraparameters = 5;
                    break;
                case 5:
                    writeout = "5";
                    // sparkle delay, speed delay, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Canvas2.Visibility = Visibility.Visible;
                    Label1.Content = "Sparkle Delay:";
                    Label2.Content = "Speed delay:";
                    extraparameters = 6;
                    break;
                case 6:
                    writeout = "6";
                    //wave dealy 3RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    TripleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Wave Delay:";
                    extraparameters = 10;
                    break;
                case 7:
                    writeout = "7";
                    //speed delay 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Speed Delay:";
                    extraparameters = 5;
                    break;
                case 8:
                    writeout = "8";
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Speed Delay";
                    //speed delay--
                    extraparameters = 0;
                    break;
                case 9:
                    writeout = "9";
                    //(red, green, blue), speed delay--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Speed Delay";
                    extraparameters = 5;
                    break;
                case 10:
                    writeout = "10";
                    //Speed delay--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    Canvas1.Visibility = Visibility.Visible;
                    Label1.Content = "Speed Delay";
                    extraparameters = 0;
                    break;
                case 11:
                    writeout = "11";
                    //Cooling rate, Sparking rate, speed delay
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    Canvas1.Visibility = Visibility.Visible;
                    Canvas2.Visibility = Visibility.Visible;
                    Canvas3.Visibility = Visibility.Visible;
                    Label1.Content = "Cooling Rate";
                    Label2.Content = "Sparking rate";
                    Label3.Content = "Speed Delay";
                    extraparameters = 2;
                    break;
                case 12:
                    writeout = "12";
                    //color (red, green, blue) array,--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    extraparameters = 4;
                    break;
                case 13:
                    writeout = "13";
                    // 3x color (red, green, blue) array,--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    TripleColorCanvas.Visibility = Visibility.Visible;
                    extraparameters = 9;
                    break;
                case 14:
                    writeout = "14";
                    //Color (red, green, blue), meteor size, trail decay, random trail decay (true/false), speed delay
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    SingleColorCanvas.Visibility = Visibility.Visible;
                    Canvas1.Visibility = Visibility.Visible;
                    Canvas2.Visibility = Visibility.Visible;
                    Canvas5.Visibility = Visibility.Visible;
                    Canvas3.Visibility = Visibility.Visible;
                    Label1.Content = "Meteor Size";
                    Label2.Content = "Trail Decay";
                    Label5.Content = "Random Trail";
                    Label3.Content = "Speed Delay";
                    extraparameters = 14;
                    break;

            }
        }
        private void TX1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }
        private void TX2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (index == 5 && stringtoint(TX2.Text) < 100)
            {
                return;
            }
            CustomCheck();
        }
        private void TX3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }
        private void TX4_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }
        private void SingleR_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void SingleG_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void SingleB_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleR1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleG1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleB1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleR2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleG2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleB2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleR3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleG3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }

        private void TripleB3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomCheck();
        }
        private void checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            CustomCheck();
        }
        private void CustomCheck()
        {
            writeout = "";
            switch (extraparameters)
            {
                case 0://+1
                    if (TX1.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TX1.Text);
                        btnenabler();
                    }
                    break;
                case 1://+2
                    if (TX1.Text != "" && TX2.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text);
                        btnenabler();
                    }
                    break;
                case 2://+3
                    if (TX1.Text != "" && TX2.Text != "" && TX3.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text);
                        btnenabler();
                    }
                    break;
                case 3://+4
                    if (TX1.Text != "" && TX2.Text != "" && TX3.Text != "" && TX4.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text) + ";" + stringtoint(TX4.Text);
                        btnenabler();
                    }
                    break;
                case 4://1RGB
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "")
                    {
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(SingleB.Text);
                        btnenabler();
                    }
                    break;
                case 5://1RGB+1
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "" && TX1.Text != "")
                    {
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleB.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(TX1.Text);
                        btnenabler();
                    }
                    break;
                case 6://1RGB+2
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "" && TX1.Text != "" && TX2.Text != "")
                    {
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(SingleB.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text);
                        btnenabler();
                    }
                    break;
                case 7://1RGB+3
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "" && TX1.Text != "" && TX2.Text != "" && TX3.Text != "")
                    {
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(SingleB.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text);
                        btnenabler();
                    }
                    break;
                case 8://1RGB+4
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "" && TX1.Text != "" && TX2.Text != "" && TX3.Text != "" && TX4.Text != "")
                    {
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(SingleB.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text) + ";" + stringtoint(TX4.Text);
                        btnenabler();
                    }
                    break;
                case 9://3RGB
                    if (TripleR1.Text != "" && TripleG1.Text != "" && TripleB1.Text != "" && TripleR2.Text != "" && TripleG2.Text != "" && TripleB2.Text != "" && TripleR3.Text != "" && TripleG3.Text != "" && TripleB3.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TripleR1.Text) + ";" + stringtoint(TripleG1.Text) + ";" + stringtoint(TripleB1.Text) + ";" + stringtoint(TripleR2.Text) + ";" + stringtoint(TripleG2.Text) + ";" + stringtoint(TripleB2.Text) + ";" + stringtoint(TripleR3.Text) + ";" + stringtoint(TripleG3.Text) + ";" + stringtoint(TripleB3.Text);
                        btnenabler();
                    }
                    break;
                case 10://3RGB+1
                    if (TripleR1.Text != "" && TripleG1.Text != "" && TripleB1.Text != "" && TripleR2.Text != "" && TripleG2.Text != "" && TripleB2.Text != "" && TripleR3.Text != "" && TripleG3.Text != "" && TripleB3.Text != "" && TX1.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TripleR1.Text) + ";" + stringtoint(TripleG1.Text) + ";" + stringtoint(TripleB1.Text) + ";" + stringtoint(TripleR2.Text) + ";" + stringtoint(TripleG2.Text) + ";" + stringtoint(TripleB2.Text) + ";" + stringtoint(TripleR3.Text) + ";" + stringtoint(TripleG3.Text) + ";" + stringtoint(TripleB3.Text) + ";" + stringtoint(TX1.Text);
                        btnenabler();
                    }
                    break;
                case 11://3RGB+2
                    if (TripleR1.Text != "" && TripleG1.Text != "" && TripleB1.Text != "" && TripleR2.Text != "" && TripleG2.Text != "" && TripleB2.Text != "" && TripleR3.Text != "" && TripleG3.Text != "" && TripleB3.Text != "" && TX1.Text != "" && TX2.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TripleR1.Text) + ";" + stringtoint(TripleG1.Text) + ";" + stringtoint(TripleB1.Text) + ";" + stringtoint(TripleR2.Text) + ";" + stringtoint(TripleG2.Text) + ";" + stringtoint(TripleB2.Text) + ";" + stringtoint(TripleR3.Text) + ";" + stringtoint(TripleG3.Text) + ";" + stringtoint(TripleB3.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text);
                        btnenabler();
                    }
                    break;
                case 12://3RGB+3
                    if (TripleR1.Text != "" && TripleG1.Text != "" && TripleB1.Text != "" && TripleR2.Text != "" && TripleG2.Text != "" && TripleB2.Text != "" && TripleR3.Text != "" && TripleG3.Text != "" && TripleB3.Text != "" && TX1.Text != "" && TX2.Text != "" && TX3.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TripleR1.Text) + ";" + stringtoint(TripleG1.Text) + ";" + stringtoint(TripleB1.Text) + ";" + stringtoint(TripleR2.Text) + ";" + stringtoint(TripleG2.Text) + ";" + stringtoint(TripleB2.Text) + ";" + stringtoint(TripleR3.Text) + ";" + stringtoint(TripleG3.Text) + ";" + stringtoint(TripleB3.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text);
                        btnenabler();
                    }
                    break;
                case 13://3RGB+4
                    if (TripleR1.Text != "" && TripleG1.Text != "" && TripleB1.Text != "" && TripleR2.Text != "" && TripleG2.Text != "" && TripleB2.Text != "" && TripleR3.Text != "" && TripleG3.Text != "" && TripleB3.Text != "" && TX1.Text != "" && TX2.Text != "" && TX3.Text != "" && TX4.Text != "")
                    {
                        writeout = index + ";" + stringtoint(TripleR1.Text) + ";" + stringtoint(TripleG1.Text) + ";" + stringtoint(TripleB1.Text) + ";" + stringtoint(TripleR2.Text) + ";" + stringtoint(TripleG2.Text) + ";" + stringtoint(TripleB2.Text) + ";" + stringtoint(TripleR3.Text) + ";" + stringtoint(TripleG3.Text) + ";" + stringtoint(TripleB3.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + stringtoint(TX3.Text) + ";" + stringtoint(TX4.Text);
                        btnenabler();
                    }
                    break;
                case 14://1RGB+3+check
                    if (SingleR.Text != "" && SingleG.Text != "" && SingleB.Text != "" && TX1.Text != "" && TX2.Text != "" && TX3.Text != "")
                    {
                        string a;
                        if (checkbox1.IsChecked == true)
                        {
                            a = "true";
                        }
                        else
                        {
                            a = "false";
                        }
                        writeout = index + ";" + stringtoint(SingleR.Text) + ";" + stringtoint(SingleG.Text) + ";" + stringtoint(SingleB.Text) + ";" + stringtoint(TX1.Text) + ";" + stringtoint(TX2.Text) + ";" + a + ";" + stringtoint(TX3.Text);
                        btnenabler();
                    }
                    break;

            }
        }
        private void btnenabler()
        {
            sendBTN.IsEnabled = true;
        }
        #region singlecolorpicker
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            colorpicker picker = new colorpicker();
            picker.ShowDialog();
            SingleR.Text = "" + picker.r;
            SingleG.Text = "" + picker.g;
            SingleB.Text = "" + picker.b;
            CustomCheck();
        }
        #endregion

        #region triplecolorpicker
        private void ColorPicker1BTN(object sender, RoutedEventArgs e)
        {
            colorpicker picker = new colorpicker();
            picker.ShowDialog();
            TripleR1.Text = "" + picker.r;
            TripleG1.Text = "" + picker.g;
            TripleB1.Text = "" + picker.b;
        }
        private void ColorPicker2BTN(object sender, RoutedEventArgs e)
        {
            colorpicker picker = new colorpicker();
            picker.ShowDialog();
            TripleR2.Text = "" + picker.r;
            TripleG2.Text = "" + picker.g;
            TripleB2.Text = "" + picker.b;
        }
        private void ColorPicker3BTN(object sender, RoutedEventArgs e)
        {
            colorpicker picker = new colorpicker();
            picker.ShowDialog();
            TripleR3.Text = "" + picker.r;
            TripleG3.Text = "" + picker.g;
            TripleB3.Text = "" + picker.b;
            CustomCheck();
        }
        #endregion

        #endregion

        #region default
        private void DefaultFunc()
        {

            switch (index)
            {
                case 0:
                    writeout = "0";
                    sendBTN.IsEnabled = true;
                    break;
                case 1:
                    writeout = "1";
                    sendBTN.IsEnabled = true;
                    break;
                case 2:
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    writeout = "2;255;0;0;255;255;255;0;0;255";
                    sendBTN.IsEnabled = true;

                    extraparameters = 9;
                    //3rgb
                    break;
                case 3:
                    writeout = "3;255;255;255;10;50;1000";
                    //number of flashes, flash speed, end pause, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 7;
                    break;
                case 4:
                    writeout = "4;255;255;255;0";
                    //speed delay, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 5;
                    break;
                case 5:
                    writeout = "5;16;16;16;20;1000";
                    // sparkle delay, speed delay, 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 6;
                    break;
                case 6:
                    writeout = "6;255;0;0;255;255;255;0;0;255;100";
                    //wave dealy 3RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 10;
                    break;
                case 7:
                    writeout = "7;0;255;0;0;0;0;50";
                    //speed delay 1RGB--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 5;
                    break;
                case 8:
                    writeout = "8;20";
                    //speed delay--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 0;
                    break;
                case 9:
                    writeout = "9;255;0;0;100";
                    //(red, green, blue), speed delay--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 5;
                    break;
                case 10:
                    writeout = "10;50";
                    //Speed delay--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 0;
                    break;
                case 11:
                    writeout = "11;55;120;15";
                    //Cooling rate, Sparking rate, speed delay
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 2;
                    break;
                case 12:
                    writeout = "12;255;0;0";
                    //color (red, green, blue) array,--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 4;
                    break;
                case 13:
                    writeout = "13;255;0;0;255;255;255;0;0;255";
                    // 3x color (red, green, blue) array,--
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 9;
                    break;
                case 14:
                    writeout = "14;255;255;255;10;64;true;30";
                    //Color (red, green, blue), meteor size, trail decay, random trail decay (true/false), speed delay
                    CustomBTN.IsEnabled = true;
                    DefaultBTN.IsEnabled = true;
                    sendBTN.IsEnabled = true;
                    extraparameters = 14;
                    break;

            }
        }
        #endregion

        #region parser
        private int stringtoint(string inputtext)
        {
            int outputint=0;
            try
            {
                outputint = int.Parse(inputtext);
            }
            catch (Exception)
            {
                    outputint = 0;
            }
            return outputint;
        }
        #endregion

        #region Serial send/read
        private void SendBTN_Click(object sender, RoutedEventArgs e)//send button
        {
            // register the event
            
            //open the port


            try
            {
                // start the communication
                
                    port.Write(writeout);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Writing failed! \nError: " + ex.Message);
            }
            
        }
        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;

            // read input
            string incoming = port.ReadExisting();

            Console.WriteLine(incoming);
        }

        #endregion


        #region textboxchanged
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dropdowncombo.IsEnabled = true;
            
            try
            {
                port.PortName = "COM" + comportTX.Text;
                port.BaudRate = 115200;
                port.Open();
            }
            catch (Exception)
            {
                port.Close();
                System.Windows.MessageBox.Show("Invalid port!");
                
            }
           
        }
        #endregion

        private void Restrat()
        {
            Canvas1.Visibility = Visibility.Hidden;
            Canvas2.Visibility = Visibility.Hidden;
            Canvas3.Visibility = Visibility.Hidden;
            Canvas4.Visibility = Visibility.Hidden;
            Canvas5.Visibility = Visibility.Hidden;
            TripleColorCanvas.Visibility = Visibility.Hidden;
            SingleColorCanvas.Visibility = Visibility.Hidden;
            CustomBTN.IsEnabled = false;
            DefaultBTN.IsEnabled = false;
            sendBTN.IsEnabled = false;
            SingleR.Text = "";
            SingleG.Text = "";
            SingleB.Text = "";
            TripleR1.Text = "";
            TripleG1.Text = "";
            TripleB1.Text = "";
            TripleR2.Text = "";
            TripleG2.Text = "";
            TripleB2.Text = "";
            TripleR3.Text = "";
            TripleG3.Text = "";
            TripleB3.Text = "";
            TX1.Text = "";
            TX2.Text = "";
            TX3.Text = "";
            TX4.Text = "";
            checkbox1.IsChecked = false;
            Label1.Content = "";
            Label2.Content = "";
            Label3.Content = "";
            Label4.Content = "";
            Label5.Content = "";
        }
        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CustomBTN_Checked(object sender, RoutedEventArgs e)
        {
            DefaultBTN.IsChecked = false;
            Restrat();
            CustomFunc();
        }

        private void DefaultBTN_Checked(object sender, RoutedEventArgs e)
        {
            CustomBTN.IsChecked = false;
            Restrat();
            DefaultFunc();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
        void OnClose(object sender, CancelEventArgs args)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private WindowState m_storedWindowState = WindowState.Normal;
        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (m_notifyIcon != null)
                    m_notifyIcon.ShowBalloonTip(2000);
            }
            else
                m_storedWindowState = WindowState;
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }
    }
}
