using System.Xml;
using System.Windows;
using System;


namespace Ruokalista5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            XmlReader lukija = XmlReader.Create("menu.xml");
            string viikonPäivä = "";
            lukija.MoveToContent();
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element && lukija.Name == "Viikonpäivä")
                {
                    if (lukija.HasAttributes)
                    {
                        viikonPäivä = lukija.GetAttribute("päivä");
                        lstViikonpäivät.Items.Add(viikonPäivä);
                    }
                }
            }
        }

        private void lstViikonpäivät_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string päivä = "";
            päivä = lstViikonpäivät.SelectedItem.ToString();
            XmlReader lukija = XmlReader.Create("menu.xml");
            lukija.MoveToContent();
            lstRuokalista.Items.Clear();
            lstHinta.Items.Clear();
            lblRuokalista.Content = päivä + " menu";
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element &&
                    lukija.Name == "Viikonpäivä")
                {
                    if (lukija.HasAttributes)
                    {
                        string haettuPäivä = lukija.GetAttribute("päivä");
                        if (päivä == haettuPäivä)
                        {
                            while (lukija.Read())
                            {
                                if (lukija.NodeType == XmlNodeType.EndElement &&
                                    lukija.Name == "Ruoat")
                                {
                                    return;
                                }
                                if (lukija.NodeType == XmlNodeType.Element &&
                                  lukija.Name == "Ruoka")
                                {
                                    lukija.Read();
                                    lstRuokalista.Items.Add(lukija.Value);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lstRuokalista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {           
            string päivä = "";            
            päivä = lstViikonpäivät.SelectedItem.ToString();
           
            XmlReader lukija = XmlReader.Create("menu.xml");
            lukija.MoveToContent();
            lstHinta.Items.Clear();
            lblHinta.Content = "Hinta";
            while (lukija.Read())
            {
                if(lukija.NodeType == XmlNodeType.Element && lukija.Name == "Viikonpäivä")
                {
                    if (lukija.HasAttributes)
                    {
                        string haettupäivä = lukija.GetAttribute("päivä");
                        if (päivä == haettupäivä)
                        {
                            while (lukija.Read())
                            {
                                if (lukija.NodeType == XmlNodeType.EndElement &&
                                    lukija.Name == "Ruoat")
                                {
                                    return;
                                }
                                if (lukija.NodeType == XmlNodeType.Element &&
                                  lukija.Name == "Ruoka")
                                {
                                    while (lukija.Read())
                                    {
                                        if (lukija.NodeType == XmlNodeType.Element &&
                                          lukija.Name == "Hinnat")
                                        {
                                            
                                        }
                                        if (lukija.NodeType == XmlNodeType.Element &&
                                        lukija.Name == "Normaalihinta")
                                        {
                                            lukija.Read();
                                            lstHinta.Items.Add(lukija.Value);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
   
