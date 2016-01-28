using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using agsXMPP;
using agsXMPP.protocol.client;
using System.Net;

namespace LeagueOfLegends_Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> Contactos = new List<string>(new string[] { });
        List<string> Contactos_Todos = new List<string>(new string[] { });
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Conectar("NOME", "PASS");                                         // POE AQUI OS DADOS DE LOGIN DA TUA CONTA
        }

        public void Conectar(string user, string password)
        {
            XmppClientConnection xmpp = new XmppClientConnection("pvp.net");
            xmpp.UseSSL = true;
            xmpp.Port = 5223;
            xmpp.Resource = "xiff";
            xmpp.AutoResolveConnectServer = false;
            xmpp.ConnectServer = "chat.euw1.lol.riotgames.com";
            //Presence p = new Presence(ShowType.chat, "Online");
            //p.Type = PresenceType.available;
            //xmpp.Send(p);
            xmpp.OnRosterItem += new XmppClientConnection.RosterHandler( xmpp_OnRosterItem);
            xmpp.OnPresence += new PresenceHandler(xmpp_OnPresence);
            xmpp.Open(user, "AIR_" + password);

            //xmpp.OnLogin += delegate (object o) { MessageBox.Show("Logged in as " + xmpp.Username); };
            System.Threading.Thread.Sleep(2000);
            foreach (string nome in Contactos_Todos)
                    {

                Offline_Friends.Items.Add(nome);
                       
                    }
            foreach (string nome in Contactos)
            {

                Online_Friends.Items.Add(nome);

            }
            button.Background = Brushes.Green;
            button.IsEnabled = false;
            button.Content = "CONECTED!";
            
        }

        public void xmpp_OnPresence(object sender, Presence pres)
        {
            
            Contactos.Add(pres.From.User);             //PROBLEMA, DA ME O SUMMONER ID EM VEZ DO NOME (pode se verificar o nome aqui http://www.lolking.net/summoner/euw/ID --> Substitui se ID pelo id ex:19358540
            //System.Threading.Thread.Sleep(100);
        }

        public void xmpp_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem RosterItem)
        {
            
                Contactos_Todos.Add(RosterItem.Name);
            
        }

     /*private string PegaNome(string id)
        {
            string nome;
            string chave = "?api_key=067c7765-e085-4a55-83f1-be65b5869416";
            WebClient net = new WebClient();
            string texto = net.DownloadString("https://euw.api.pvp.net/api/lol/euw/v1.4/summoner/" + id.Replace("sum", "") + chave);
            string[] papas = texto.Split('"');
            nome = papas[7];
            return nome;
        }*/
          
    }
}
