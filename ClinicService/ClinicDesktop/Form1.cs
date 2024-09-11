using ClinicServiceNamespace;
using ClinicService.Models;


namespace ClinicDesktop
{
public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  async void btnUpdateButton_Click(object sender, EventArgs e)
        {
            ClinicClient clinicClient = new ClinicClient("http://localhost:5111/", new HttpClient());
            ICollection<Client> clients = await clinicClient.ClientGetAllAsync();
        
            listViewClients.Items.Clear();
            foreach (Client client in clients)
            {
                ListViewItem item = new ListViewItem();
                item.Text = client.ToString();
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.SurName
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.FirstName
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = client.Patronymic
                });
                listViewClients.Items.Add(item);
                
            }        
        }
    }

    public partial class Sample
    {
        public int a;
    }
}
