using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace LDmp
{
    public partial class SecStep : Form
    {
        string csrf = "";
        string json = "";
        string cptImage = "";
        string login = null;
        HttpRequest request = null;

        public SecStep(HttpRequest request, string login, string json)
        {
            InitializeComponent();
            this.login = login;
            this.request = request;
            this.request.Referer = "https://auth.mail.ru/cgi-bin/secstep";
            this.json = json;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSmsCode.Text != "")
            {
                try
                {
                    request.AllowAutoRedirect = true;
                    request.AddParam("csrf", csrf);
                    request.AddParam("Login", login);
                    request.AddParam("AuthCode", txtSmsCode.Text);
                    request.AddParam("Permanent", 1);
                    if (cptImage != "") request.AddParam("Captcha", txtCpt.Text);
                    request.Post("https://auth.mail.ru/cgi-bin/secstep");

                    //string loc = usr.request.Response.Location;
                    //usr.request.Get(loc);

                    //MessageBox.Show(usr.request.Cookies.ToString());
                    //string secstep = "";
                    //if (usr.request.Cookies.TryGetValue("secstep", out secstep))
                    //{
                    //    string loc = usr.request.Response.Location;
                    //    usr.request.Get("https://auth.mail.ru/cgi-bin/auth?Login=" + usr.login + "&token=" + secstep + "&Page=https%3A%2F%2Fwf.mail.ru%2F");
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.Close();
            }
        }

        async public void GetCaptcha()
        {
            if (cptImage != "")
            {
                try
                {
                    HttpResponse resp = null;

                    await Task.Run(() =>
                    {
                        //https://c.mail.ru/c/1?0.7735242388132923
                        resp = request.Get(cptImage + @"?0.7735242388132923");
                    });
                    if (resp == null)
                    {
                        MessageBox.Show("Ошибка получения запроса");
                        throw new Exception("error getting image");
                    }
                    byte[] imageByte = resp.ToBytes();
                    using (MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length))
                    {
                        ms.Write(imageByte, 0, imageByte.Length);
                        pbCpt.Image = Image.FromStream(ms, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SecStep_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            try
            {
                JObject rss = JObject.Parse(json);

                //string itemTitle = (string)rss["body"]["signs"][0]["name"];

                JToken jt = rss.SelectToken("secstep_phone");
                if (jt != null)
                    txtHeader.Text += "Телефон на который пришла смс: " + jt.ToString() + Environment.NewLine;

                jt = rss.SelectToken("secstep_login");
                if (jt != null)
                    txtHeader.Text += "Mail: " + jt.ToString() + Environment.NewLine;

                jt = rss.SelectToken("device");
                if (jt != null)
                    txtHeader.Text += "Device: " + jt.ToString() + Environment.NewLine;

                jt = rss.SelectToken("csrf");
                if (jt != null)
                    csrf = jt.ToString();
                else
                    this.Close();

                jt = rss.SelectToken("secstep_captcha");
                if (jt != null)
                    cptImage = jt.ToString();

                if (cptImage == "")
                {
                    txtCpt.Visible = lblCpt.Visible = pbCpt.Visible = btnGetCpt.Visible = false;
                }
                else
                    GetCaptcha();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSmsCode_TextChanged(object sender, EventArgs e)
        {
            if (cptImage == "")
            {
                if (txtSmsCode.Text == "")
                    btnSend.Enabled = false;
                else
                    btnSend.Enabled = true;
            }
            else
            {
                if (txtSmsCode.Text == "" || txtCpt.Text == "")
                    btnSend.Enabled = false;
                else
                    btnSend.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetCpt_Click(object sender, EventArgs e)
        {
            GetCaptcha();
        }

        private void txtCpt_TextChanged(object sender, EventArgs e)
        {
            if (cptImage != "")
            {
                if (txtSmsCode.Text == "")
                    btnSend.Enabled = false;
                else
                {
                    if (txtCpt.Text != "")
                        btnSend.Enabled = true;
                    else
                        btnSend.Enabled = false;
                }
            }
        }
    }
}
