﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Human_Resource_Information_System
{
    public partial class rpt_payroll_summary : Form
    {
        thisDatabase db = new thisDatabase();
        String fileloc_dtr = "";

        private GlobalClass gc;
        private GlobalMethod gm;
        public String papersize;

        public rpt_payroll_summary()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
            InitializeComponent();
        }

        private void rpt_payroll_summary_Load(object sender, EventArgs e)
        {
            fileloc_dtr = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            pic_loading.Visible = false;
            gc.load_payroll_period(cbo_payollperiod);
            gc.load_dept(cbo_department);
            gc.load_employee(cbo_employee);
            display_list();
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void display_list()
        {
            dgvl_payroll_summary.Invoke(new Action(() => {
                try { dgvl_payroll_summary.Rows.Clear(); }
                catch (Exception) { }
                int i = 0;
                String query = "SELECT * FROM rssys.hr_rpt_files WHERE rpt_type = 'PAYROLL_SUMMARY' ORDER BY date_added";

                try
                {
                    DataTable dt = db.QueryBySQLCode(query);

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgvl_payroll_summary.Rows.Add();
                        DataGridViewRow row = dgvl_payroll_summary.Rows[i];

                        row.Cells["rpt_id"].Value = dt.Rows[r]["rpt_id"].ToString();
                        row.Cells["filename"].Value = dt.Rows[r]["filename"].ToString();
                        row.Cells["date_added"].Value = dt.Rows[r]["date_added"].ToString();

                        i++;
                    }
                }
                catch { }
            }));
        }


        private void btn_submit_Click(object sender, EventArgs e)
        {
            papersize = "";
            if (cbo_payollperiod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payroll period.");
                cbo_payollperiod.DroppedDown = true;
                return;
            }

            //page_size page_size = new page_size();
            //page_size.ShowDialog();

            btn_submit.Enabled = false;
            pic_loading.Visible = true;
            bgworker.RunWorkerAsync();
        }
        
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            String query = "",dept_query = "",deptid = "", empid = "", date_from = "", date_to = "", pay_code = "", table = "hr_rpt_files", filename = "", code = "", col = "", val = "", date_in = "";
            DataTable pay_period = null;
            
            query = "SELECT empid, firstname, lastname,mi,pay_rate,department,sss_table,phic_table FROM rssys.hr_employee";
            dept_query = "SELECT * FROM rssys.hr_department";

            cbo_employee.Invoke(new Action(() => {
                if (cbo_employee.SelectedIndex != -1)
                {
                    empid = cbo_employee.SelectedValue.ToString();
                    query += " WHERE empid='" + empid + "'";
                }
            }));

            cbo_department.Invoke(new Action(() =>
            {
                if(cbo_department.SelectedIndex != -1)
                {
                    deptid = cbo_department.SelectedValue.ToString();
                    dept_query += " WHERE deptid = '" + deptid + "' LIMIT 1";
                }
            }));
            query += " ORDER BY empid ASC";

            try
            {
                DataTable departments = db.QueryBySQLCode(dept_query);
                DataTable employees = db.QueryBySQLCode(query);
                cbo_payollperiod.Invoke(new Action(() => {
                    pay_code = cbo_payollperiod.SelectedValue.ToString();
                }));

                pay_period = get_date(pay_code);

                if (pay_period.Rows.Count > 0)
                {
                    date_from = gm.toDateString(pay_period.Rows[0]["date_from"].ToString(), "yyyy-MM-dd");
                    date_to = gm.toDateString(pay_period.Rows[0]["date_to"].ToString(), "yyyy-MM-dd");
                }

                filename = RandomString(5) + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                filename += ".pdf";

                Document document = null;
                //System.IO.FileStream fs = new FileStream("\\\\RIGHTAPPS\\RightApps\\Eastland\\payroll_reports\\payroll_summary\\" + filename, FileMode.Create);
                System.IO.FileStream fs = new FileStream(fileloc_dtr + "\\ViewController\\RPT\\Payroll\\payroll_summary\\" + filename, FileMode.Create);
               
                document = new Document(PageSize.LETTER.Rotate());

                PdfWriter.GetInstance(document, fs);
                document.Open();

                document.Add(new Paragraph("SUGBU REALTY, INC", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_LEFT });

                document.Add(new Paragraph("PAYROLL PERIOD", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_LEFT });

                document.Add(new Chunk("\n"));

                PdfPTable _thead = new PdfPTable(15);
                _thead.WidthPercentage = 100f;
                float[] columnWidths = { 100, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 };
                _thead.SetWidths(columnWidths);



                _thead.AddCell(new PdfPCell(new Paragraph("Employee", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Rate", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Basic Pay", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Late/UT", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Overtime", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Holiday Pay", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Earnings", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Nigt Diff./Leaves", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Gross Pay", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Deductions", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("SSS", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("PHIC", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("HDMF", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("With-Tax-C", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                _thead.AddCell(new PdfPCell(new Paragraph("Net Pay", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });

                //ITEXTSHARP CELLS VARIABLE

                Double pay_rate = 0, basic_pay = 0, late_ut = 0, regular_ot_b = 0, dayoff_ot_b = 0, legal_hol_ot_b = 0, special_hol_ot_b = 0, legal_hol_pay_b = 0, spl_hol_pay_b = 0, night_diff = 0, overtime = 0, holiday_pay = 0, gross_pay = 0, night_diff_pay = 0, er_share = 0, ee_share = 0, ec_share = 0, phic_er = 0, phic_ee = 0, w_tax = 0, vl_b = 0, sl_b = 0, pl_b = 0, total_leave = 0;

                cbo_employee.Invoke(new Action(() =>
                {
                    if (cbo_employee.SelectedIndex != -1)
                    {
                        foreach (DataRow _emp in employees.Rows)
                        {
                            DataTable emp_dept = db.QueryBySQLCode("SELECT * FROM rssys.hr_department WHERE deptid='" + _emp["department"].ToString() + "' LIMIT 1");
                            DataTable emp_payroll = db.QueryBySQLCode("SELECT * FROM rssys.hr_emp_payroll WHERE empid = '" + _emp["empid"].ToString() + "'");

                            _thead.AddCell(new PdfPCell(new Paragraph(emp_dept.Rows[0]["dept_name"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLDITALIC)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });


                            try { pay_rate = Convert.ToDouble(_emp["pay_rate"].ToString()); } catch { pay_rate = 0.00; }
                            try { basic_pay = Convert.ToDouble(emp_payroll.Rows[0]["basic_pay"].ToString()); } catch { basic_pay = 0.00; }
                            try { late_ut = Convert.ToDouble(emp_payroll.Rows[0]["late_ut"].ToString()); } catch { late_ut = 0.00; }
                            try { regular_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["reqular_ot_b"].ToString()); } catch { regular_ot_b = 0.00; }
                            try { dayoff_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["dayoff_ot_b"].ToString()); } catch { dayoff_ot_b = 0.00;  }
                            try { legal_hol_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["legal_hol_ot_b"].ToString()); } catch { legal_hol_ot_b = 0.00; }
                            try { special_hol_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["special_hol_ot_b"].ToString()); } catch { special_hol_ot_b = 0.00; }
                            try { legal_hol_pay_b = Convert.ToDouble(emp_payroll.Rows[0]["legal_hol_pay_b"].ToString()); } catch { legal_hol_pay_b = 0.00; }
                            try { spl_hol_pay_b = Convert.ToDouble(emp_payroll.Rows[0]["spl_hol_pay_b"].ToString()); } catch { spl_hol_pay_b = 0.00; }
                            try { night_diff = Convert.ToDouble(emp_payroll.Rows[0]["night_diff_b"].ToString()); } catch { night_diff = 0.00; }
                            try { overtime = regular_ot_b + dayoff_ot_b + legal_hol_ot_b + special_hol_ot_b; } catch { overtime = 0.00;  }
                            try { vl_b = Convert.ToDouble(emp_payroll.Rows[0]["vl_b"].ToString()); } catch { vl_b = 0.00; }
                            try { sl_b = Convert.ToDouble(emp_payroll.Rows[0]["sl_b"].ToString()); } catch { sl_b = 0.00; }
                            try { pl_b = Convert.ToDouble(emp_payroll.Rows[0]["pl_b"].ToString()); } catch { pl_b = 0.00; }

                            total_leave = vl_b + sl_b + pl_b;

                            holiday_pay = legal_hol_pay_b + spl_hol_pay_b;

                            gross_pay = basic_pay + night_diff + regular_ot_b + dayoff_ot_b + legal_hol_ot_b + special_hol_ot_b + legal_hol_pay_b + spl_hol_pay_b;

                            gross_pay = gross_pay - late_ut + total_leave;

                            _thead.AddCell(new PdfPCell(new Paragraph(_emp["lastname"].ToString() + ", " + _emp["firstname"].ToString() + " " + _emp["mi"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph(pay_rate.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            _thead.AddCell(new PdfPCell(new Paragraph(basic_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            _thead.AddCell(new PdfPCell(new Paragraph("-" + late_ut.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            _thead.AddCell(new PdfPCell(new Paragraph(overtime.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            _thead.AddCell(new PdfPCell(new Paragraph(holiday_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });

                            PdfPTable _other_earnings_table = new PdfPTable(2);
                            _other_earnings_table.WidthPercentage = 100f;
                            float[] oe_columnWidths = { 50, 50 };
                            _other_earnings_table.SetWidths(oe_columnWidths);
                            Double earnings_total = 0;
                            DataTable _other_earnings = db.QueryBySQLCode("SELECT oe.description, SUM(ee.amount) as total FROM rssys.hr_other_earnings oe  LEFT JOIN rssys.hr_earning_entry ee ON oe.code =  ee.earning_code WHERE ee.payroll_period = '" + pay_code + "' AND ee.emp_no = '" + _emp["empid"].ToString() + "' GROUP BY oe.description");
                            foreach (DataRow _oe in _other_earnings.Rows)
                            {
                                earnings_total += Convert.ToDouble(_oe["total"].ToString());
                                _other_earnings_table.AddCell(new PdfPCell(new Paragraph(_oe["description"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                _other_earnings_table.AddCell(new PdfPCell(new Paragraph(_oe["total"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            }

                            gross_pay += earnings_total;
                            _other_earnings_table.AddCell(new PdfPCell(new Paragraph("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                            _other_earnings_table.AddCell(new PdfPCell(new Paragraph(earnings_total.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                            _thead.AddCell(new PdfPCell(_other_earnings_table));



                            try { night_diff_pay = Convert.ToDouble(emp_payroll.Rows[0]["night_diff_b"].ToString()); } catch { night_diff_pay = 0.00; }
                            gross_pay += night_diff;

                            PdfPTable night_leave = new PdfPTable(2);
                            night_leave.WidthPercentage = 100f;
                            float[] night_leave_night = { 50, 50 };
                            night_leave.SetWidths(night_leave_night);

                            night_leave.AddCell(new PdfPCell(new Paragraph("Night Diff.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            night_leave.AddCell(new PdfPCell(new Paragraph(night_diff_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            night_leave.AddCell(new PdfPCell(new Paragraph("Leaves Ttl.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            night_leave.AddCell(new PdfPCell(new Paragraph(total_leave.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));


                            _thead.AddCell(new PdfPCell(night_leave));
                            _thead.AddCell(new PdfPCell(new Paragraph(gross_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });


                            PdfPTable deductions = new PdfPTable(2);
                            deductions.WidthPercentage = 100f;
                            float[] deductions_width = { 50, 50 };
                            deductions.SetWidths(deductions_width);
                            Double deductions_total = 0;
                            DataTable _deductions = db.QueryBySQLCode("SELECT od.description, SUM(de.amount) as total FROM rssys.hr_other_deductions od  LEFT JOIN rssys.hr_deduction_entry de ON od.code =  de.deduction_code WHERE de.payroll_period = '" + pay_code + "' AND de.emp_no = '" + _emp["empid"].ToString() + "' GROUP BY od.description");
                            foreach (DataRow _d in _deductions.Rows)
                            {
                                deductions_total += Convert.ToDouble(_d["total"].ToString());
                                deductions.AddCell(new PdfPCell(new Paragraph(_d["description"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                deductions.AddCell(new PdfPCell(new Paragraph(_d["total"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            }

                            deductions.AddCell(new PdfPCell(new Paragraph("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            deductions.AddCell(new PdfPCell(new Paragraph(deductions_total.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                            gross_pay -= deductions_total;


                            _thead.AddCell(new PdfPCell(deductions));

                            PdfPTable sss_table = new PdfPTable(2);
                            sss_table.WidthPercentage = 100f;
                            float[] sss_width = { 50, 50 };
                            sss_table.SetWidths(sss_width);

                            DataTable sss = db.QueryBySQLCode("SELECT * FROM rssys.hr_sss WHERE code = '" + _emp["sss_table"].ToString() + "' LIMIT 1");

                            try { er_share = Convert.ToDouble(sss.Rows[0]["empshare_sc"].ToString()); } catch { er_share = 0; }
                            try { ee_share = Convert.ToDouble(sss.Rows[0]["empshare_ec"].ToString()); } catch { ee_share = 0; }
                            try { ec_share = Convert.ToDouble(sss.Rows[0]["s_ec"].ToString()); } catch { ec_share = 0; }

                            gross_pay -= ee_share + er_share;
                            

                            sss_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            sss_table.AddCell(new PdfPCell(new Paragraph(er_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            sss_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            sss_table.AddCell(new PdfPCell(new Paragraph(ee_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            sss_table.AddCell(new PdfPCell(new Paragraph("EC", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            sss_table.AddCell(new PdfPCell(new Paragraph(ec_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            _thead.AddCell(new PdfPCell(sss_table));


                            PdfPTable philhealth_table = new PdfPTable(2);
                            philhealth_table.WidthPercentage = 100f;
                            float[] philhealth_width = { 50, 50 };
                            philhealth_table.SetWidths(philhealth_width);

                            DataTable phic = db.QueryBySQLCode("SELECT * FROM rssys.hr_philhealth WHERE code ='" + _emp["phic_table"].ToString() + "' LIMIT 1");
                            try { phic_er = Convert.ToDouble(phic.Rows[0]["emp_er"].ToString()); } catch { phic_er = 0.00; }
                            try { phic_ee = Convert.ToDouble(phic.Rows[0]["emp_ee"].ToString()); } catch { phic_ee = 0.00; }

                            gross_pay -= phic_ee + phic_er;



                            philhealth_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            philhealth_table.AddCell(new PdfPCell(new Paragraph(phic_er.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            philhealth_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            philhealth_table.AddCell(new PdfPCell(new Paragraph(phic_ee.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));


                            _thead.AddCell(new PdfPCell(philhealth_table));


                            PdfPTable hdmf_table = new PdfPTable(2);
                            hdmf_table.WidthPercentage = 100f;
                            float[] hdmf_width = { 50, 50 };
                            hdmf_table.SetWidths(hdmf_width);

                            Double hdmf_ee = 0.0;
                            Double hdmf_er = 0.0;
                            if (pay_rate < 5000.00)
                            {
                                if (pay_rate <= 1500.00)
                                {
                                    hdmf_ee = (1 / 100) * pay_rate;
                                    hdmf_er = (2 / 100) * pay_rate;

                                }
                                else if (pay_rate > 1500.00)
                                {
                                    hdmf_ee = (2 / 100) * pay_rate;
                                    hdmf_er = (2 / 100) * pay_rate;
                                }
                            }
                            if (pay_rate >= 5000.00)
                            {
                                hdmf_ee = 100;
                                hdmf_er = 100;
                            }


                            hdmf_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            hdmf_table.AddCell(new PdfPCell(new Paragraph(hdmf_er.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            hdmf_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                            hdmf_table.AddCell(new PdfPCell(new Paragraph(hdmf_ee.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                            gross_pay -= hdmf_er + hdmf_ee;

                            _thead.AddCell(new PdfPCell(hdmf_table));

                            try { w_tax = Convert.ToDouble(emp_payroll.Rows[0]["w_tax"].ToString()); } catch { w_tax = 0.00; }
                            _thead.AddCell(new PdfPCell(new Paragraph(w_tax.ToString("0.00"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_RIGHT }) { HorizontalAlignment = Element.ALIGN_RIGHT });

                            gross_pay = gross_pay - w_tax;
                            _thead.AddCell(new PdfPCell(new Paragraph(gross_pay.ToString("N",new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });

                            

                            //DataTable _emp_payroll = db.QueryBySQLCode("SELECT ");

                        }
                    }
                    else // IF SELECTED IS PAYROLL PERIOD ONLY
                    {
                        foreach(DataRow _dept in departments.Rows)
                        {
                            _thead.AddCell(new PdfPCell(new Paragraph(_dept["dept_name"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLDITALIC)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });

                            DataTable _employees = db.QueryBySQLCode("SELECT * FROM rssys.hr_employee WHERE department = '" + _dept["deptid"].ToString() + "'");

                            foreach (DataRow _emp in _employees.Rows)
                            {
                                DataTable emp_dept = db.QueryBySQLCode("SELECT * FROM rssys.hr_department WHERE deptid='" + _emp["department"].ToString() + "' LIMIT 1");
                                DataTable emp_payroll = db.QueryBySQLCode("SELECT * FROM rssys.hr_emp_payroll WHERE empid = '" + _emp["empid"].ToString() + "'");



                                try { pay_rate = Convert.ToDouble(_emp["pay_rate"].ToString()); } catch { pay_rate = 0.00; }
                                try { basic_pay = Convert.ToDouble(emp_payroll.Rows[0]["basic_pay"].ToString()); } catch { basic_pay = 0.00; }
                                try { late_ut = Convert.ToDouble(emp_payroll.Rows[0]["late_ut"].ToString()); } catch { late_ut = 0.00; }
                                try { regular_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["reqular_ot_b"].ToString()); } catch { regular_ot_b = 0.00; }
                                try { dayoff_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["dayoff_ot_b"].ToString()); } catch { dayoff_ot_b = 0.00; }
                                try { legal_hol_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["legal_hol_ot_b"].ToString()); } catch { legal_hol_ot_b = 0.00; }
                                try { special_hol_ot_b = Convert.ToDouble(emp_payroll.Rows[0]["special_hol_ot_b"].ToString()); } catch { special_hol_ot_b = 0.00; }
                                try { legal_hol_pay_b = Convert.ToDouble(emp_payroll.Rows[0]["legal_hol_pay_b"].ToString()); } catch { legal_hol_pay_b = 0.00; }
                                try { spl_hol_pay_b = Convert.ToDouble(emp_payroll.Rows[0]["spl_hol_pay_b"].ToString()); } catch { spl_hol_pay_b = 0.00; }
                                try { night_diff = Convert.ToDouble(emp_payroll.Rows[0]["night_diff_b"].ToString()); } catch { night_diff = 0.00; }
                                try { overtime = regular_ot_b + dayoff_ot_b + legal_hol_ot_b + special_hol_ot_b; } catch { overtime = 0.00; }
                                try { vl_b = Convert.ToDouble(emp_payroll.Rows[0]["vl_b"].ToString()); } catch { vl_b = 0.00; }
                                try { sl_b = Convert.ToDouble(emp_payroll.Rows[0]["sl_b"].ToString()); } catch { sl_b = 0.00; }
                                try { pl_b = Convert.ToDouble(emp_payroll.Rows[0]["pl_b"].ToString()); } catch { pl_b = 0.00; }

                                total_leave = vl_b + sl_b + pl_b;

                                holiday_pay = legal_hol_pay_b + spl_hol_pay_b;

                                gross_pay = basic_pay + night_diff + regular_ot_b + dayoff_ot_b + legal_hol_ot_b + special_hol_ot_b + legal_hol_pay_b + spl_hol_pay_b;

                                gross_pay = gross_pay - late_ut + total_leave;

                                _thead.AddCell(new PdfPCell(new Paragraph(_emp["lastname"].ToString() + ", " + _emp["firstname"].ToString() + " " + _emp["mi"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_LEFT });
                                _thead.AddCell(new PdfPCell(new Paragraph(pay_rate.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                                _thead.AddCell(new PdfPCell(new Paragraph(basic_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                                _thead.AddCell(new PdfPCell(new Paragraph("-" + late_ut.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                                _thead.AddCell(new PdfPCell(new Paragraph(overtime.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });
                                _thead.AddCell(new PdfPCell(new Paragraph(holiday_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });

                                PdfPTable _other_earnings_table = new PdfPTable(2);
                                _other_earnings_table.WidthPercentage = 100f;
                                float[] oe_columnWidths = { 50, 50 };
                                _other_earnings_table.SetWidths(oe_columnWidths);
                                Double earnings_total = 0;
                                DataTable _other_earnings = db.QueryBySQLCode("SELECT oe.description, SUM(ee.amount) as total FROM rssys.hr_other_earnings oe  LEFT JOIN rssys.hr_earning_entry ee ON oe.code =  ee.earning_code WHERE ee.payroll_period = '" + pay_code + "' AND ee.emp_no = '" + _emp["empid"].ToString() + "' GROUP BY oe.description");
                                foreach (DataRow _oe in _other_earnings.Rows)
                                {
                                    try { earnings_total += Convert.ToDouble(_oe["total"].ToString()); } catch { }
                                    _other_earnings_table.AddCell(new PdfPCell(new Paragraph(_oe["description"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                    _other_earnings_table.AddCell(new PdfPCell(new Paragraph(_oe["total"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                }
                                gross_pay += earnings_total;
                                _other_earnings_table.AddCell(new PdfPCell(new Paragraph("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                                _other_earnings_table.AddCell(new PdfPCell(new Paragraph(earnings_total.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                                _thead.AddCell(new PdfPCell(_other_earnings_table));


                                PdfPTable night_leave = new PdfPTable(2);
                                night_leave.WidthPercentage = 100f;
                                float[] night_leave_night = { 50, 50 };
                                night_leave.SetWidths(night_leave_night);

                                night_leave.AddCell(new PdfPCell(new Paragraph("Night Diff.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                night_leave.AddCell(new PdfPCell(new Paragraph(night_diff_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                night_leave.AddCell(new PdfPCell(new Paragraph("Leaves Ttl.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                night_leave.AddCell(new PdfPCell(new Paragraph(total_leave.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));


                                _thead.AddCell(new PdfPCell(night_leave));


                                _thead.AddCell(new PdfPCell(new Paragraph(gross_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_RIGHT });


                                PdfPTable deductions = new PdfPTable(2);
                                deductions.WidthPercentage = 100f;
                                float[] deductions_width = { 50, 50 };
                                deductions.SetWidths(deductions_width);
                                Double deductions_total = 0;
                                DataTable _deductions = db.QueryBySQLCode("SELECT od.description, SUM(de.amount) as total FROM rssys.hr_other_deductions od  LEFT JOIN rssys.hr_deduction_entry de ON od.code =  de.deduction_code WHERE de.payroll_period = '" + pay_code + "' AND de.emp_no = '" + _emp["empid"].ToString() + "' GROUP BY od.description");
                                foreach (DataRow _d in _deductions.Rows)
                                {
                                    try { deductions_total += Convert.ToDouble(_d["total"].ToString()); } catch { }
                                    deductions.AddCell(new PdfPCell(new Paragraph(_d["description"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                    deductions.AddCell(new PdfPCell(new Paragraph(_d["total"].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                }

                                deductions.AddCell(new PdfPCell(new Paragraph("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                deductions.AddCell(new PdfPCell(new Paragraph(deductions_total.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.BOLD))));
                                gross_pay -= deductions_total;

                                _thead.AddCell(new PdfPCell(deductions));

                                PdfPTable sss_table = new PdfPTable(2);
                                sss_table.WidthPercentage = 100f;
                                float[] sss_width = { 50, 50 };
                                sss_table.SetWidths(sss_width);

                                
                                DataTable sss = db.QueryBySQLCode("SELECT * FROM rssys.hr_sss WHERE code = '" + _emp["sss_table"].ToString() + "' LIMIT 1");

                                try { er_share = Convert.ToDouble(sss.Rows[0]["empshare_sc"].ToString()); } catch { er_share = 0; }
                                try { ee_share = Convert.ToDouble(sss.Rows[0]["empshare_ec"].ToString()); } catch { ee_share = 0; }
                                try { ec_share = Convert.ToDouble(sss.Rows[0]["s_ec"].ToString()); } catch { ec_share = 0; }

                                gross_pay -= ee_share + er_share;


                                sss_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                sss_table.AddCell(new PdfPCell(new Paragraph(er_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                sss_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                sss_table.AddCell(new PdfPCell(new Paragraph(ee_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                sss_table.AddCell(new PdfPCell(new Paragraph("EC", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                sss_table.AddCell(new PdfPCell(new Paragraph(ec_share.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                _thead.AddCell(new PdfPCell(sss_table));


                                PdfPTable philhealth_table = new PdfPTable(2);
                                philhealth_table.WidthPercentage = 100f;
                                float[] philhealth_width = { 50, 50 };
                                philhealth_table.SetWidths(philhealth_width);

                                DataTable phic = db.QueryBySQLCode("SELECT * FROM rssys.hr_philhealth WHERE code ='" + _emp["phic_table"].ToString() + "' LIMIT 1");
                                try { phic_er = Convert.ToDouble(phic.Rows[0]["emp_er"].ToString()); } catch { phic_er = 0; }
                                try { phic_ee = Convert.ToDouble(phic.Rows[0]["emp_ee"].ToString()); } catch { phic_ee = 0; }

                                gross_pay -= phic_ee + phic_er;

                                philhealth_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                philhealth_table.AddCell(new PdfPCell(new Paragraph(phic_er.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                philhealth_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                philhealth_table.AddCell(new PdfPCell(new Paragraph(phic_ee.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));


                                _thead.AddCell(new PdfPCell(philhealth_table));


                                PdfPTable hdmf_table = new PdfPTable(2);
                                hdmf_table.WidthPercentage = 100f;
                                float[] hdmf_width = { 50, 50 };
                                hdmf_table.SetWidths(hdmf_width);

                                Double hdmf_ee = 0.0;
                                Double hdmf_er = 0.0;
                                if (pay_rate < 5000.00)
                                {
                                    if (pay_rate <= 1500.00)
                                    {
                                        hdmf_ee = (1 / 100) * pay_rate;
                                        hdmf_er = (2 / 100) * pay_rate;

                                    }
                                    else if (pay_rate > 1500.00)
                                    {
                                        hdmf_ee = (2 / 100) * pay_rate;
                                        hdmf_er = (2 / 100) * pay_rate;
                                    }
                                }
                                if (pay_rate >= 5000.00)
                                {
                                    hdmf_ee = 100;
                                    hdmf_er = 100;
                                }


                                hdmf_table.AddCell(new PdfPCell(new Paragraph("ER", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                hdmf_table.AddCell(new PdfPCell(new Paragraph(hdmf_er.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                hdmf_table.AddCell(new PdfPCell(new Paragraph("EE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));
                                hdmf_table.AddCell(new PdfPCell(new Paragraph(hdmf_ee.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7f, iTextSharp.text.Font.NORMAL))));

                                gross_pay -= hdmf_er + hdmf_ee;

                                _thead.AddCell(new PdfPCell(hdmf_table));

                                try { w_tax = Convert.ToDouble(emp_payroll.Rows[0]["w_tax"].ToString()); } catch { w_tax = 0.00; }
                                _thead.AddCell(new PdfPCell(new Paragraph(w_tax.ToString("0.00"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL)) { Alignment = Element.ALIGN_RIGHT }) { HorizontalAlignment = Element.ALIGN_RIGHT });

                                gross_pay = gross_pay - w_tax;
                                _thead.AddCell(new PdfPCell(new Paragraph(gross_pay.ToString("N", new CultureInfo("en-US")), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD)) { Alignment = Element.ALIGN_CENTER }) { HorizontalAlignment = Element.ALIGN_CENTER });

                            }

                        }
                    }
                }));


                document.Add(_thead);


                document.Close();
                code = db.get_pk("rpt_id");
                col = "rpt_id,filename,date_added,rpt_type";
                val = "'" + code + "','" + filename + "','" + DateTime.Now.ToShortDateString() + "','PAYROLL_SUMMARY'";

                if (db.InsertOnTable(table, col, val))
                {
                    db.set_pkm99("rpt_id", db.get_nextincrementlimitchar(code, 8)); //changes from 'hr_empid'
                    MessageBox.Show("New summary reports created");
                }
                else
                {
                    MessageBox.Show("Failed on saving.");
                }
                pic_loading.Invoke(new Action(() => {
                    pic_loading.Visible = false;
                    btn_submit.Enabled = true;
                }));

                display_list();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            
        }
        private DataTable get_date(String code)
        {
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT date_from,date_to from rssys.hr_payrollpariod where pay_code='" + code + "'");
            }
            catch { }
            return dt;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int r = -1;
            String dtr_filename = "";
            //String sys_dir = "\\\\RIGHTAPPS\\RightApps\\Eastland\\payroll_reports\\payroll_summary\\";
            String sys_dir = fileloc_dtr + "\\ViewController\\RPT\\Payroll\\payroll_summary\\";
            try
            {
                if (dgvl_payroll_summary.Rows.Count > 1)
                {
                    r = dgvl_payroll_summary.CurrentRow.Index;

                    try
                    {
                        dtr_filename = dgvl_payroll_summary["filename", r].Value.ToString();

                        try
                        {
                            System.Diagnostics.Process.Start("iexplore.exe", sys_dir + dtr_filename);

                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Process.Start("chrome.exe", sys_dir + dtr_filename);
                        }
                        catch
                        {
                            System.Diagnostics.Process.Start("iexplore.exe", sys_dir + dtr_filename);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Select a filename");
                    }
                }
                else
                {
                    MessageBox.Show("Files is empty.");
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_deletefile_Click(object sender, EventArgs e)
        {
            int r = -1;
            String dtr_filename = "", rpt_id = "";
            //String sys_dir = "\\\\RIGHTAPPS\\RightApps\\Eastland\\payroll_reports\\payroll_summary\\";
            String sys_dir = fileloc_dtr + "\\ViewController\\RPT\\Payroll\\payroll_summary\\";

            try
            {
                if (dgvl_payroll_summary.Rows.Count > 1)
                {
                    r = dgvl_payroll_summary.CurrentRow.Index;

                    try
                    {
                        dtr_filename = dgvl_payroll_summary["filename", r].Value.ToString();
                        rpt_id = dgvl_payroll_summary["rpt_id", r].Value.ToString();
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this file?", "Confirmation", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            File.Delete(sys_dir + dtr_filename);
                            String query = "DELETE FROM rssys.hr_rpt_files WHERE rpt_id = '" + rpt_id + "' AND rpt_type = 'PAYROLL_SUMMARY'";
                            db.QueryBySQLCode(query);
                            MessageBox.Show("File successfully deleted");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to remove file. It may not exist");
                    }
                }
                else
                {
                    MessageBox.Show("Empty files.");
                }
            }
            catch (Exception ex)
            {

            }
            display_list();
        }
    }
}
