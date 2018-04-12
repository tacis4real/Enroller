using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BioEnumerator.CommonUtils;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.FrameworkControls
{
    public partial class UclGridPager : UserControl
    {
        public event Action<object, EventArgs> NavigateGridToNewPage;
        public event Action<object, int> RefreshGridLisPerPage;
        private bool _firstLoad = true;
        public UclGridPager()
        {
            InitializeComponent();
            btnLast.Click += GridViewNavigate;
            btnNext.Click += GridViewNavigate;
            btnPrevious.Click += GridViewNavigate;
            btnFirst.Click += GridViewNavigate;
            ddlRowsPerPage.SelectedIndexChanged += ddlRowsPerPage_SelectedIndexChanged;
            loadDropDown();
            _firstLoad = false;

        }

        void ddlRowsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_firstLoad) { return; }
            if (ddlRowsPerPage.SelectedValue == null) { return; }
            if (int.Parse(ddlRowsPerPage.SelectedValue.ToString()) < 0) { return; }
            if (RefreshGridLisPerPage != null)
            {
                RefreshGridLisPerPage(sender, int.Parse(ddlRowsPerPage.Text.Trim()));
            }
        }

        protected void GridViewNavigate(object sender, EventArgs e)
        {
            if (NavigateGridToNewPage != null)
            {
                NavigateGridToNewPage(sender, e);
            }
        }

        private void loadDropDown()
        {
            try
            {
                ddlRowsPerPage.Items.Clear();
                ddlRowsPerPage.ValueMember = "Id";
                ddlRowsPerPage.DisplayMember = "Name";

                var myList = new List<NameAndValueObject>
                {
                    new NameAndValueObject {Id = 5, Name = "5"},
                    new NameAndValueObject {Id = 10, Name = "10"},
                    new NameAndValueObject {Id = 15, Name = "15"},
                    new NameAndValueObject {Id = 20, Name = "20"},
                    new NameAndValueObject {Id = 30, Name = "30"},
                    new NameAndValueObject {Id = 50, Name = "50"},
                    new NameAndValueObject {Id = 50, Name = "100"}
                };
                ddlRowsPerPage.DataSource = myList;
                ddlRowsPerPage.SelectedIndex = 1;
            }
            catch (Exception)
            {

            }

        }

        public void BindBarItems(PagedDataSource gridView, int recordCount)
        {
            GridPagerNavigator.SetUpNavigationBar(gridView, recordCount, btnFirst, btnPrevious, btnNext, btnLast, lblStart, lblEnd, lblTotal);
        }


    }
}
