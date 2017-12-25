using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace gridviewPagerExample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(true);
            }
        }

        private void BindData(bool pInit)
        {
            int nowPage = !IsPostBack ? 1 : this.ucPager1.NowPage;
            int pageSize = 10;
            List<Template> list = GetData();

            if (pInit) this.ucPager1.InitPager(list.Count, pageSize);
            list = list.Skip(pageSize * (this.ucPager1.NowPage - 1)).Take(pageSize).ToList();
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }
        //分頁
        protected void PageClick(Object sender, System.EventArgs e)
        {
            this.BindData(false);
        }

        private List<Template> GetData()
        {
            List<Template> list = new List<Template>();
            for (int i = 0; i < 9765; i++)
            {
                list.Add(new Template() { 
                    ID = i.ToString(),
                    Name = string.Format("Name{0}", i.ToString()),
                    Company = "",
                    Address = "",
                    Mobile = ""
                });
            }
            return list;
        }

        public class Template
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
        }
    }
}
