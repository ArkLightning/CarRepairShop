using Microsoft.Extensions.Logging;
using CarRepairShop;
using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopContracts.SearchModels;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRepairShop
{
    public partial class FormCar : Form
    {
        private readonly ILogger _logger;
        private readonly ICarLogic _logic;
        private int? _id;
        private Dictionary<int, (IDetailModel, int)> _carDetails;
        public int Id { set { _id = value; } }
        public FormCar(ILogger<FormCar> logger, ICarLogic logic)
        {
            InitializeComponent();
            _logger = logger;
            _logic = logic;
            _carDetails = new Dictionary<int, (IDetailModel, int)>();
        }

        private void FormCar_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                _logger.LogInformation("Загрузка изделия");
                try
                {
                    var view = _logic.ReadElement(new CarSearchModel
                    {
                        Id = _id.Value
                    });
                    if (view != null)
                    {
                        textBoxName.Text = view.CarName;
                        textBoxPrice.Text = view.Price.ToString();
                        _carDetails = view.CarDetails ?? new Dictionary<int, (IDetailModel, int)>();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка загрузки изделия");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void LoadData()
        {
            _logger.LogInformation("Загрузка деталей изделия");
            try
            {
                if (_carDetails != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in _carDetails)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1.DetailName, pc.Value.Item2 });
                    }
                    textBoxPrice.Text = CalcPrice().ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки компонент изделия");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormCarDetail));
            if (service is FormCarDetail form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.DetailModel == null)
                    {
                        return;
                    }
                    _logger.LogInformation("Добавление нового детали:{ DetailName} - { Count}", form.DetailModel.DetailName, form.Count);
                    if (_carDetails.ContainsKey(form.Id))
                    {
                        _carDetails[form.Id] = (form.DetailModel, form.Count);
                    }
                    else
                    {
                        _carDetails.Add(form.Id, (form.DetailModel, form.Count));
                    }
                    LoadData();
                }
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var service = Program.ServiceProvider?.GetService(typeof(FormCarDetail));
                if (service is FormCarDetail form)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    form.Id = id;
                    form.Count = _carDetails[id].Item2;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.DetailModel == null)
                        {
                            return;
                        }
                        _logger.LogInformation("Изменение детали: { DetailName} - { Count}", form.DetailModel.DetailName, form.Count);
                        _carDetails[form.Id] = (form.DetailModel, form.Count);
                        LoadData();
                    }
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись?", "Вопрос",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _logger.LogInformation("Удаление детали:{ DetailName} - { Count}", dataGridView.SelectedRows[0].Cells[1].Value);
                        _carDetails?.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_carDetails == null || _carDetails.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.LogInformation("Сохранение изделия");
            try
            {
                var model = new CarBindingModel
                {
                    Id = _id ?? 0,
                    CarName = textBoxName.Text,
                    Price = Convert.ToDouble(textBoxPrice.Text),
                    CarDetails = _carDetails
                };
                var operationResult = _id.HasValue ? _logic.Update(model) : _logic.Create(model);
                if (!operationResult)
                {
                    throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка сохранения изделия");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private double CalcPrice()
        {
            double price = 0;
            foreach (var elem in _carDetails)
            {
                price += ((elem.Value.Item1?.Cost ?? 0) * elem.Value.Item2);
            }
            return Math.Round(price * 1.1, 2);
        }
    }
}
