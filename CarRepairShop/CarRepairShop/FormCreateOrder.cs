using Microsoft.Extensions.Logging;
using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopContracts.SearchModels;
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
    public partial class FormCreateOrder : Form
    {
        private readonly ILogger _logger;
        private readonly ICarLogic _logicS;
        private readonly IOrderLogic _logicO;
        public FormCreateOrder(ILogger<FormCreateOrder> logger, ICarLogic logicS, IOrderLogic logicO)
        {
            InitializeComponent();
            _logger = logger;
            _logicS = logicS;
            _logicO = logicO;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            _logger.LogInformation("Загрузка изделий для заказа");
            try
            {
                var list = _logicS.ReadList(null);
                if (list != null)
                {
                    ComboBoxCars.DisplayMember = "CarName";
                    ComboBoxCars.ValueMember = "Id";
                    ComboBoxCars.DataSource = list;
                    ComboBoxCars.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки списка кораблей");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (ComboBoxCars.SelectedValue != null &&
           !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(ComboBoxCars.SelectedValue);
                    var product = _logicS.ReadElement(new CarSearchModel
                    {
                        Id = id
                    });
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = Math.Round(count * (product?.Price ?? 0), 2).ToString();
                    _logger.LogInformation("Расчет суммы заказа");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка расчета суммы заказа");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ComboBoxCars.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.LogInformation("Создание заказа");
            try
            {
                var operationResult = _logicO.CreateOrder(new OrderBindingModel
                {
                    CarId = Convert.ToInt32(ComboBoxCars.SelectedValue),
                    CarName = ComboBoxCars.Text,
                    Count = Convert.ToInt32(TextBoxCount.Text),
                    Sum = Convert.ToDouble(TextBoxSum.Text)
                });
                if (!operationResult)
                {
                    throw new Exception("Ошибка при создании заказа.Дополнительная информация в логах.");
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания заказа");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
