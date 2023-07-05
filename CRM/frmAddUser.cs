﻿/*----------------------------------------------------------------
Copyright (C) 2014 宏图会员管理系统（Grant 巩建春）

项目名称： 宏图会员管理系统
创建者：   grant (巩建春 emaill : nnn987@126.com ; QQ:406333743;Tel:+86 15619212255)
CLR版本：  4.0.30319.42000
时间：     2014/8/28 18:16:22

功能描述：本软件为本人业余时间所写，其所有源码都可以进行免费的学习交流，切勿用于商业用途

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace CRM
{
    public partial class frmAddUser : skinBase
    {
        public DataRow row;
        public frmAddUser()
        {
            InitializeComponent();
            InitUserType();
            //CommStatic.TxtBoxCardId = txtCardId;
            txtUserName.Focus();
        }
        public frmAddUser(DataRow _row)
        {
            InitializeComponent();
            this.row = _row;
            InitUserType();
            cbxUserType.Text = row["UserTypeName"].ToString();

            //txtCardId.Text = row["VipCardId"].ToString();
            //txtCardId.Tag = row;
            //CommStatic.TxtBoxCardId = txtCardId;

            txtTel.Text = row["Tel"] == null ? "" : row["Tel"].ToString();
            txtUserName.Text = row["UserName"] == null ? "" : row["UserName"].ToString();
            txtAddress.Text = row["Address"] == null ? "" : row["Address"].ToString();
            txtRemark.Text = row["Remark"] == null ? "" : row["Remark"].ToString();
            if (row["Sex"].ToString() == "男") radioButtonMan.Checked = true;
            else radioButtonWen.Checked = true;
            if (row["Active"].ToString() == "启用") radioButtonOK.Checked = true;
            else radioButtonStop.Checked = true;
            this.Text = "编辑会员";
        }

        private void pbxAddUserType_Click(object sender, EventArgs e)
        {
            UserTypeAdd typeMgr = new UserTypeAdd(1);
            if (typeMgr.ShowDialog() == DialogResult.OK)
            {
                InitUserType();
            }
        }

        private void InitUserType()
        {
            try
            {
                string sql = "select * from UserType where Sort=@Sort";
                DataSet ds = DBHelper.ExecuteDataSet(sql, new string[] { "@Sort" }, new object[] { 1 });
                if (ds == null || ds.Tables[0].Rows.Count < 1) return;
                DataTable dt = ds.Tables[0];
                DataRow row = dt.NewRow();
                dt.Rows.InsertAt(row, 0);
                cbxUserType.DataSource = dt;
                cbxUserType.DisplayMember = "UserTypeName";
                cbxUserType.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(frmAddUser));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtTel.Text = "";
            radioButtonMan.Checked = false;
            radioButtonWen.Checked = false;
            radioButtonOK.Checked = true;
            cbxUserType.Text = null;
            txtAddress.Text = "";
            txtRemark.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string cardId = txtCardId.Text;
                //if (string.IsNullOrEmpty(cardId))
                //{
                //    MessageBox.Show("卡号不能为空，请在读卡器上刷卡");
                //    return;
                //}
                object o = cbxUserType.SelectedValue;
                if (o == null || o.ToString() == "")
                {
                    MessageBox.Show("请选择会员类型，如果没有合适的类型，可以点击后面的+号，管理类型");
                    return;
                }
                string sql;
                string[] prm;
                object[] obj;
                if (this.row == null)//新增
                {
                    //sql = "select * from card where cardid=@cardid";
                    //prm = new string[] { "@cardid" };
                    //obj = new object[] { cardid };
                    //dataset ds = dbhelper.executedataset(sql, prm, obj);
                    //if (ds == null || ds.tables[0].rows.count < 1)
                    //{
                    //    messagebox.show("请勿使用非法卡，否则可能导致系统风险！");
                    //    return;
                    //}
                    //datarow row = ds.tables[0].rows[0];
                    //string ciphertxt = row["org"].tostring();
                    //string clearttxt = tools.tools.decrypt(ciphertxt);
                    //string[] args = clearttxt.split(",".tochararray(), stringsplitoptions.removeemptyentries);
                    //if (args.length != 4 || args[0] != row["cardid"].tostring())
                    //{
                    //    messagebox.show("此卡数据异常，无法使用，请换其他卡");
                    //    return;
                    //}
                    //datetime time = datetime.parse(row["createtime"].tostring());
                    //string temp = row["cardid"].tostring() + time.tostring("yyyymmdd");
                    //temp = tools.tools.getmd5(temp);
                    //if (temp != row["key"].tostring())
                    //{
                    //    messagebox.show("请勿使用非法卡，否则可能导致系统风险！");
                    //    return;
                    //}
                    //if (row["status"].tostring() != "1")
                    //{
                    //    messagebox.show("此卡已经使用，不能重复使用");
                    //    return;
                    //}
                    TimeSpan ts  = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    double times = ts.TotalSeconds;
                    sql = "Insert into User(ID,VipCardId,UserName,Sex,Tel,CreateTime,Money,Active,Address,Remark,UserType)values(@ID,@VipCardId,@UserName,@Sex,@Tel,@CreateTime,@Moeny,@Active,@Address,@Remark,@UserType)";
                    prm = new string[] { "@ID", "@VipCardId", "@UserName", "@Sex", "@Tel", "@CreateTime", "@Moeny", "@Active", "@Address", "@Remark", "@UserType" };
                    obj = new object[] { Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"), txtUserName.Text, radioButtonMan.Checked ? 1 : 2, txtTel.Text, DateTime.Now, 0, radioButtonOK.Checked ? 1 : 2, txtAddress.Text, txtRemark.Text, o.ToString() };
                }
                else//编辑
                {
                    sql = "Update User Set UserName=@UserName,Sex=@Sex,Tel=@Tel,Active=@Active,Address=@Address,Remark=@Remark,UserType=@UserType where ID=@ID";
                    prm = new string[] { "@UserName", "@Sex", "@Tel", "@Active", "@Address", "@Remark", "@UserType", "@ID" };
                    obj = new object[] { txtUserName.Text, radioButtonMan.Checked ? 1 : 2, txtTel.Text, radioButtonOK.Checked ? 1 : 2, txtAddress.Text, txtRemark.Text, o.ToString(), row["ID"].ToString() };
                }
                if (DBHelper.ExecuteNonQuery(sql, prm, obj) > 0)
                {
                    //if (row == null)//新增的卡要更新卡库的状态
                    //    DBHelper.ExecuteNonQuery("Update Card Set Status=2 where CardID=@CardID", new string[] { "@CardID" }, new object[] { Guid.NewGuid().ToString("N") });
                    MessageBox.Show("保存成功！");
                    CommStatic.TxtBoxCardId = null;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(frmAddUser));
            }

        }

        // n 为生成随机数个数
        private int[] GenerateUniqueRandom(int minValue, int maxValue, int n)
        {
            //如果生成随机数个数大于指定范围的数字总数，则最多只生成该范围内数字总数个随机数
            if (n > maxValue - minValue + 1)
                n = maxValue - minValue + 1;

            int maxIndex = maxValue - minValue + 2;// 索引数组上限
            int[] indexArr = new int[maxIndex];
            for (int i = 0; i < maxIndex; i++)
            {
                indexArr[i] = minValue - 1;
                minValue++;
            }

            Random ran = new Random();
            int[] randNum = new int[n];
            int index;
            for (int j = 0; j < n; j++)
            {
                index = ran.Next(1, maxIndex - 1);// 生成一个随机数作为索引

                //根据索引从索引数组中取一个数保存到随机数数组
                randNum[j] = indexArr[index];

                // 用索引数组中最后一个数取代已被选作随机数的数
                indexArr[index] = indexArr[maxIndex - 1];
                maxIndex--; //索引上限减 1
            }
            return randNum;
        }

        private void frmAddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CommStatic.FrmAddUser = null;
            CommStatic.TxtBoxCardId = null;
            CommStatic.FrmMain.InitSystemInfo();
        }
    }
}
