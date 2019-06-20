using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinApp
{
    public partial class Form1 : Form
    {
        private AutoCompleteStringCollection data = null;
        DataTable AllNames = new DataTable();
        string strFindStr = "";
        ListBox suggestList = new ListBox();

        //fill it up and leave it untouched!
        public Form1()
        {
            InitializeComponent();
            comboBox1.Parent.Controls.Add(suggestList);
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxCallCode_KeyPress);
        }

        private void cmbxCallCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.AutoComplete(sender as ComboBox, e, false);
        }

        public void AutoComplete(ComboBox cb, KeyPressEventArgs e, bool blnLimitToList)
        {
            suggestList.Items.Clear();
            try
            {
                if (e.KeyChar == (char)8)
                {
                    if (cb.SelectionStart <= 1)
                    {
                        cb.Text = "";
                        return;
                    }

                    if (cb.SelectionLength == 0)
                        strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                    else
                        strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
                }
                else
                {
                    if (cb.SelectionLength == 0)
                        strFindStr = cb.Text + e.KeyChar;
                    else
                        strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
                }

                int intIdx = -1;

                // Search the string in the ComboBox list.
                intIdx = cb.FindString(strFindStr);
                if (intIdx == -1)
                {
                    
                    //
                    ArrayList suggestItems = new ArrayList();
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        if (cb.Items[i].ToString().Contains(strFindStr))
                        {
                            suggestItems.Add(cb.Items[i].ToString());
                            suggestList.Items.Add(cb.Items[i].ToString());
                            //intIdx = i;
                            //break;
                        }
                    }
                   
                }
                if (intIdx != -1)
                {
                    cb.SelectedIndex = intIdx;
                    cb.SelectedText = "";
                    cb.SelectionStart = strFindStr.Length;
                    cb.SelectionLength = cb.Text.Length;
                    e.Handled = true;
                }
                else
                {
                    
                    //suggestList.Visible = true;
                    //suggestList.BringToFront();
                    //suggestList.Focus();
                    e.Handled = blnLimitToList;
                }
            }
            catch { }

            //string name = string.Format("{0}{1}", comboBox1.Text, e.KeyChar.ToString()); //join previous text and new pressed char
            //DataRow[] rows = comboBox1.Items.Select(string.Format("FieldName LIKE '%{0}%'", name));
            //DataTable filteredTable = AllNames.Clone();
            //foreach (DataRow r in rows)
            //    filteredTable.ImportRow(r);
            //comboBox1.DataSource = null;
            //comboBox1.DataSource = filteredTable.DefaultView;
            //comboBox1.DisplayMember = "FieldName";
        }
    }
}
