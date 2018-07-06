﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
namespace Human_Resource_Information_System
{
    public partial class m_employee : Form
    {
        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db = new thisDatabase();
        public m_employee()
        {
            InitializeComponent();
        }


        private void m_employee_Load(object sender, EventArgs e)
        {
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();


            date_shift_sched_from.CustomFormat = "hh:mm tt";
            date_shift_sched_from.ShowUpDown = true;
            date_shift_sched_to.CustomFormat = "hh:mm tt";
            date_shift_sched_to.ShowUpDown = true;

            date_sift_sched_sat_from.CustomFormat = "hh:mm tt";
            date_sift_sched_sat_from.ShowUpDown = true;

            date_sift_sched_sat_to.CustomFormat = "hh:mm tt";
            date_sift_sched_sat_to.ShowUpDown = true;

            dtp_resigned.Enabled = false;
            dtp_terminated.Enabled = false;
            dtp_contractual.Enabled = false;
            dtp_probitioned.Enabled = false;
            dtp_regularized.Enabled = false;
            gc.load_emp_stat(cbo_status);
            gc.load_rate_type(cbo_rate_type);
            gc.load_wtax(cbo_tax_bracket);
            gc.load_phic(cbo_phic);

            gc.load_days(cbo_dayoff1);
            gc.load_days(cbo_dayoff2);


            gc.load_dept(cbo_department);
            gc.load_position(cbo_position);
            gc.load_civil_status(cbo_civil_stat);
            gc.load_cbo_sss(cbo_sss);

            disp_list();
        }
        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();

            tbcntrl_main.SelectedTab = tpg_info;
            tpg_info.Show();
            seltbp = false;
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_opt_1.Show();

            tbcntrl_main.SelectedTab = tpg_list;
            tpg_list.Show();
            seltbp = false;
            tab_details.SelectedTab = tpg_emp_info;
            tpg_emp_info.Show();
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_win1();
            disp_list();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            Boolean success = false, ok = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();

            String branch = GlobalClass.branch;
            String col = "", val = "", add_col = "", add_val = "";
            String notifyadd = null;
            String table = "hr_employee";
            String code = "",phic="", w_tax = "" , lastname = "", firstname = "", mi = "", section = "", position = "", picture = "", department = "", date_hired = "1900-01-01", contractual_date = "1900-01-01", prohibition_date = "1900-01-01", date_regular = "1900-01-01", date_resigned = "1900-01-01", date_terminated = "1900-01-01", empstatus = "", contract_days = "0", prc = "", ctc = "", rate_type = "", pay_rate = "", biometric = "", sss = "", pagibig = "", philhealth = "", payroll_account = "", tin = "", tax_bracket = "", shift_sched_from = "", dayoff1 = "", dayoff2 = "", sex = "", birth = "", civil_status = "", religion = "", height = "0.00", weight = "0.00", father = "", father_address = "", father_contact = "", father_job = "", mother = "", mother_address = "", mother_contact = "", mother_job = "", emp_contact = "", home_tel = "", email = "", home_address = "", emergency_name = "", emergency_contact = "", em_home_address = "", relationship = "", shift_sched_sat_from = "", shift_sched_to = "", shift_sched_sat_to = "", fixed_rate = "", primary = "", secondary = "", tertiary = "", graduate = "", post_graduate = "",sss_table = "";

            if (String.IsNullOrEmpty(txt_lastname.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }
            if (String.IsNullOrEmpty(txt_firstname.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }

            if (String.IsNullOrEmpty(txt_mi.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }

            if (cbo_department.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department.");
                cbo_department.DroppedDown = true;
                return;
            }

            if (cbo_section.Items.Count > 0)
            {
                if (cbo_section.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select department section.");
                    cbo_section.DroppedDown = true;
                    return;
                }
            }


            if (cbo_position.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a position.");
                cbo_position.DroppedDown = true;
                return;
            }

            if (cbo_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select employee status.");
                cbo_status.DroppedDown = true;
                return;
            }
            if (dtp_hired.Value.ToShortDateString() == null)
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }
            if (cbo_rate_type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a rate type.");
                cbo_rate_type.DroppedDown = true;
                return;
            }
            if (cbo_tax_bracket.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a tax bracket.");
                cbo_tax_bracket.DroppedDown = true;
                return;
            }
            if (cbo_gender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.");
                cbo_gender.DroppedDown = true;
                return;
            }


            w_tax = txt_w_tax.Text != "" ? txt_w_tax.Text : "0.00";
            lastname = txt_lastname.Text;
            firstname = txt_firstname.Text;
            mi = txt_mi.Text;
            if (cbo_department.SelectedIndex != -1)
            {
                department = cbo_department.SelectedValue.ToString();
            }

            if (cbo_section.SelectedIndex != -1)
            {
                section = cbo_section.SelectedValue.ToString();
            }
            if (cbo_position.SelectedIndex != -1)
            {
                position = cbo_position.SelectedValue.ToString();
            }

            date_hired = dtp_hired.Value.ToString("yyyy-MM-dd");

            if (cbo_status.SelectedIndex != -1)
            {
                empstatus = cbo_status.SelectedValue.ToString();
            }

            if (txt_contract_days.Text != "")
            {
                contract_days = txt_contract_days.Text;
            }
            prc = txt_prc_number.Text;
            ctc = txt_ctc_num.Text;

            if (cbo_rate_type.SelectedIndex != -1)
            {
                rate_type = cbo_rate_type.SelectedValue.ToString();
            }

            if(cbo_sss.SelectedIndex != -1)
            {
                sss_table = cbo_sss.SelectedValue.ToString();
            }
            try
            {
                pay_rate = Convert.ToDouble(txt_pay_rate.Text).ToString();
            }
            catch (Exception ex) { MessageBox.Show("Pay rate must be numeric."); return; }


            biometric = txt_biometric.Text;
            sss = txt_sss_number.Text;

            
            
            pagibig = txt_pagibig.Text;
            philhealth = txt_philhealth.Text;
            payroll_account = txt_payroll_act.Text;
            tin = txt_tin.Text;
            

            shift_sched_from = date_shift_sched_from.Value.ToString("HH:mm");
            shift_sched_to = date_shift_sched_to.Value.ToString("HH:mm");
            shift_sched_sat_from = date_sift_sched_sat_from.Value.ToString("HH:mm");
            shift_sched_sat_to = date_sift_sched_sat_to.Value.ToString("HH:mm");
            if (cbo_gender.SelectedIndex != -1)
            {
                sex = cbo_gender.SelectedItem.ToString();
            }

            birth = date_birth.Value.ToShortDateString();
            if (cbo_civil_stat.SelectedIndex != -1)
            {
                civil_status = cbo_civil_stat.SelectedValue.ToString();
            }

            religion = txt_religion.Text;

            try
            {
                height = Convert.ToDouble(txt_height.Text).ToString();
            }
            catch (Exception ex) { MessageBox.Show("Height must be numeric"); return; }


            try { weight = Convert.ToDouble(txt_weight.Text).ToString(); }
            catch (Exception ex) { MessageBox.Show("Weight must be numeric"); return; }


            if (chk_fixed_rate.Checked == true)
            {
                fixed_rate = "1";
            }

            father = txt_father.Text;
            father_address = txt_father_address.Text;
            father_contact = txt_father_contact.Text;
            father_job = txt_father_occupation.Text;
            mother = txt_mother.Text;
            mother_address = txt_mother_address.Text;
            mother_contact = txt_mother_contact.Text;
            mother_job = txt_mother_occupation.Text;
            emp_contact = txt_contact_no.Text;
            home_tel = txt_home_tel.Text;
            email = txt_email.Text;
            home_address = txt_home_address.Text;
            emergency_name = txt_ctc_name.Text;
            emergency_contact = txt_ctc_no.Text;
            em_home_address = txt_home_add.Text;
            relationship = txt_relation.Text;
            primary = txt_primary.Text;
            secondary = txt_secondary.Text;
            tertiary = txt_tertiary.Text;
            graduate = txt_graduate.Text;
            post_graduate = txt_post_graduate.Text;
            if (cbo_dayoff1.SelectedIndex != -1)
            {
                dayoff1 = cbo_dayoff1.SelectedValue.ToString();

            }
            else
            {
                MessageBox.Show("First dayoff is required");
                return;
            }
            if (cbo_dayoff2.SelectedIndex != -1)
            {
                dayoff2 = cbo_dayoff2.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("Second dayoff is required");
                return;
            }
            if(cbo_phic.SelectedIndex != -1)
            {
                phic = cbo_phic.SelectedValue.ToString();
            }
            

            if (chk_resigned.Checked == true)
            {
                date_resigned = dtp_resigned.Value.ToString("yyyy-MM-dd");
            }
            if (chk_terminated.Checked == true)
            {
                date_terminated = dtp_terminated.Value.ToString("yyyy-MM-dd");
            }
            if (chk_contractual.Checked == true)
            {
                contractual_date = dtp_contractual.Value.ToString("yyyy-MM-dd");
            }
            if (chk_probition.Checked == true)
            {
                prohibition_date = dtp_probitioned.Value.ToString("yyyy-MM-dd");
            }
            if (chk_regular.Checked == true)
            {
                date_regular = dtp_regularized.Value.ToString("yyyy-MM-dd");
            }

            if (isnew)
            {
                code = code = db.get_pk("empid"); //changes from 'hr_empid'
                col = "empid,lastname,firstname,mi,positions,department,section,date_hired,contractual_date,date_resigned,date_terminated,prohibition_date,date_regular,empstatus,contract_days,prc,ctc,rate_type,pay_rate,biometric,sss,pagibig,philhealth,payroll_account,tin,tax_bracket,shift_sched_from,dayoff1,dayoff2,sex,birth,civil_status,religion,height,weight,father,father_address,father_contact,father_job,mother,mother_address,mother_contact,mother_job,emp_contact,home_tel,email,home_address,emergency_name,emergency_contact,em_home_address,relationship,shift_sched_sat_from,shift_sched_to,shift_sched_sat_to,fixed_rate,primary_ed,secondary_ed,tertiary_ed,graduate,post_graduate,sss_table,phic_table,w_tax";

                val = "" + db.str_E(code) + "," + db.str_E(lastname) + "," + db.str_E(firstname) + "," + db.str_E(mi) + "," + db.str_E(position) + "," + db.str_E(department) + "," + db.str_E(section) + ",'" + date_hired + "','" + contractual_date + "','" + date_resigned + "','" + date_terminated + "','" + prohibition_date + "','" + date_regular + "'," + db.str_E(empstatus) + "," + db.str_E(contract_days) + "," + db.str_E(prc) + "," + db.str_E(ctc) + "," + db.str_E(rate_type) + "," + db.str_E(pay_rate) + "," + db.str_E(biometric) + "," + db.str_E(sss) + "," + db.str_E(pagibig) + ", " + db.str_E(philhealth) + "," + db.str_E(payroll_account) + "," + db.str_E(tin) + "," + db.str_E(tax_bracket) + "," + db.str_E(shift_sched_from) + "," + db.str_E(dayoff1) + "," + db.str_E(dayoff2) + "," + db.str_E(sex) + "," + db.str_E(birth) + "," + db.str_E(civil_status) + "," + db.str_E(religion) + "," + db.str_E(height) + "," + db.str_E(weight) + "," + db.str_E(father) + "," + db.str_E(father_address) + "," + db.str_E(father_contact) + "," + db.str_E(father_job) + "," + db.str_E(mother) + "," + db.str_E(mother_address) + "," + db.str_E(mother_contact) + "," + db.str_E(mother_job) + "," + db.str_E(emp_contact) + "," + db.str_E(home_tel) + "," + db.str_E(email) + "," + db.str_E(home_address) + "," + db.str_E(emergency_name) + "," + db.str_E(emergency_contact) + "," + db.str_E(em_home_address) + "," + db.str_E(relationship) + "," + db.str_E(shift_sched_sat_from) + "," + db.str_E(shift_sched_to) + "," + db.str_E(shift_sched_sat_to) + "," + db.str_E(fixed_rate) + "," + db.str_E(primary) + "," + db.str_E(secondary) + "," + db.str_E(tertiary) + "," + db.str_E(graduate) + "," + db.str_E(post_graduate) + "," + db.str_E(sss_table) + "," + db.str_E(phic) + "," + db.str_E(w_tax ) + "";

                //db.DeleteOnTable(table, "empid='" + code + "' AND cancel='Y'");
                if (db.InsertOnTable(table, col, val))
                {
                    success = true;
                    db.set_pkm99("empid", db.get_nextincrementlimitchar(code, 8)); //changes from 'hr_empid'
                }
                else
                {
                    success = false;
                    //db.DeleteOnTable(table, "empid='" + code + "'");
                    MessageBox.Show("Failed on saving.");
                }
            }
            else
            {
                col = "lastname=" + db.str_E(lastname) + ",firstname=" + db.str_E(firstname) + ",mi=" + db.str_E(mi) + ",positions=" + db.str_E(position) + ",department=" + db.str_E(department) + ",section =" + db.str_E(section) + ",date_hired='" + date_hired + "',contractual_date='" + contractual_date + "',date_resigned = '" + date_resigned + "',date_terminated='" + date_terminated + "',prohibition_date = '" + prohibition_date + "',date_regular ='" + date_regular + "',empstatus=" + db.str_E(empstatus) + ",contract_days=" + db.str_E(contract_days) + ",prc=" + db.str_E(prc) + ",ctc=" + db.str_E(ctc) + ",rate_type=" + db.str_E(rate_type) + ",pay_rate=" + db.str_E(pay_rate) + ",biometric=" + db.str_E(biometric) + ",sss=" + db.str_E(sss) + ",pagibig=" + db.str_E(pagibig) + ",philhealth=" + db.str_E(philhealth) + ",payroll_account=" + db.str_E(payroll_account) + ",tin=" + db.str_E(tin) + ",tax_bracket=" + db.str_E(tax_bracket) + ", shift_sched_from=" + db.str_E(shift_sched_from) + ",dayoff1=" + db.str_E(dayoff1) + ",dayoff2=" + db.str_E(dayoff2) + ",sex=" + db.str_E(sex) + ",birth=" + db.str_E(birth) + ",civil_status=" + db.str_E(civil_status) + ",religion=" + db.str_E(religion) + ",height=" + db.str_E(height) + ",weight=" + db.str_E(weight) + ",father=" + db.str_E(father) + ",father_address=" + db.str_E(father_address) + ", father_contact=" + db.str_E(father_contact) + ",father_job=" + db.str_E(father_job) + ",mother=" + db.str_E(mother) + ", mother_address=" + db.str_E(mother_address) + ",mother_contact=" + db.str_E(mother_contact) + ", mother_job=" + db.str_E(mother_job) + ", emp_contact=" + db.str_E(emp_contact) + ", home_tel=" + db.str_E(home_tel) + ",email=" + db.str_E(email) + ", home_address=" + db.str_E(home_address) + ",emergency_name=" + db.str_E(emergency_name) + ", emergency_contact=" + db.str_E(emergency_contact) + ",em_home_address=" + db.str_E(em_home_address) + ",relationship=" + db.str_E(relationship) + ",shift_sched_sat_from=" + db.str_E(shift_sched_sat_from) + ",shift_sched_to=" + db.str_E(shift_sched_to) + ",shift_sched_sat_to=" + db.str_E(shift_sched_sat_to) + ",fixed_rate=" + db.str_E(fixed_rate) + ",primary_ed=" + db.str_E(primary) + ",secondary_ed=" + db.str_E(secondary) + ",tertiary_ed=" + db.str_E(tertiary) + ",graduate=" + db.str_E(graduate) + ",post_graduate=" + db.str_E(post_graduate) + ",sss_table=" + db.str_E(sss_table) + ",phic_table=" +db.str_E(phic)+ ",w_tax='" + w_tax + "'";
                code = txt_code.Text;
                if (db.UpdateOnTable(table, col, "empid=" + db.str_E(code) + ""))
                {
                    success = true;
                }
                else
                {
                    success = false;
                    MessageBox.Show("Failed on saving.");
                }
            }

            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            }
        }

        private void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_employee ORDER BY empid ASC");

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int i = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[i];

                row.Cells["ID"].Value = dt.Rows[r]["empid"].ToString();
                row.Cells["name"].Value = dt.Rows[r]["firstname"].ToString() + " " + dt.Rows[r]["lastname"].ToString() + " " + dt.Rows[r]["mi"].ToString();

            }
        }


        private void cbo_department_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gc.load_section(cbo_section, cbo_department.SelectedValue.ToString());
            cbo_section.DroppedDown = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tpg_background_Click(object sender, EventArgs e)
        {

        }

        private void tpg_education_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_resigned.Checked == false)
            {
                dtp_resigned.Enabled = false;
            }
            else
            {
                dtp_resigned.Enabled = true;
            }
        }

        private void chk_terminated_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_terminated.Checked == false)
            {
                dtp_terminated.Enabled = false;
            }
            else
            {
                dtp_terminated.Enabled = true;
            }
        }

        private void chk_contractual_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_contractual.Checked == false)
            {
                dtp_contractual.Enabled = false;
            }
            else
            {
                dtp_contractual.Enabled = true;
            }
        }

        private void chk_probition_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_probition.Checked == false)
            {
                dtp_probitioned.Enabled = false;
            }
            else
            {
                dtp_probitioned.Enabled = true;
            }
        }

        private void chk_regular_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_regular.Checked == false)
            {
                dtp_regularized.Enabled = false;
            }
            else
            {
                dtp_regularized.Enabled = true;
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            frm_clear();

            isnew = true;
            goto_win2();

        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;


            int r = -1;
            String code = "", name = "";
            try
            {
                if (dgv_list.Rows.Count > 1)
                {
                    r = dgv_list.CurrentRow.Index;

                    try
                    {
                        code = dgv_list["ID", r].Value.ToString();

                        display_employee(code);
                    }
                    catch { }

                    goto_win2();
                }
                else
                {
                    MessageBox.Show("Employee list is empty.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void display_employee(String code)
        {
            DataTable dt = db.QueryBySQLCode("SELECT distinct emp.*,civil.*,day.*,dept.*,section.*,emp_status.*,pos.*,wtax.*,rate_type.* FROM rssys.hr_employee emp LEFT JOIN rssys.hr_civil_status civil ON civil.code = emp.civil_status LEFT JOIN rssys.hr_days day ON day.day = emp.dayoff1 LEFT JOIN rssys.hr_department dept ON dept.deptid = emp.department LEFT JOIN rssys.hr_depsection section ON section.secid = emp.section LEFT JOIN rssys.hr_emp_status emp_status ON emp_status.statcode = emp.empstatus LEFT JOIN rssys.hr_position pos ON pos.postid = emp.positions LEFT JOIN rssys.hr_rate_type rate_type ON rate_type.ratecode = emp.rate_type LEFT JOIN rssys.hr_wtax wtax ON wtax.code = emp.tax_bracket WHERE emp.empid ='" + code + "' LIMIT 1");
            if (dt.Rows.Count > 0)
            {
                txt_code.Text = dt.Rows[0]["empid"].ToString();
                txt_firstname.Text = dt.Rows[0]["firstname"].ToString();
                txt_lastname.Text = dt.Rows[0]["lastname"].ToString();
                txt_mi.Text = dt.Rows[0]["mi"].ToString();
                cbo_department.SelectedValue = dt.Rows[0]["department"].ToString();
                gc.load_section(cbo_section, cbo_department.SelectedValue.ToString());
                cbo_section.SelectedValue = dt.Rows[0]["section"].ToString();
                cbo_position.SelectedValue = dt.Rows[0]["positions"].ToString();
                dtp_hired.Value = gm.toDateValue(dt.Rows[0]["date_hired"].ToString());

                if (!String.IsNullOrEmpty(dt.Rows[0]["date_resigned"].ToString()))
                {
                    chk_resigned.Checked = true;
                    dtp_resigned.Value = gm.toDateValue(dt.Rows[0]["date_resigned"].ToString());
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["date_terminated"].ToString()))
                {
                    chk_terminated.Checked = true;
                    dtp_terminated.Value = gm.toDateValue(dt.Rows[0]["date_terminated"].ToString());
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["contractual_date"].ToString()))
                {
                    chk_contractual.Checked = true;
                    dtp_contractual.Value = gm.toDateValue(dt.Rows[0]["contractual_date"].ToString());
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["prohibition_date"].ToString()))
                {
                    chk_probition.Checked = true;
                    dtp_probitioned.Value = gm.toDateValue(dt.Rows[0]["prohibition_date"].ToString());
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["date_regular"].ToString()))
                {
                    chk_regular.Checked = true;
                    dtp_regularized.Value = gm.toDateValue(dt.Rows[0]["date_regular"].ToString());
                }

                cbo_status.SelectedValue = dt.Rows[0]["empstatus"].ToString();
                txt_contract_days.Text = dt.Rows[0]["contract_days"].ToString();
                txt_prc_number.Text = dt.Rows[0]["prc"].ToString();
                txt_ctc_num.Text = dt.Rows[0]["ctc"].ToString();
                cbo_rate_type.SelectedValue = dt.Rows[0]["rate_type"].ToString();
                try { txt_pay_rate.Text = Convert.ToDouble(dt.Rows[0]["pay_rate"]).ToString("N", new CultureInfo("en-US")); }
                catch { txt_pay_rate.Text = "0.00"; }
                txt_biometric.Text = dt.Rows[0]["biometric"].ToString();
                cbo_sss.SelectedValue = dt.Rows[0]["sss_table"].ToString();
                txt_sss_number.Text =  dt.Rows[0]["sss"].ToString();
                txt_pagibig.Text = dt.Rows[0]["pagibig"].ToString();
                txt_philhealth.Text = dt.Rows[0]["philhealth"].ToString();
                txt_payroll_act.Text = dt.Rows[0]["payroll_account"].ToString();
                txt_tin.Text = dt.Rows[0]["tin"].ToString();
                cbo_tax_bracket.SelectedValue = dt.Rows[0]["tax_bracket"].ToString();
                txt_primary.Text = dt.Rows[0]["primary_ed"].ToString();
                txt_secondary.Text = dt.Rows[0]["secondary_ed"].ToString();
                txt_graduate.Text = dt.Rows[0]["graduate"].ToString();
                txt_post_graduate.Text = dt.Rows[0]["post_graduate"].ToString();
                txt_tertiary.Text = dt.Rows[0]["tertiary_ed"].ToString();



                if (dt.Rows[0]["fixed_rate"].ToString() == "1")
                {
                    chk_fixed_rate.Checked = true;
                }

                date_shift_sched_from.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_from"].ToString());



                date_shift_sched_to.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_to"].ToString());



                date_sift_sched_sat_from.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_sat_from"].ToString());
                date_sift_sched_sat_to.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_sat_to"].ToString());


                cbo_dayoff1.SelectedValue = dt.Rows[0]["dayoff1"].ToString();
                cbo_dayoff2.SelectedValue = dt.Rows[0]["dayoff2"].ToString();
                cbo_gender.SelectedItem = dt.Rows[0]["sex"].ToString();
                date_birth.Value = gm.toDateValue(dt.Rows[0]["birth"].ToString());
                cbo_civil_stat.SelectedValue = dt.Rows[0]["civil_status"].ToString();
                txt_religion.Text = dt.Rows[0]["religion"].ToString();

                try { txt_height.Text = Convert.ToDouble(dt.Rows[0]["height"]).ToString("N", new CultureInfo("en-US")); }
                catch { txt_height.Text = "0.00"; }

                try { txt_weight.Text = Convert.ToDouble(dt.Rows[0]["weight"]).ToString("N", new CultureInfo("en-US")); }
                catch { txt_weight.Text = "0.00"; }

                txt_father.Text = dt.Rows[0]["father"].ToString();
                txt_father_address.Text = dt.Rows[0]["father_address"].ToString();
                txt_father_contact.Text = dt.Rows[0]["father_contact"].ToString();
                txt_father_occupation.Text = dt.Rows[0]["father_job"].ToString();
                txt_mother.Text = dt.Rows[0]["mother"].ToString();
                txt_mother_address.Text = dt.Rows[0]["mother_address"].ToString();
                txt_mother_contact.Text = dt.Rows[0]["mother_contact"].ToString();
                txt_mother_occupation.Text = dt.Rows[0]["mother_job"].ToString();
                txt_contact_no.Text = dt.Rows[0]["emp_contact"].ToString();
                txt_home_tel.Text = dt.Rows[0]["home_tel"].ToString();
                txt_email.Text = dt.Rows[0]["email"].ToString();
                txt_home_address.Text = dt.Rows[0]["home_address"].ToString();
                txt_ctc_name.Text = dt.Rows[0]["emergency_name"].ToString();
                txt_ctc_no.Text = dt.Rows[0]["emergency_contact"].ToString();
                txt_home_add.Text = dt.Rows[0]["em_home_address"].ToString();
                txt_relation.Text = dt.Rows[0]["relationship"].ToString();

                cbo_phic.SelectedValue = dt.Rows[0]["phic_table"].ToString();
                txt_w_tax.Text = dt.Rows[0]["w_tax"].ToString();
            }

        }
        private void frm_clear()
        {
            txt_code.Text = "";
            txt_lastname.Text = "";
            txt_firstname.Text = "";
            txt_mi.Text = "";
            cbo_department.SelectedIndex = -1;
            cbo_section.SelectedIndex = -1;
            cbo_position.SelectedIndex = -1;
            dtp_hired.ResetText();
            txt_w_tax.Text = "";
            chk_resigned.Checked = false;
            dtp_resigned.Enabled = false;

            chk_contractual.Checked = false;
            dtp_contractual.Enabled = false;

            chk_terminated.Checked = false;
            dtp_terminated.Enabled = false;

            chk_probition.Checked = false;
            dtp_probitioned.Checked = false;

            chk_regular.Checked = false;
            dtp_regularized.Enabled = false;

            cbo_status.SelectedIndex = -1;
            txt_contract_days.Text = "";
            txt_prc_number.Text = "";
            txt_ctc_num.Text = "";
            cbo_rate_type.SelectedIndex = -1;
            txt_pay_rate.Text = "";
            txt_biometric.Text = "";
            //cbo_sss.SelectedIndex = -1;
            txt_sss_number.Text = "";
            txt_pagibig.Text = "";
            txt_philhealth.Text = "";
            txt_payroll_act.Text = "";
            txt_tin.Text = "";
            cbo_tax_bracket.SelectedIndex = -1;

            date_shift_sched_from.ResetText();
            date_sift_sched_sat_from.ResetText();

            date_shift_sched_to.ResetText();
            date_sift_sched_sat_from.ResetText();
            date_sift_sched_sat_to.ResetText();


            cbo_dayoff1.SelectedIndex = -1;
            cbo_dayoff2.SelectedIndex = -1;
            cbo_gender.SelectedIndex = -1;
            date_birth.ResetText();
            cbo_civil_stat.SelectedIndex = -1;
            txt_religion.Text = "";
            txt_height.Text = "";
            txt_weight.Text = "";
            txt_father.Text = "";
            txt_father_address.Text = "";
            txt_father_contact.Text = "";
            txt_father_occupation.Text = "";
            txt_mother.Text = "";
            txt_mother_address.Text = "";
            txt_mother_contact.Text = "";
            txt_mother_occupation.Text = "";
            txt_contact_no.Text = "";
            txt_home_tel.Text = "";
            txt_email.Text = "";
            txt_home_address.Text = "";
            txt_ctc_name.Text = "";
            txt_ctc_no.Text = "";
            txt_home_add.Text = "";
            txt_relation.Text = "";
            txt_primary.Text = "";
            txt_secondary.Text = "";
            txt_tertiary.Text = "";
            txt_graduate.Text = "";
            txt_post_graduate.Text = "";

        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unable to use this proccess.");
        }

        private void tpg_contribution_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }
    }

}
