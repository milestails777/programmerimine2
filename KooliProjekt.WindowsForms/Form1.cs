using KooliProjekt.WindowsForms.Api;
using System.Collections;
using System.ComponentModel;
using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form, IMainView
    {
        private readonly IApiClient _apiClient;
        private MainViewPresenter _mainViewPresenter;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<Project> DataSource
        {
            get { return (IList<Project>)dataGridView1.DataSource; }
            set { dataGridView1.DataSource = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Project SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentId
        {
            get { return int.Parse(idField.Text); }
            set { idField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurrentName
        {
            get { return titleField.Text; }
            set { titleField.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentBudget
        {
            get { return decimal.Parse(budgetField.Text); }
            set { budgetField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentPricePerHour
        {
            get { return decimal.Parse(priceField.Text); }
            set { priceField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime CurrentStartDate
        {
            get { return DateTime.Parse(startDateField.Text); }
            set { startDateField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime CurrentDueDate
        {
            get { return DateTime.Parse(dueDateField.Text); }
            set { dueDateField.Text = value.ToString(); }
        }

        public void SetPresenter(MainViewPresenter presenter)
        {
            _mainViewPresenter = presenter;
        }

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

        private async void deletecommand_Click(object sender, EventArgs e)
        {

            var message = "Oled kindel, et soovid kustutada " + titleField.Text + "?";
            var answer = MessageBox.Show(message, "Kustutamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
            {
                return;
            }

            var id = int.Parse(idField.Text);
            var result = await _apiClient.Delete(id);
            if (result.HasErrors)
            {
                ShowError("Viga kustutamisel", result);
            }

            await _mainViewPresenter.LoadData();
        }

        private async void SaveCommand_Click(object sender, EventArgs e)
        {
            var project = new Project();
            project.Id = int.Parse(idField.Text);
            project.Name = titleField.Text;
            project.Budget = decimal.Parse(budgetField.Text);
            project.PricePerHour = decimal.Parse(priceField.Text);
            project.StartDate = DateTime.Parse(startDateField.Text);
            project.DueDate = DateTime.Parse(dueDateField.Text);

            var result = await _apiClient.Save(project);
            if (result.HasErrors)
            {
                ShowError("Viga salvestamisel", result);
            }
            await _mainViewPresenter.LoadData();
        }

        public bool ConfirmDelete()
        {
            var message = "Oled kindel, et soovid kustutada " + titleField.Text + "?";
            var answer = MessageBox.Show(message, "Kustutamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (answer == DialogResult.Yes);
        }

        public void ShowError(string message, OperationResult result)
        {
            var error = message + "\r\n";
            var apiErrors = "";
            var propertyErrors = "";

            if (result.Errors != null)
            {
                foreach (var apiError in result.Errors)
                {
                    apiErrors += apiError + "\r\n";
                }
            }

            if (result.PropertyErrors != null)
            {
                foreach (var propertyError in result.PropertyErrors)
                {
                    propertyErrors += propertyError.Key + ": " + propertyError.Value;
                }
            }

            if (!string.IsNullOrEmpty(apiErrors))
            {
                error += "\r\n" + apiErrors + "\r\n";
            }

            if (!string.IsNullOrEmpty(propertyErrors))
            {
                error += "\r\n" + propertyErrors;
            }

            error = error.Trim();

            MessageBox.Show(error, "Viga!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                _mainViewPresenter.SetSelection(null);
                return;
            }

            var selectedList = (Project)dataGridView1.CurrentRow.DataBoundItem;
            _mainViewPresenter.SetSelection(selectedList);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await _mainViewPresenter.LoadData();
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
                await _mainViewPresenter.LoadData();
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
