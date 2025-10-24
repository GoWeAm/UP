namespace MasterPolApp
{
    partial class PartnerMainForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPartnerInfo = new System.Windows.Forms.Label();
            this.btnCalculateDiscount = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(232)))), ((int)(((byte)(211)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(500, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(299, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Панель управления партнера";
            // 
            // lblPartnerInfo
            // 
            this.lblPartnerInfo.AutoSize = true;
            this.lblPartnerInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPartnerInfo.Location = new System.Drawing.Point(12, 80);
            this.lblPartnerInfo.Name = "lblPartnerInfo";
            this.lblPartnerInfo.Size = new System.Drawing.Size(72, 20);
            this.lblPartnerInfo.TabIndex = 1;
            this.lblPartnerInfo.Text = "Партнер:";
            // 
            // btnCalculateDiscount
            // 
            this.btnCalculateDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.btnCalculateDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculateDiscount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCalculateDiscount.ForeColor = System.Drawing.Color.White;
            this.btnCalculateDiscount.Location = new System.Drawing.Point(50, 130);
            this.btnCalculateDiscount.Name = "btnCalculateDiscount";
            this.btnCalculateDiscount.Size = new System.Drawing.Size(400, 50);
            this.btnCalculateDiscount.TabIndex = 2;
            this.btnCalculateDiscount.Text = "Расчет скидки";
            this.btnCalculateDiscount.UseVisualStyleBackColor = false;
            this.btnCalculateDiscount.Click += new System.EventHandler(this.btnCalculateDiscount_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(50, 200);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(400, 50);
            this.btnCreateOrder.TabIndex = 3;
            this.btnCreateOrder.Text = "Создать заявку";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.btnViewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOrders.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnViewOrders.ForeColor = System.Drawing.Color.White;
            this.btnViewOrders.Location = new System.Drawing.Point(50, 270);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(400, 50);
            this.btnViewOrders.TabIndex = 4;
            this.btnViewOrders.Text = "Мои заявки";
            this.btnViewOrders.UseVisualStyleBackColor = false;
     
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(50, 340);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(400, 50);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // PartnerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnViewOrders);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.btnCalculateDiscount);
            this.Controls.Add(this.lblPartnerInfo);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "PartnerMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Панель партнера - Мастер Пол";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPartnerInfo;
        private System.Windows.Forms.Button btnCalculateDiscount;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Button btnExit;
    }
}