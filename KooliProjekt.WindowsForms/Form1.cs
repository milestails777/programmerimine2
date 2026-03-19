using System.Collections;
using System.Net.Http.Json;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
        private readonly IApiClient _apiClient;

        public Form1(IApiClient apiClient)
        {
            _apiClient = apiClient;

            InitializeComponent();

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            saveCommand.Click += SaveCommand_Click;
            addCommand.Click += AddCommand_Click;
            deleteCommand.Click += deletecommand_Click;
        }

        private void AddCommand_Click(object sender, EventArgs e)
        {
            idField.Text = "0";
            titleField.Text = string.Empty;
            budgetField.Text = string.Empty;
            priceField.Text = string.Empty;
            startDateField.Text = string.Empty;
            dueDateField.Text = string.Empty;
        }

        private void deletecommand_Click(object sender, EventArgs e)
        {

            Task.Run(async () =>
            {
                await _apiClient.Delete(int.Parse(idField.Text));
                await LoadProjects();
            });
        }

        private void SaveCommand_Click(object sender, EventArgs e)
        {
            var project = new Project();
            project.Id = int.Parse(idField.Text);
            project.Name = titleField.Text;
            project.Budget = decimal.Parse(budgetField.Text);
            project.PricePerHour = decimal.Parse(priceField.Text);
            project.StartDate = DateTime.Parse(startDateField.Text);
            project.DueDate = DateTime.Parse(dueDateField.Text);

            Task.Run(async () =>
            {
                await _apiClient.Save(project);
                await LoadProjects();
            });
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedList = (Project)dataGridView1.CurrentRow.DataBoundItem;
            if (selectedList == null)
            {
                return;
            }

            idField.Text = selectedList.Id.ToString();
            titleField.Text = selectedList.Name;
            budgetField.Text = selectedList.Budget.ToString();
            priceField.Text = selectedList.PricePerHour.ToString();
            startDateField.Text = selectedList.StartDate.ToString();
            dueDateField.Text = selectedList.DueDate.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(async () => await LoadProjects());
        }

        private async Task LoadProjects()
        {
            var response = await _apiClient.List(1, 100);

            this.Invoke(() =>
            {
                dataGridView1.DataSource = response.Value.Results;
            });
        }

        private void saveCommand_Click_1(object sender, EventArgs e)
        {
            var project = new Project();
            project.Name = titleField.Text;
            project.Id = int.Parse(idField.Text);
            project.Budget = decimal.Parse(budgetField.Text);
            project.PricePerHour = decimal.Parse(priceField.Text);
            project.StartDate = DateTime.Parse(startDateField.Text);
            project.DueDate = DateTime.Parse(dueDateField.Text);

            Task.Run(async () =>
            {
                await _apiClient.Save(project);
                await LoadProjects();
            });
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
    }
}
