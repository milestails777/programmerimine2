using System.Collections;
using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1(Api.IApiClient apiClient)
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadTodoLists();
        }

        private void LoadTodoLists()
        {
            var url = "http://localhost:5086/api/Projects/List";
            url += "?page=1&pageSize=10";

            using var client = new HttpClient();            
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<Project>>>(url).Result;
            dataGridView1.DataSource = response.Value.Results;
        }
    }
}
