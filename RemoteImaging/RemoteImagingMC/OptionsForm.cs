﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using System.Timers;
using System.Runtime.InteropServices;
using Damany.Security.UsersAdmin;
using Damany.RemoteImaging.Common.Forms;

namespace RemoteImaging
{
    public partial class OptionsForm : Form
    {
        private static OptionsForm instance = null;
        private UsersManager userManager;

        public OptionsForm(UsersManager mnger)
        {
            if (mnger == null)
                throw new ArgumentNullException("mnger", "mnger is null.");

            InitializeComponent();

            this.userManager = mnger;
        }


        private void browseForUploadFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.ImageUploadPool = dlg.SelectedPath;
            }

        }

        private void browseForOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.OutputPath = dlg.SelectedPath;
            }
        }

        public IList<Camera> Cameras
        {
            get
            {
                IList<Camera> cams = new List<Camera>();

                foreach (Camera item in camList)
                {
                    cams.Add(item);
                }

                return cams;
            }

            set
            {
                camList.Clear();
                foreach (Camera item in value)
                {
                    camList.Add(item);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void linkLabelConfigCamera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (bs.Current == null)
            {
                return;
            }

            Camera cam = bs.Current as Camera;
            if (string.IsNullOrEmpty(cam.IpAddress))
            {
                return;
            }

            using (FormConfigCamera form = new FormConfigCamera())
            {
                StringBuilder sb = new StringBuilder(form.Text);
                sb.Append("-[");
                sb.Append(cam.IpAddress);
                sb.Append("]");

                form.Navigate(cam.IpAddress);
                form.Text = sb.ToString();
                form.ShowDialog(this);
            }
        }



        private BindingList<Camera> camList =
            new BindingList<Camera>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (usersIsDirty)
            {
                userManager.Save();
            }
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.usesList.DataSource = this.userManager.Users;
            this.usesList.DisplayMember = "Name";

        }


        #region 弹出窗口的操作
        public void ShowResDialog(int picIndex, string msg)
        {
            AlertSettingRes asr = new AlertSettingRes(msg, picIndex);
            asr.HeightMax = 169;
            asr.WidthMax = 175;
            asr.ShowDialog(this);
        }
        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ckbImageAndVideo_CheckedChanged(object sender, EventArgs e)
        {
        }

        bool usersIsDirty = false;
        private void addNewUserButton_Click(object sender, EventArgs e)
        {
            using (var form = new AddNewUserForm())
            {
                DialogResult result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

                if (userManager.UserNameExists(form.UserName))
                {
                    Util.ShowErrorMessage("该用户名已经存在！");
                    return;
                }

                var user = new User(form.UserName, form.PassWord);
                user.Roles.Add("Users");

                this.userManager.AddUser(user);
                this.usersIsDirty = true;
                
            }
        }

        private void deleteSelectedUser_Click(object sender, EventArgs e)
        {
            if ((this.usesList.SelectedItem as User).Roles.Contains("admin"))
            {
                Util.ShowErrorMessage("不能删除'管理员'用户!");
                return;
            }

            userManager.DeleteUser((this.usesList.SelectedItem as User).Name);
            this.usersIsDirty = true;

        }


    }
}