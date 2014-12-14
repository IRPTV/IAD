using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using IAD.MyDBTableAdapters;
using System.IO;

namespace IAD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Load Countries          
            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Contry_Dt = Ta.Select_All_Countries();
            for (int i = 0; i < Contry_Dt.Rows.Count; i++)
            {
                NewListItem Lst = new NewListItem();
                Lst.Text = Contry_Dt[i]["Title"].ToString();
                Lst.Value = Contry_Dt[i]["Id"].ToString();
                cmbCountrySearch.Items.Add(Lst);
                cmbCountryMaster.Items.Add(Lst);
                cmbCountryMaster2.Items.Add(Lst);
                cmbCountryMaster.SelectedIndex = 0;
                cmbCountryMaster2.SelectedIndex = 0;
                cmbCountrySearch.SelectedIndex = 0;

            }
            cmbCountryMaster.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCountryMaster.AutoCompleteSource = AutoCompleteSource.ListItems;


            cmbCountryMaster2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCountryMaster2.AutoCompleteSource = AutoCompleteSource.ListItems;


            cmbCountrySearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCountrySearch.AutoCompleteSource = AutoCompleteSource.ListItems;


            //Fill Companies
            MyDB.IADDataTable Dt = Ta.Select_All_Companies();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                NewListItem Lst = new NewListItem();
                Lst.Text = Dt[i]["Title"].ToString();
                Lst.Value = Dt[i]["Id"].ToString();
                cmbCompany.Items.Add(Lst);
                cmbCompanies2.Items.Add(Lst);
                comboBox1.Items.Add(Lst);
            }
            if (cmbCompany.Items.Count > 0)
            {
                cmbCompany.SelectedIndex = 0;
                cmbCompanies2.SelectedIndex = 0;
                comboBox1.SelectedIndex = 0;
            }
            cmbCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCompany.AutoCompleteSource = AutoCompleteSource.ListItems;

            //cmbCompanies2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmbCompanies2.AutoCompleteSource = AutoCompleteSource.ListItems;



            //Load Contract Types
            MyDB.IADDataTable Contracts_Dt = Ta.Select_All_Contract_Types();
            for (int i = 0; i < Contracts_Dt.Rows.Count; i++)
            {
                NewListItem Lst = new NewListItem();
                Lst.Text = Contracts_Dt[i]["Title"].ToString();
                Lst.Value = Contracts_Dt[i]["Id"].ToString();
                cmbContractMaster.Items.Add(Lst);
                cmbContractSearch.Items.Add(Lst);
                cmbContractMaster.SelectedIndex = 0;
                cmbContractSearch.SelectedIndex = 0;

            }


            //Set Status

            NewListItem StatActive_Lst = new NewListItem();
            StatActive_Lst.Text = "Active";
            StatActive_Lst.Value = "True";

            cmbStatusMaster.Items.Add(StatActive_Lst);
            cmbStatusSearch.Items.Add(StatActive_Lst);
            cmbStatusMaster.SelectedIndex = 0;
            cmbStatusSearch.SelectedIndex = 0;

            NewListItem StatCancel_Lst = new NewListItem();
            StatCancel_Lst.Text = "Not Active";
            StatCancel_Lst.Value = "False";

            cmbStatusMaster.Items.Add(StatCancel_Lst);
            cmbStatusSearch.Items.Add(StatCancel_Lst);

        }

        private void BtnAddCompany_Click(object sender, EventArgs e)
        {
            NewListItem Lst1 = (NewListItem)cmbCountryMaster2.SelectedItem;
            IADTableAdapter Ta = new IADTableAdapter();
            int? RetId = 0;
            Ta.Insert_Company(ref RetId,
                txtCompanyTitle.Text,
                txtCompanyWeb.Text,
                txtCompanyPostal.Text,
                txtCompanyDesc.Text,
                txtCompanyRemarks.Text,
                int.Parse(Lst1.Value.ToString())
                );
            FillCompaniesGrid();

            txtCompanyTitle.Text = "";
            txtCompanyDesc.Text = "";
            txtCompanyWeb.Text = "";
            txtCompanyPostal.Text = "";
            txtCompanyRemarks.Text = "";

            MyDB.IADDataTable Dt = Ta.Select_All_Companies();
            cmbCompanies2.Items.Clear();
            cmbCompany.Items.Clear();
            comboBox1.Items.Clear();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                NewListItem Lst = new NewListItem();
                Lst.Text = Dt[i]["Title"].ToString();
                Lst.Value = Dt[i]["Id"].ToString();
                cmbCompany.Items.Add(Lst);
                cmbCompanies2.Items.Add(Lst);
                comboBox1.Items.Add(Lst);
            }
            if (cmbCompany.Items.Count > 0)
            {
                cmbCompany.SelectedIndex = 0;
                cmbCompanies2.SelectedIndex = 0;
                comboBox1.SelectedIndex = 0;
            }

        }
        protected void FillCompaniesGrid()
        {
            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Dt = Ta.Select_All_Companies();

            DataTable DTable = new DataTable();

            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("Title");
            DataColumn col3 = new DataColumn("Description");
            DataColumn col4 = new DataColumn("WebSite");
            DataColumn col5 = new DataColumn("Postal");
            DataColumn col6 = new DataColumn("Remarks");
            DataColumn col7 = new DataColumn("Country");

            DTable.Columns.Add(col1);
            DTable.Columns.Add(col2);
            DTable.Columns.Add(col3);
            DTable.Columns.Add(col4);
            DTable.Columns.Add(col5);
            DTable.Columns.Add(col6);
            DTable.Columns.Add(col7);


            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                MyDB.IADDataTable DtCountry = Ta.Country_ById(int.Parse(Dt[i]["CONCODE"].ToString()));

                DataRow row = DTable.NewRow();
                row[col1] = Dt[i]["ID"].ToString();
                row[col2] = Dt[i]["Title"].ToString();
                row[col3] = Dt[i]["DESCIRPTION"].ToString();
                row[col4] = Dt[i]["WEB"].ToString();
                row[col5] = Dt[i]["POSTAL"].ToString();
                row[col6] = Dt[i]["REMARKS"].ToString();
                row[col7] = DtCountry[0]["TITLE"].ToString();
                DTable.Rows.Add(row);
            }

            dgvCompaniesList.DataSource = DTable;
            dgvCompaniesList.Columns[0].Width = 40;
            dgvCompaniesList.Columns[1].Width = 200;
            dgvCompaniesList.Columns[2].Width = 60;


            toolStripButton14.Visible = false;
            dgvCompaniesList.ClearSelection();

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            FillCompaniesGrid();
        }

        private void dgvCompaniesList_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCompaniesList.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt =
                    Ta.Select_Company_ById(int.Parse(dgvCompaniesList.SelectedRows[0].Cells[0].Value.ToString()));
                txtCompanyTitle.Text = Dt[0]["Title"].ToString();
                txtCompanyDesc.Text = Dt[0]["DESCIRPTION"].ToString();
                txtCompanyWeb.Text = Dt[0]["WEB"].ToString();
                txtCompanyPostal.Text = Dt[0]["POSTAL"].ToString();
                txtCompanyRemarks.Text = Dt[0]["REMARKS"].ToString();

                for (int i = 0; i < cmbCountryMaster2.Items.Count; i++)
                {
                    NewListItem Lst = (NewListItem)cmbCountryMaster2.Items[i];

                    if (Lst.Value.ToString() == Dt[0]["CONCODE"].ToString())
                    {
                        cmbCountryMaster2.SelectedIndex = i;
                    }
                }
                toolStripButton14.Visible = true;
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (dgvCompaniesList.SelectedRows.Count == 1)
            {
                NewListItem Lst1 = (NewListItem)cmbCountryMaster2.SelectedItem;

                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt = Ta.Update_Company(int.Parse(dgvCompaniesList.SelectedRows[0].Cells[0].Value.ToString()),
                    txtCompanyTitle.Text,
                    txtCompanyWeb.Text,
                  txtCompanyPostal.Text,
                  txtCompanyDesc.Text,
                  txtCompanyRemarks.Text,
                   int.Parse(Lst1.Value.ToString()));

                FillCompaniesGrid();

                txtCompanyTitle.Text = "";
                txtCompanyDesc.Text = "";
                txtCompanyWeb.Text = "";
                txtCompanyPostal.Text = "";
                txtCompanyRemarks.Text = "";
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            DialogResult Rs = MessageBox.Show("Are you sure to delete selected company?", "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rs == System.Windows.Forms.DialogResult.Yes)
            {
                if (dgvCompaniesList.SelectedRows.Count == 1)
                {
                    IADTableAdapter Ta = new IADTableAdapter();
                    MyDB.IADDataTable Dt = Ta.Delete_CompanyById(int.Parse(dgvCompaniesList.SelectedRows[0].Cells[0].Value.ToString()));
                    FillCompaniesGrid();
                    txtCompanyTitle.Text = "";
                    txtCompanyDesc.Text = "";
                    txtCompanyWeb.Text = "";
                    txtCompanyPostal.Text = "";
                    txtCompanyRemarks.Text = "";
                }
            }

        }

        private void cmbCompanies2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCompanyContactGrid();
        }
        protected void FillCompanyContactGrid()
        {
            NewListItem Lst = (NewListItem)cmbCompanies2.SelectedItem;
            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Dt = Ta.Select_Company_Contact_ByField("Comp_Id", Lst.Value.ToString());


            DataTable DTable = new DataTable();

            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("Name");
            DataColumn col3 = new DataColumn("Title");
            DataColumn col4 = new DataColumn("fax");
            DataColumn col5 = new DataColumn("Mobile");
            DataColumn col6 = new DataColumn("Email");
            DataColumn col7 = new DataColumn("tell");

            DTable.Columns.Add(col1);
            DTable.Columns.Add(col2);
            DTable.Columns.Add(col3);
            DTable.Columns.Add(col4);
            DTable.Columns.Add(col5);
            DTable.Columns.Add(col6);
            DTable.Columns.Add(col7);


            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = DTable.NewRow();
                row[col1] = Dt[i]["ID"].ToString();
                row[col2] = Dt[i]["Name"].ToString();
                row[col3] = Dt[i]["Title"].ToString();
                row[col4] = Dt[i]["fax"].ToString();
                row[col5] = Dt[i]["Mobile"].ToString();
                row[col6] = Dt[i]["Email"].ToString();
                row[col7] = Dt[i]["tell"].ToString();
                DTable.Rows.Add(row);
            }

            dgvCompContact.DataSource = DTable;
            dgvCompContact.Columns[0].Width = 40;
            dgvCompContact.Columns[1].Width = 200;
            dgvCompContact.Columns[2].Width = 60;


            btnContactCompanyUpdate.Visible = false;
            dgvCompContact.ClearSelection();
        }

        private void btnContactCompanyAdd_Click(object sender, EventArgs e)
        {

            NewListItem Lst = (NewListItem)cmbCompanies2.SelectedItem;


            IADTableAdapter Ta = new IADTableAdapter();
            int? RetVal = 0;
            MyDB.IADDataTable Dt = Ta.Insert_Company_Contact(ref RetVal,
                txtCompContactName.Text,
                txtCompContactTitle.Text,
                txtCompContactTell.Text,
                txtCompContactFax.Text,
                txtCompContactMobile.Text,
                txtCompContactEmail.Text,
                  int.Parse(Lst.Value.ToString()));

            txtCompContactName.Text = "";
            txtCompContactTitle.Text = "";
            txtCompContactTell.Text = "";
            txtCompContactFax.Text = "";
            txtCompContactMobile.Text = "";
            txtCompContactEmail.Text = "";
            FillCompanyContactGrid();


        }

        private void btnContactCompanySelectAll_Click(object sender, EventArgs e)
        {
            FillCompanyContactGrid();
        }

        private void btnContactCompanyUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCompContact.SelectedRows.Count == 1)
            {
                NewListItem Lst = (NewListItem)cmbCompanies2.SelectedItem;

                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt =
                    Ta.Update_Comp_Contact(int.Parse(dgvCompContact.SelectedRows[0].Cells[0].Value.ToString()),
                    txtCompContactName.Text,
                    txtCompContactTitle.Text,
                    txtCompContactTell.Text,
                    txtCompContactFax.Text,
                    txtCompContactMobile.Text,
                    txtCompContactEmail.Text,
                    int.Parse(Lst.Value.ToString()));

                txtCompContactName.Text = "";
                txtCompContactTitle.Text = "";
                txtCompContactTell.Text = "";
                txtCompContactFax.Text = "";
                txtCompContactMobile.Text = "";
                txtCompContactEmail.Text = "";
                FillCompanyContactGrid();

            }
        }

        private void dgvCompContact_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCompContact.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt =
                    Ta.Select_Comp_Contact_ById(int.Parse(dgvCompContact.SelectedRows[0].Cells[0].Value.ToString()));
                txtCompContactName.Text = Dt[0]["name"].ToString();
                txtCompContactTell.Text = Dt[0]["tell"].ToString();
                txtCompContactTitle.Text = Dt[0]["title"].ToString();
                txtCompContactMobile.Text = Dt[0]["mobile"].ToString();
                txtCompContactFax.Text = Dt[0]["FAX"].ToString();
                txtCompContactEmail.Text = Dt[0]["EMAIL"].ToString();

                btnContactCompanyUpdate.Visible = true;
            }
        }

        private void btnContactCompanyDelete_Click(object sender, EventArgs e)
        {
            DialogResult Rs = MessageBox.Show("Are you sure to delete selected contact?", "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rs == System.Windows.Forms.DialogResult.Yes)
            {
                if (dgvCompContact.SelectedRows.Count == 1)
                {
                    IADTableAdapter Ta = new IADTableAdapter();
                    MyDB.IADDataTable Dt =
                        Ta.Delete_Comp_Contact_ByID(int.Parse(dgvCompContact.SelectedRows[0].Cells[0].Value.ToString()));
                    txtCompContactName.Text = "";
                    txtCompContactTitle.Text = "";
                    txtCompContactTell.Text = "";
                    txtCompContactFax.Text = "";
                    txtCompContactMobile.Text = "";
                    txtCompContactEmail.Text = "";
                    FillCompanyContactGrid();
                }
            }
        }

        private void btnSearchSelect_Click(object sender, EventArgs e)
        {
            //Search Master 
            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Dt = Ta.Select_All_Master();


            DataTable DTable = new DataTable();

            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("EFFECTIVE_DATE");
            DataColumn col3 = new DataColumn("EXPIRY_DATE");
            DataColumn col4 = new DataColumn("DESCRIPTION");
            DataColumn col5 = new DataColumn("COST");
            DataColumn col6 = new DataColumn("ACTIVE?");

            DTable.Columns.Add(col1);
            DTable.Columns.Add(col2);
            DTable.Columns.Add(col3);
            DTable.Columns.Add(col4);
            DTable.Columns.Add(col5);
            DTable.Columns.Add(col6);


            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = DTable.NewRow();
                row[col1] = Dt[i]["ID"].ToString();
                row[col2] = DateTime.Parse(Dt[i]["EFFECTIVE_DATE"].ToString()).ToShortDateString();
                row[col3] = DateTime.Parse(Dt[i]["EXPIRY_DATE"].ToString()).ToShortDateString();
                row[col4] = Dt[i]["DESCRIPTION"].ToString();
                row[col5] = Dt[i]["COST"].ToString();
                row[col6] = Dt[i]["STATUS"].ToString().Replace("True", "Active").Replace("False", "Not Active");
                DTable.Rows.Add(row);
            }

            dgvSearchResult.DataSource = DTable;
            dgvSearchResult.Columns[0].Width = 40;
            dgvSearchResult.Columns[1].Width = 100;
            dgvSearchResult.Columns[2].Width = 100;
            dgvSearchResult.Columns[3].Width = 150;





        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //Add Master 
                IADTableAdapter Ta = new IADTableAdapter();
                int? X = 0;
                string CostKind = "---";
                if (checkBox1.Checked)
                {
                    CostKind = "Monthly";
                }
                Ta.Insert_Master(ref X, int.Parse(((NewListItem)cmbCountryMaster.SelectedItem).Value.ToString()),
                   ((NewListItem)cmbCompany.SelectedItem).Value.ToString(),
                   txtMasterDesc.Text,
                   int.Parse(((NewListItem)cmbContractMaster.SelectedItem).Value.ToString()),
                   dateTimePicker3.Value, dateTimePicker1.Value,
                   txtMasterCost.Text,
                    bool.Parse(((NewListItem)cmbStatusMaster.SelectedItem).Value.ToString()),
                    txtMasterRemarks.Text,
                    txtMasterRenewal.Text,
                    CostKind,
                    txtMasterCostTerms.Text);

                MessageBox.Show("Data inserted");

                txtMasterRenewal.Text = "";
                txtMasterCost.Text = "";
                txtMasterCostTerms.Text = "";
                txtMasterDesc.Text = "";
                txtMasterRemarks.Text = "";
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.Message);
                throw;
            }







        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                //Update Master
                IADTableAdapter Ta = new IADTableAdapter();
                string CostKind = "---";
                if (checkBox1.Checked)
                {
                    CostKind = "Monthly";
                }
                Ta.Update_Master(int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString()),
                    int.Parse(((NewListItem)cmbCountryMaster.SelectedItem).Value.ToString()),
                   ((NewListItem)cmbCompany.SelectedItem).Value.ToString(),
                   txtMasterDesc.Text,
                   int.Parse(((NewListItem)cmbContractMaster.SelectedItem).Value.ToString()),
                   dateTimePicker3.Value, dateTimePicker1.Value,
                   txtMasterCost.Text,
                    bool.Parse(((NewListItem)cmbStatusMaster.SelectedItem).Value.ToString()),
                    txtMasterRemarks.Text,
                    txtMasterRenewal.Text,
                    CostKind,
                    txtMasterCostTerms.Text);

                MessageBox.Show("Data Updated");
            }

        }

        private void dgvSearchResult_DoubleClick(object sender, EventArgs e)
        {
            try
            {


                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt =
                    Ta.Select_Current_Master(int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString()));
                if (Dt.Rows.Count == 1)
                {

                    txtMasterRenewal.Text = Dt[0]["RENEWAL"].ToString();
                    txtMasterCost.Text = Dt[0]["COST"].ToString();
                    txtMasterCostTerms.Text = Dt[0]["COSTTERMS"].ToString();
                    txtMasterDesc.Text = Dt[0]["DESCRIPTION"].ToString();
                    txtMasterRemarks.Text = Dt[0]["REMARKS"].ToString();

                    if (Dt[0]["COSTType"].ToString() == "Monthly")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }

                    dateTimePicker3.Value = DateTime.Parse(Dt[0]["EFFECTIVE_DATE"].ToString());
                    dateTimePicker1.Value = DateTime.Parse(Dt[0]["EXPIRY_DATE"].ToString());

                    for (int i = 0; i < cmbCompany.Items.Count; i++)
                    {
                        NewListItem Lst = (NewListItem)cmbCompany.Items[i];

                        if (Lst.Value.ToString() == Dt[0]["COMPANY_ID"].ToString())
                        {
                            cmbCompany.SelectedIndex = i;
                        }
                    }


                    for (int i = 0; i < cmbCountryMaster.Items.Count; i++)
                    {
                        NewListItem Lst = (NewListItem)cmbCountryMaster.Items[i];

                        if (Lst.Value.ToString() == Dt[0]["COUNTRY_Id"].ToString())
                        {
                            cmbCountryMaster.SelectedIndex = i;
                        }
                    }

                    for (int i = 0; i < cmbContractMaster.Items.Count; i++)
                    {
                        NewListItem Lst = (NewListItem)cmbContractMaster.Items[i];

                        if (Lst.Value.ToString() == Dt[0]["CONTRACT_TYPE"].ToString())
                        {
                            cmbContractMaster.SelectedIndex = i;
                        }
                    }


                    for (int i = 0; i < cmbStatusMaster.Items.Count; i++)
                    {
                        NewListItem Lst = (NewListItem)cmbStatusMaster.Items[i];

                        if (Lst.Value.ToString() == Dt[0]["STATUS"].ToString())
                        {
                            cmbStatusMaster.SelectedIndex = i;
                        }
                    }


                    //Goto Master Tab
                    tabControl1.SelectedIndex = 1;
                    tabControl2.SelectedIndex = 0;
                }
            }
            catch (Exception Exp)
            {

                MessageBox.Show(Exp.Message);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Add Contract Contact
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                int? X = 0;
                Ta.Insert_Master_Contact(ref X,
                    txtContactName.Text,
                    txtContactTitle.Text,
                    txtContactTell.Text,
                    txtContactFax.Text,
                    txtContactMobile.Text,
                    txtContactEmail.Text,
                    int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString())
                    );

                MessageBox.Show("Contact Inserted");

                FillContactGridContract();

                txtContactName.Text = "";
                txtContactTitle.Text = "";
                txtContactTell.Text = "";
                txtContactFax.Text = "";
                txtContactMobile.Text = "";
                txtContactEmail.Text = "";
            }
            else
            {
                MessageBox.Show("Please Select Contract From Seach tab First");
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //Select Contact For Contract
            FillContactGridContract();

        }
        protected void FillContactGridContract()
        {
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt = Ta.Select_Contact_ByField("MASTER_ID",
                    dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString());


                DataTable DTable = new DataTable();

                DataColumn col1 = new DataColumn("ID");
                DataColumn col2 = new DataColumn("Name");
                DataColumn col3 = new DataColumn("Title");
                DataColumn col4 = new DataColumn("fax");
                DataColumn col5 = new DataColumn("Mobile");
                DataColumn col6 = new DataColumn("Email");

                DTable.Columns.Add(col1);
                DTable.Columns.Add(col2);
                DTable.Columns.Add(col3);
                DTable.Columns.Add(col4);
                DTable.Columns.Add(col5);
                DTable.Columns.Add(col6);


                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = DTable.NewRow();
                    row[col1] = Dt[i]["ID"].ToString();
                    row[col2] = Dt[i]["Name"].ToString();
                    row[col3] = Dt[i]["Title"].ToString();
                    row[col4] = Dt[i]["fax"].ToString();
                    row[col5] = Dt[i]["Mobile"].ToString();
                    row[col6] = Dt[i]["Email"].ToString();
                    DTable.Rows.Add(row);
                }

                dgvContactList.DataSource = DTable;
                dgvContactList.Columns[0].Width = 40;
                dgvContactList.Columns[1].Width = 200;
                dgvContactList.Columns[2].Width = 60;
            }
            else
            {
                MessageBox.Show("Please Select Contract First From Search Tab");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult Rs = MessageBox.Show("Are you sure to delete selected contact?", "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rs == System.Windows.Forms.DialogResult.Yes)
            {
                //Delete contract Contact
                if (dgvContactList.SelectedRows.Count == 1)
                {
                    IADTableAdapter Ta = new IADTableAdapter();
                    Ta.Delete_Contact_ById(int.Parse(dgvContactList.SelectedRows[0].Cells[0].Value.ToString()));
                    FillContactGridContract();
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //Update Contact Contract
            if (dgvContactList.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                Ta.Update_Contact(int.Parse(dgvContactList.SelectedRows[0].Cells[0].Value.ToString()),
                    txtContactName.Text,
                    txtContactTitle.Text,
                    txtContactTell.Text,
                    txtContactFax.Text,
                    txtContactMobile.Text,
                    txtContactEmail.Text,
                    int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString())
                    );

                MessageBox.Show("Contact Data Updated");
                FillContactGridContract();
            }
        }

        private void dgvContactList_Click(object sender, EventArgs e)
        {
            //Select Current Contact
            if (dgvContactList.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt =
                    Ta.Select_Contact_ById(int.Parse(dgvContactList.SelectedRows[0].Cells[0].Value.ToString()));

                txtContactName.Text = Dt[0]["NAME"].ToString();
                txtContactTitle.Text = Dt[0]["TITLE"].ToString();
                txtContactTell.Text = Dt[0]["TELL"].ToString();
                txtContactFax.Text = Dt[0]["FAX"].ToString();
                txtContactEmail.Text = Dt[0]["Email"].ToString();
                txtContactMobile.Text = Dt[0]["Mobile"].ToString();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                //Upload File
                if (txtFilePath.Text.Length > 5)
                {
                    DirectoryInfo Dir =
                        new DirectoryInfo( System.Configuration.ConfigurationSettings.AppSettings["AttachmentsDirectory"].Trim()
                            + dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString() + "\\");
                    try
                    {
                        if (!Dir.Exists)
                        {
                            Dir.Create();
                        }
                    }
                    catch (Exception Exp)
                    {

                        MessageBox.Show(Exp.Message);
                    }


                    IADTableAdapter Ta = new IADTableAdapter();
                    int? X = 0;
                    Ta.Insert_File(ref X,
                        txtFileTitle.Text,
                        "",
                      int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString())
                      );

                    string Dest = Dir.FullName + X.ToString() + Path.GetExtension(openFileDialog1.FileName);

                    Ta.Update_File(X, txtFileTitle.Text, Dest,
                        int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString()));
                    try
                    {
                        File.Copy(openFileDialog1.FileName, Dest);
                    }
                    catch (Exception exp)
                    {

                        MessageBox.Show(exp.Message);
                    }


                    FillFiles();
                    txtFileTitle.Text = "";
                    txtFilePath.Text = "";
                    openFileDialog1.FileName = "";
                }
                else
                {
                    MessageBox.Show("Please Select File First");
                }
            }
            else
            {
                MessageBox.Show("Please Select Contract First From Search Tab");
            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtFilePath.Text = openFileDialog1.FileName;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //Select Files
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                FillFiles();
            }
            else
            {
                MessageBox.Show("Please Select Contract First From Search Tab");
            }

        }
        protected void FillFiles()
        {
            if (dgvSearchResult.SelectedRows.Count == 1)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                MyDB.IADDataTable Dt = Ta.Select_Files_ByField("MASTER_ID",
                    dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString());


                DataTable DTable = new DataTable();

                DataColumn col1 = new DataColumn("ID");
                DataColumn col2 = new DataColumn("Title");
                DataColumn col3 = new DataColumn("Path");

                DTable.Columns.Add(col1);
                DTable.Columns.Add(col2);
                DTable.Columns.Add(col3);



                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = DTable.NewRow();
                    row[col1] = Dt[i]["ID"].ToString();
                    row[col2] = Dt[i]["Title"].ToString();
                    row[col3] = Dt[i]["Path"].ToString();

                    DTable.Rows.Add(row);
                }

                dgvFiles.DataSource = DTable;
                dgvFiles.Columns[0].Width = 40;
                dgvFiles.Columns[1].Width = 200;
                dgvFiles.Columns[2].Width = 400;
            }
            else
            {
                MessageBox.Show("For view files please select Contract from Search tab First");
            }
        }

        private void dgvFiles_DoubleClick(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedRows.Count == 1)
            {
                System.Diagnostics.Process.Start(dgvFiles.SelectedRows[0].Cells[2].Value.ToString());
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            DialogResult Rs = MessageBox.Show("Are you sure to delete selected File?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rs == System.Windows.Forms.DialogResult.Yes)
            {
                if (dgvFiles.SelectedRows.Count == 1)
                {
                    try
                    {
                        FileInfo Fl = new FileInfo(dgvFiles.SelectedRows[0].Cells[2].Value.ToString());
                        if (Fl.Exists)
                        {
                            Fl.Delete();
                        }
                        else
                        {
                            MessageBox.Show("File Not Exist Or Server is shutdown");
                        }

                        IADTableAdapter Ta = new IADTableAdapter();
                        Ta.Delete_File_ById(int.Parse(dgvFiles.SelectedRows[0].Cells[0].Value.ToString()));
                        FillFiles();

                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(Exp.Message);
                        throw;
                    }

                }
            }

        }


        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            StringBuilder Condition = new StringBuilder();
            if (checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked ||
                checkBox7.Checked || checkBox8.Checked)
            {
                Condition.Append(" Where ");
            }
            if (checkBox2.Checked)
            {
                Condition.Append(" Country_Id=" + ((NewListItem)cmbCountrySearch.SelectedItem).Value + " and ");
            }

            if (checkBox3.Checked)
            {
                Condition.Append(" CONTRACT_TYPE=" + ((NewListItem)cmbContractSearch.SelectedItem).Value + " and ");
            }
            if (checkBox7.Checked)
            {
                string IsActive = "1";
                if (cmbStatusSearch.SelectedIndex == 0)
                {
                    IsActive = "1";
                }
                else
                {
                    IsActive = "0";

                }
                Condition.Append(" STATUS=" + IsActive + " and ");
            }
            if (checkBox5.Checked)
            {
                Condition.Append(" COMPANY_ID=" + ((NewListItem)comboBox1.SelectedItem).Value + " and ");
            }

            if (checkBox6.Checked)
            {
                Condition.Append(" DESCRIPTION like N'%" + richTextBox10.Text.Trim() + "%' and ");
            }

            if (checkBox4.Checked)
            {
                Condition.Append(" EXPIRY_DATE <= '" + dateTimePicker2.Value.ToShortDateString() + "' and ");
            }

            if (checkBox8.Checked)
            {
                Condition.Append(" EFFECTIVE_DATE <= '" + dateTimePicker4.Value.ToShortDateString() + "' and ");
            }

            if (Condition.Length > 6)
            {
                Condition = Condition.Remove(Condition.Length - 4, 4);
                Condition.Append(" order by EFFECTIVE_DATE desc");
            }


            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Dt = Ta.Master_Search(Condition.ToString());

            DataTable DTable = new DataTable();

            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("EFFECTIVE_DATE");
            DataColumn col3 = new DataColumn("EXPIRY_DATE");
            DataColumn col4 = new DataColumn("DESCRIPTION");
            DataColumn col5 = new DataColumn("COST");
            DataColumn col6 = new DataColumn("ACTIVE?");

            DTable.Columns.Add(col1);
            DTable.Columns.Add(col2);
            DTable.Columns.Add(col3);
            DTable.Columns.Add(col4);
            DTable.Columns.Add(col5);
            DTable.Columns.Add(col6);


            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = DTable.NewRow();
                row[col1] = Dt[i]["ID"].ToString();
                row[col2] = DateTime.Parse(Dt[i]["EFFECTIVE_DATE"].ToString()).ToShortDateString();
                row[col3] = DateTime.Parse(Dt[i]["EXPIRY_DATE"].ToString()).ToShortDateString();
                row[col4] = Dt[i]["DESCRIPTION"].ToString();
                row[col5] = Dt[i]["COST"].ToString();
                row[col6] = Dt[i]["STATUS"].ToString().Replace("True", "Active").Replace("False", "Not Active");
                DTable.Rows.Add(row);
            }

            dgvSearchResult.DataSource = DTable;
            dgvSearchResult.Columns[0].Width = 40;
            dgvSearchResult.Columns[1].Width = 100;
            dgvSearchResult.Columns[2].Width = 100;
            dgvSearchResult.Columns[3].Width = 150;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DialogResult Rs = MessageBox.Show("Are you sure to delete selected contract?", "Delete Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rs == System.Windows.Forms.DialogResult.Yes)
            {
                IADTableAdapter Ta = new IADTableAdapter();
                Ta.Delete_Master_ById(int.Parse(dgvSearchResult.SelectedRows[0].Cells[0].Value.ToString()));
                btnSearchSelect_Click(new object(), new EventArgs());
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewListItem Lst2 = (NewListItem)cmbCompany.SelectedItem;

            IADTableAdapter Ta = new IADTableAdapter();
            MyDB.IADDataTable Dt =
                Ta.Select_Company_ById(int.Parse(Lst2.Value.ToString()));

            txtMasterDesc.Text = Dt[0]["DESCIRPTION"].ToString();
            txtMasterRemarks.Text = Dt[0]["REMARKS"].ToString();


            for (int i = 0; i < cmbCountryMaster.Items.Count; i++)
            {
                NewListItem Lst = (NewListItem)cmbCountryMaster.Items[i];

                if (Lst.Value.ToString() == Dt[0]["CONCODE"].ToString())
                {
                    cmbCountryMaster.SelectedIndex = i;
                }
            }
        }

    }
}
