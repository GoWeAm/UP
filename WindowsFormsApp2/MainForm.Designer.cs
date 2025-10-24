namespace MasterPolApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridViewPartners = new System.Windows.Forms.DataGridView();
            this.btnAddPartner = new System.Windows.Forms.Button();
            this.btnEditPartner = new System.Windows.Forms.Button();
            this.btnDeletePartner = new System.Windows.Forms.Button();
            this.btnSalesReport = new System.Windows.Forms.Button();
            this.btnMaterialCalculation = new System.Windows.Forms.Button();
            this.btnViewSalesHistory = new System.Windows.Forms.Button();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPartners)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPartners
            // 
            this.dataGridViewPartners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPartners.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPartners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPartners.Location = new System.Drawing.Point(12, 100);
            this.dataGridViewPartners.Name = "dataGridViewPartners";
            this.dataGridViewPartners.Size = new System.Drawing.Size(960, 425);
            this.dataGridViewPartners.TabIndex = 0;
            // 
            // btnAddPartner
            // 
            this.btnAddPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnAddPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPartner.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddPartner.ForeColor = System.Drawing.Color.White;
            this.btnAddPartner.Location = new System.Drawing.Point(12, 60);
            this.btnAddPartner.Name = "btnAddPartner";
            this.btnAddPartner.Size = new System.Drawing.Size(120, 30);
            this.btnAddPartner.TabIndex = 1;
            this.btnAddPartner.Text = "Добавить партнера";
            this.btnAddPartner.UseVisualStyleBackColor = false;
            this.btnAddPartner.Click += new System.EventHandler(this.btnAddPartner_Click);
            // 
            // btnEditPartner
            // 
            this.btnEditPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnEditPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPartner.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEditPartner.ForeColor = System.Drawing.Color.White;
            this.btnEditPartner.Location = new System.Drawing.Point(178, 60);
            this.btnEditPartner.Name = "btnEditPartner";
            this.btnEditPartner.Size = new System.Drawing.Size(120, 30);
            this.btnEditPartner.TabIndex = 2;
            this.btnEditPartner.Text = "Редактировать";
            this.btnEditPartner.UseVisualStyleBackColor = false;
            this.btnEditPartner.Click += new System.EventHandler(this.btnEditPartner_Click);
            // 
            // btnDeletePartner
            // 
            this.btnDeletePartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDeletePartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePartner.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeletePartner.ForeColor = System.Drawing.Color.White;
            this.btnDeletePartner.Location = new System.Drawing.Point(327, 60);
            this.btnDeletePartner.Name = "btnDeletePartner";
            this.btnDeletePartner.Size = new System.Drawing.Size(120, 30);
            this.btnDeletePartner.TabIndex = 3;
            this.btnDeletePartner.Text = "Удалить";
            this.btnDeletePartner.UseVisualStyleBackColor = false;
            this.btnDeletePartner.Click += new System.EventHandler(this.btnDeletePartner_Click);
            // 
            // btnSalesReport
            // 
            this.btnSalesReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnSalesReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesReport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSalesReport.ForeColor = System.Drawing.Color.White;
            this.btnSalesReport.Location = new System.Drawing.Point(484, 60);
            this.btnSalesReport.Name = "btnSalesReport";
            this.btnSalesReport.Size = new System.Drawing.Size(120, 30);
            this.btnSalesReport.TabIndex = 4;
            this.btnSalesReport.Text = "Отчет по продажам";
            this.btnSalesReport.UseVisualStyleBackColor = false;
            this.btnSalesReport.Click += new System.EventHandler(this.btnSalesReport_Click);
            // 
            // btnMaterialCalculation
            // 
            this.btnMaterialCalculation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnMaterialCalculation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialCalculation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMaterialCalculation.ForeColor = System.Drawing.Color.White;
            this.btnMaterialCalculation.Location = new System.Drawing.Point(631, 60);
            this.btnMaterialCalculation.Name = "btnMaterialCalculation";
            this.btnMaterialCalculation.Size = new System.Drawing.Size(150, 30);
            this.btnMaterialCalculation.TabIndex = 5;
            this.btnMaterialCalculation.Text = "Расчет материалов";
            this.btnMaterialCalculation.UseVisualStyleBackColor = false;
            this.btnMaterialCalculation.Click += new System.EventHandler(this.btnMaterialCalculation_Click);
            // 
            // btnViewSalesHistory
            // 
            this.btnViewSalesHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnViewSalesHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewSalesHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnViewSalesHistory.ForeColor = System.Drawing.Color.White;
            this.btnViewSalesHistory.Location = new System.Drawing.Point(807, 60);
            this.btnViewSalesHistory.Name = "btnViewSalesHistory";
            this.btnViewSalesHistory.Size = new System.Drawing.Size(150, 30);
            this.btnViewSalesHistory.TabIndex = 6;
            this.btnViewSalesHistory.Text = "История продаж";
            this.btnViewSalesHistory.UseVisualStyleBackColor = false;
            this.btnViewSalesHistory.Click += new System.EventHandler(this.btnViewSalesHistory_Click);
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserRole.Location = new System.Drawing.Point(12, 35);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(37, 15);
            this.lblUserRole.TabIndex = 7;
            this.lblUserRole.Text = "Роль:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(232)))), ((int)(((byte)(211)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(984, 30);
            this.panelHeader.TabIndex = 8;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(314, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Управление партнерами - Мастер Пол";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 537);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblUserRole);
            this.Controls.Add(this.btnViewSalesHistory);
            this.Controls.Add(this.btnMaterialCalculation);
            this.Controls.Add(this.btnSalesReport);
            this.Controls.Add(this.btnDeletePartner);
            this.Controls.Add(this.btnEditPartner);
            this.Controls.Add(this.btnAddPartner);
            this.Controls.Add(this.dataGridViewPartners);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мастер Пол - Управление партнерами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPartners)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPartners;
        private System.Windows.Forms.Button btnAddPartner;
        private System.Windows.Forms.Button btnEditPartner;
        private System.Windows.Forms.Button btnDeletePartner;
        private System.Windows.Forms.Button btnSalesReport;
        private System.Windows.Forms.Button btnMaterialCalculation;
        private System.Windows.Forms.Button btnViewSalesHistory;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
    }
}