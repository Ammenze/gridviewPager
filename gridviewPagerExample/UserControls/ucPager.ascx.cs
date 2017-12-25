using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gridviewPagerExample.UserControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        public enum Lang
        {
            english,
            chinese
        }
        Lang _lang = Lang.chinese;
        public Lang Language
        {
            get { return _lang; }
            set { _lang = value; }
        }
        public int PageSize
        {//每頁顯示資料數
            get { return this.TbxPagePer.Text.Trim() != "" ? int.Parse(this.TbxPagePer.Text.Trim()) : 10; }
            set { this.TbxPagePer.Text = value.ToString(); }
        }
        public int NowPage
        {//目前選擇的頁數
            get { return this.hdfNowPage.Value == "" ? 1 : int.Parse(this.hdfNowPage.Value); }
            set { this.hdfNowPage.Value = value.ToString(); }
        }
        public int TotalPage
        {//資料總數除以每頁顯示資料數
            get
            {
                if (this.TotalData == 0)
                {
                    return 0;
                }
                else
                {
                    return TotalData / PageSize + (TotalData % PageSize == 0 ? 0 : 1);
                }
            }
        }
        public int TotalData
        {//資料總數
            get { return this.hdfTotalData.Value == "" ? 0 : int.Parse(this.hdfTotalData.Value); }
            set { this.hdfTotalData.Value = value.ToString(); }
        }
        public int Capacity
        {//顯示的頁數
            get { return this.hdfCapacity.Value == "" ? 5 : int.Parse(this.hdfCapacity.Value); }
            set { this.hdfCapacity.Value = value.ToString(); }
        }
        public int StartPage
        {//顯示的起始頁數
            get { return this.hdfsPage.Value == "" ? 1 : int.Parse(this.hdfsPage.Value); }
            set { this.hdfsPage.Value = value.ToString(); }
        }
        public int EndPage
        {//顯示的結束頁數
            get { return this.hdfePage.Value == "" ? this.Capacity : int.Parse(this.hdfePage.Value); }
            set { this.hdfePage.Value = value.ToString(); }
        }
        public bool ShowPagePer
        {//是否顯示每頁筆數
            set { this.divTbxPagePer.Visible = value; }
        }
        public bool ShowWarning
        {//是否顯示每頁筆數
            set { this.LblWarning.Visible = value; }
        }
        public string MaxPageSize
        {//是否顯示每頁筆數
            set { this.hdfMaxPageSize.Value = value; }
        }
        public event EventClickEventHandler PageClick;
        public delegate void EventClickEventHandler(object sender, System.EventArgs e);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (Language == Lang.english)
                {
                    this.LnkPrev.Text = "Previous";
                    this.LnkNxt.Text = "Next";
                }
                if (Language == Lang.chinese)
                {
                    this.LnkPrev.Text = "上一頁";
                    this.LnkNxt.Text = "下一頁";
                }
                this.ShowPager();
                this.TbxPagePer.Text = this.PageSize.ToString();
                this.hdfMaxPageSize.Value = "100";
            }
        }

        public void InitPager(int pTotalData, int pPageSize)
        {
            this.TotalData = pTotalData;
            this.PageSize = pPageSize;
            this.SetupDDLPage();
            this.SetupPager();
        }

        protected void SetupPager()
        {
            int ttlpage = this.TotalPage;
            this.GetPageRange();
            List<GvwPage> pagelist = new List<GvwPage>();

            if (this.StartPage != 1) pagelist.Add(new GvwPage("...", (this.StartPage - 1).ToString(), false));
            for (int i = 1; i <= ttlpage; i++)
            {
                if (i >= this.StartPage && i <= this.EndPage)
                    pagelist.Add(new GvwPage(i.ToString(), i.ToString(), (i == this.NowPage) ? true : false));
            }
            if (this.EndPage != this.TotalPage && this.NowPage != this.TotalPage) pagelist.Add(new GvwPage("...", (this.EndPage + 1).ToString(), false));

            this.DataList1.DataSource = pagelist;
            this.DataList1.DataBind();
            this.DdlPage.SelectedValue = this.NowPage.ToString();
            this.LnkNxt.Visible = (this.NowPage == ttlpage) ? false : true;
            this.LnkPrev.Visible = (this.NowPage == 1) ? false : true;
            this.LnkNxt.CommandArgument = (this.NowPage + 1).ToString();
            this.LnkPrev.CommandArgument = (this.NowPage - 1).ToString();
            this.ShowPager();
            this.LblTotalAmount.Text = this.TotalData.ToString();
        }

        protected void ShowPager()
        {
            this.Visible = this.TotalData == 0 ? false : this.TotalPage == 1 ? this.divTbxPagePer.Visible : true;
            this.divPager.Visible = this.TotalPage > 1 ? true : this.divTbxPagePer.Visible ? false : true;
        }

        protected void SetupDDLPage()
        {
            this.DdlPage.Items.Clear();
            int ttlpage = this.TotalPage;
            for (int i = 1; i <= ttlpage; i++)
                this.DdlPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            this.divDdlPage.Visible = this.Capacity > ttlpage ? false : true;
            this.LblTotalPage.Text = this.TotalPage.ToString();
        }

        protected void GetPageRange()//int pRange)
        {
            //int cnt = pRange;
            int cut = this.Capacity / 2;
            int mod = this.Capacity % 2 == 1 ? 0 : 1;
            int sp = this.NowPage - cut + mod;
            int ep = this.NowPage + cut;
            int n = 1 - sp;
            ep += n > 0 && n <= cut ? 1 - sp : 0;
            sp -= ep - this.TotalPage > 0 ? ep - this.TotalPage : 0;
            this.hdfsPage.Value = (sp <= 0 ? 1 : sp).ToString();
            this.hdfePage.Value = (ep > this.TotalPage ? this.TotalPage : ep).ToString();
        }

        protected void LnkNumber_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            if (lnk != null)
            {
                this.NowPage = int.Parse(lnk.CommandArgument);
                this.SetupPager();
                if (PageClick != null) PageClick(sender, e);
            }
        }

        protected void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NowPage = int.Parse(this.DdlPage.SelectedValue);
            this.SetupPager();
            if (PageClick != null) PageClick(sender, e);
        }

        protected void TbxPagePer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(this.TbxPagePer.Text.Trim());
                if (i > int.Parse(this.hdfMaxPageSize.Value)) this.TbxPagePer.Text = this.hdfMaxPageSize.Value;
                this.SetupDDLPage();
                if (this.DdlPage.Items.FindByValue(this.NowPage.ToString()) != null)
                    this.DdlPage.SelectedValue = this.NowPage.ToString();
                else
                    this.NowPage = 1;
                this.SetupPager();
                if (PageClick != null) PageClick(sender, e);
            }
            catch
            {

            }
        }

        private class GvwPage
        {
            public GvwPage(string x, string p, bool s)
            {
                _p = p;
                _selected = s;
                _txt = x;
            }
            string _p;
            public string page
            {
                get { return _p; }
            }
            string _txt;
            public string pText
            {
                get { return _txt; }
            }
            bool _selected;
            public bool ShowLabel
            {
                get { return _selected; }
            }
            public bool ShowLink
            {
                get
                {
                    if (_selected)
                        return false;
                    else
                        return true;
                }
            }
        }
    }
}