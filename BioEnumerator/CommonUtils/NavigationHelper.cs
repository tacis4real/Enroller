using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;

namespace BioEnumerator.CommonUtils
{
    public class GridPagerNavigator
    {
        public static void SetUpNavigationBar(PagedDataSource gvGeneric, int totalRowCount, Button btnFirst, Button btnPrevious, Button btnNext, Button btnLast, Label lblStartIndex, Label lblEndIndex, Label lblTotalCount)
        {
            int startIndex;
            int endIndex;
            SetStartEndIndex(gvGeneric, totalRowCount, out startIndex, out endIndex);

            lblStartIndex.Text = startIndex.ToString(CultureInfo.InvariantCulture);
            lblEndIndex.Text = endIndex.ToString(CultureInfo.InvariantCulture);
            lblTotalCount.Text = totalRowCount.ToString(CultureInfo.InvariantCulture);

            if (startIndex == 1)
            {
                if (endIndex < totalRowCount) // at the first page, can move to next one
                {
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                else // no paging at all
                {
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
            }
            else if (endIndex == totalRowCount)
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
        }
        public static void PreparePaging(PagedDataSource gvGeneric, int totalRowCount, int rowsPerPage)
        {
            var pageSize = rowsPerPage > 0 ? rowsPerPage : totalRowCount;
            gvGeneric.PageSize = pageSize;
        }
        public static void PreparePageIndex(PagedDataSource gvGeneric, int commandArg)
        {
            switch (commandArg)
            {
                case 3:
                    gvGeneric.CurrentPageIndex++;
                    break;
                case 4:
                    gvGeneric.CurrentPageIndex = gvGeneric.PageCount - 1;
                    break;
                case 2:
                    var newInt = gvGeneric.CurrentPageIndex - 1;
                    gvGeneric.CurrentPageIndex = Math.Max(0, newInt);
                    break;
                case 1:
                    gvGeneric.CurrentPageIndex = 0;
                    break;
            }
        }
        private static void SetStartEndIndex(PagedDataSource gridView, int totalRowCount, out int startIndex, out int endIndex)
        {
            startIndex = gridView.CurrentPageIndex * gridView.PageSize + 1;
            endIndex = Math.Min(totalRowCount, (gridView.CurrentPageIndex + 1) * gridView.PageSize);
        }
        public static void SetUpNavigationBar(PagedDataSource gvGeneric, int totalRowCount, Button btnPrevious, Button btnNext)
        {
            int startIndex;
            int endIndex;
            SetStartEndIndex(gvGeneric, totalRowCount, out startIndex, out endIndex);

            if (startIndex == 1)
            {
                if (endIndex < totalRowCount) // at the first page, can move to next one
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;

                }
                else // no paging at all
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                }
            }
            else if (endIndex == totalRowCount)
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;

            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;

            }
        }
    }
    public class NavigationHelper
    {
        /// <summary>
        /// Methods help navigate records in the grid
        /// </summary>
        /// <copyright>Copyright © xPlug Technologies Limited 2013. All Rights Reserved</copyright>
        /// <Author>OLUWASEYI ADEGBOYEGA | sadegboyega@xplugng.com | www.xplugng.com </Author>
        /// <History>Date Created: 20-09-2013</History>

        public static void SetUpNavigationBar(PagedDataSource gvGeneric, int totalRowCount, Button btnPrevious, Button btnNext)
        {
            int startIndex;
            int endIndex;
            SetStartEndIndex(gvGeneric, totalRowCount, out startIndex, out endIndex);

            if (startIndex == 1)
            {
                if (endIndex < totalRowCount) // at the first page, can move to next one
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;

                }
                else // no paging at all
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                }
            }
            else if (endIndex == totalRowCount)
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;

            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;

            }
        }


        public static void PreparePaging(PagedDataSource gvGeneric, int totalRowCount, int rowsPerPage)
        {
            var pageSize = rowsPerPage > 0 ? rowsPerPage : totalRowCount;
            gvGeneric.PageSize = pageSize;
        }
        public static void PreparePageIndex(PagedDataSource gvGeneric, int commandArg)
        {
            switch (commandArg)
            {
                case 3: //"imgNext"
                    gvGeneric.CurrentPageIndex++;
                    break;
                case 4: //"imgLast"
                    gvGeneric.CurrentPageIndex = gvGeneric.PageCount - 1;
                    break;
                case 2: //"imgPrevious"
                    var newInt = gvGeneric.CurrentPageIndex - 1;
                    gvGeneric.CurrentPageIndex = Math.Max(0, newInt);
                    break;
                case 1: //"imgFirst"
                    gvGeneric.CurrentPageIndex = 0;
                    break;
            }
        }
        private static void SetStartEndIndex(PagedDataSource gridView, int totalRowCount, out int startIndex, out int endIndex)
        {
            startIndex = gridView.CurrentPageIndex * gridView.PageSize + 1;
            endIndex = Math.Min(totalRowCount, (gridView.CurrentPageIndex + 1) * gridView.PageSize);
        }

    }
}
