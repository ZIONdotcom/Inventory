﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        class NumberFormatException : Exception
        {
            public NumberFormatException(string number) : base(number) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string letter) : base(letter) { }  
        }

        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string currency) : base(currency) { }
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = { "Beverages, ", "Bread/Bakery, ", "Canned/Jared Goods, ", "Dairy, ", "Frozen Goods, ", "Meat, ", "Personal Care, ", "Other" };
            foreach (string Category in ListOfProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }

        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;

        private int _Quantity;
        private double _SellPrice;

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    throw new StringFormatException(name);
            }
            catch (StringFormatException eme){
                MessageBox.Show("String Format only" + eme.Message);
            }
            finally { Console.WriteLine("Always enter proper format!"); }
            return name;
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                    throw new NumberFormatException(qty);
                    
            }
            catch (NumberFormatException kimi) {
                MessageBox.Show("Numbers only! " +  kimi.Message);  
            }
            finally { Console.WriteLine("Always enter proper format!"); }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            try {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                    throw new CurrencyFormatException(price);
            }
            catch (CurrencyFormatException imi)
            {
                MessageBox.Show("Enter currency format only!" + imi.Message);
            }
            finally { Console.WriteLine("Always enter proper format!"); }

            return Convert.ToDouble(price);
        }

        BindingSource showProductList = new BindingSource();

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description)); gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; gridViewProductList.DataSource = showProductList;

        }
    }
}
