using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinApp.Properties
{
    public partial class MyComboBox : ComboBox
    {

        private IList<object> collectionList = null;

        public MyComboBox()
        {
            InitializeComponent();
            collectionList = new List<object>();
        }
        public MyComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        protected override void OnTextUpdate(EventArgs e)
        {
            IList<object> Values = collectionList
                .Where(x => x.ToString().ToLower().Contains(Text.ToLower()))
                .ToList<object>();

            this.Items.Clear();
            if (this.Text != string.Empty)
                this.Items.AddRange(Values.ToArray());
            else
                this.Items.AddRange(collectionList.ToArray());

            this.SelectionStart = this.Text.Length;
            this.DroppedDown = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Text == string.Empty)
            {
                this.Items.Clear();
                this.Items.AddRange(collectionList.ToArray());
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            collectionList = this.Items.OfType<object>().ToList();
        }
    }
}
