namespace MasterPolApp
{
    partial class SalesHistoryForm
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
            this.dataGridViewSalesHistory = new System.Windows.Forms.DataGridView();
            this.lblPartnerName = new System.Windows.Forms.Label();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelTotals = new System.Windows.Forms.Panel();
            this.btnDeleteSale = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesHistory)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelTotals.SuspendLayout();
            this.SuspendLayout();

            // dataGridViewSalesHistory
            this.dataGridViewSalesHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSalesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSalesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSalesHistory.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewSalesHistory.Name = "dataGridViewSalesHistory";
            this.dataGridViewSalesHistory.Size = new System.Drawing.Size(760, 350);
            this.dataGridViewSalesHistory.TabIndex = 0;

            // lblPartnerName
            this.lblPartnerName.AutoSize = true;
            this.lblPartnerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPartnerName.Location = new System.Drawing.Point(12, 10);
            this.lblPartnerName.Name = "lblPartnerName";
            this.lblPartnerName.Size = new System.Drawing.Size(52, 20);
            this.lblPartnerName.TabIndex = 1;
            this.lblPartnerName.Text = "label1";

            // lblTotalSales
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalSales.Location = new System.Drawing.Point(10, 10);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(140, 15);
            this.lblTotalSales.TabIndex = 2;
            this.lblTotalSales.Text = "Общая сумма продаж:";

            // lblTotalCost
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalCost.Location = new System.Drawing.Point(10, 35);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(125, 15);
            this.lblTotalCost.TabIndex = 3;
            this.lblTotalCost.Text = "Общая себестоимость:";

            // lblTotalProfit
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalProfit.ForeColor = System.Drawing.Color.Green;
            this.lblTotalProfit.Location = new System.Drawing.Point(10, 60);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(97, 15);
            this.lblTotalProfit.TabIndex = 4;
            this.lblTotalProfit.Text = "Общая прибыль:";

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(232)))), ((int)(((byte)(211)))));
            this.panelHeader.Controls.Add(this.lblPartnerName);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(784, 40);
            this.panelHeader.TabIndex = 5;

            // panelTotals
            this.panelTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTotals.Controls.Add(this.lblTotalSales);
            this.panelTotals.Controls.Add(this.lblTotalCost);
            this.panelTotals.Controls.Add(this.lblTotalProfit);
            this.panelTotals.Location = new System.Drawing.Point(12, 420);
            this.panelTotals.Name = "panelTotals";
            this.panelTotals.Size = new System.Drawing.Size(760, 90);
            this.panelTotals.TabIndex = 6;

            // btnDeleteSale
            this.btnDeleteSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDeleteSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSale.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeleteSale.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSale.Location = new System.Drawing.Point(652, 25);
            this.btnDeleteSale.Name = "btnDeleteSale";
            this.btnDeleteSale.Size = new System.Drawing.Size(120, 25);
            this.btnDeleteSale.TabIndex = 7;
            this.btnDeleteSale.Text = "Удалить запись";
            this.btnDeleteSale.UseVisualStyleBackColor = false;
            this.btnDeleteSale.Click += new System.EventHandler(this.btnDeleteSale_Click);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(526, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 25);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // SalesHistoryForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 522);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDeleteSale);
            this.Controls.Add(this.panelTotals);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.dataGridViewSalesHistory);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "SalesHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "История продаж - Мастер Пол";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesHistory)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelTotals.ResumeLayout(false);
            this.panelTotals.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSalesHistory;
        private System.Windows.Forms.Label lblPartnerName;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelTotals;
        private System.Windows.Forms.Button btnDeleteSale;
        private System.Windows.Forms.Button btnRefresh;
    }
}