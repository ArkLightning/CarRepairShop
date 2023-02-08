﻿using Microsoft.Extensions.Logging;
using CarRepairShop;
using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopDataModels.Enums;
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
    public partial class FormMain : Form
    {
        private readonly ILogger _logger;
        private readonly IOrderLogic _orderLogic;
        public FormMain(ILogger<FormMain> logger, IOrderLogic orderLogic)
        {
            InitializeComponent();
            _logger = logger;
            _orderLogic = orderLogic;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            _logger.LogInformation("Загрузка заказов");
            try
            {
                var list = _orderLogic.ReadList(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns["CarId"].Visible = false;
                }
                _logger.LogInformation("Загрузка заказов");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки заказов");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ДеталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormDetails));
            if (service is FormDetails form)
            {
                form.ShowDialog();
            }
        }

        private void АвтомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormCars));
            if (service is FormCars form)
            {
                form.ShowDialog();
            }
        }

        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormCreateOrder));
            if (service is FormCreateOrder form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'В работе'", id);
                try
                {
                    var operationResult = _orderLogic.TakeOrderInWork(new OrderBindingModel { Id = id, Count = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Count"].Value), Sum = double.Parse(dataGridView.SelectedRows[0].Cells["Sum"].Value.ToString()), Status = Enum.Parse<OrderStatus>(dataGridView.SelectedRows[0].Cells["Status"].Value.ToString()), CarId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["CarId"].Value), CarName = dataGridView.SelectedRows[0].Cells["CarName"].Value.ToString(), DateCreate = DateTime.Parse(dataGridView.SelectedRows[0].Cells["DateCreate"].Value.ToString()), });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка передачи заказа в работу");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'Готов'", id);
                try
                {
                    var operationResult = _orderLogic.FinishOrder(new OrderBindingModel { Id = id, Count = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Count"].Value), Sum = double.Parse(dataGridView.SelectedRows[0].Cells["Sum"].Value.ToString()), Status = Enum.Parse<OrderStatus>(dataGridView.SelectedRows[0].Cells["Status"].Value.ToString()), CarId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["CarId"].Value), CarName = dataGridView.SelectedRows[0].Cells["CarName"].Value.ToString(), DateCreate = DateTime.Parse(dataGridView.SelectedRows[0].Cells["DateCreate"].Value.ToString()), });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении.Дополнительная информация в логах.");
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка отметки о готовности заказа");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonIssuedOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'Выдан'", id);
                try
                {
                    var operationResult = _orderLogic.DeliveryOrder(new OrderBindingModel { Id = id, Count = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Count"].Value), Sum = double.Parse(dataGridView.SelectedRows[0].Cells["Sum"].Value.ToString()), Status = Enum.Parse<OrderStatus>(dataGridView.SelectedRows[0].Cells["Status"].Value.ToString()), CarId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["CarId"].Value), CarName = dataGridView.SelectedRows[0].Cells["CarName"].Value.ToString(), DateCreate = DateTime.Parse(dataGridView.SelectedRows[0].Cells["DateCreate"].Value.ToString()), });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                    }
                    _logger.LogInformation("Заказ №{id} выдан", id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка отметки о выдачи заказа");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
