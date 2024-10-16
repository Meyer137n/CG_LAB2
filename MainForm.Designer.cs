using System.Drawing;
using System;

namespace CG_LAB2
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonDeffaultPosition = new System.Windows.Forms.Button();
            //this.buttonAlgorithm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDeffaultPosition
            // 
            this.buttonDeffaultPosition.Location = new System.Drawing.Point(60, 50);
            this.buttonDeffaultPosition.Name = "buttonDeffaultPosition";
            this.buttonDeffaultPosition.Size = new System.Drawing.Size(183, 30);
            this.buttonDeffaultPosition.TabIndex = 0;
            this.buttonDeffaultPosition.Text = "Начльная позиция";
            this.buttonDeffaultPosition.UseVisualStyleBackColor = true;
            this.buttonDeffaultPosition.Click += new System.EventHandler(this.buttonDeffaultPosition_Click);
            //
            // buttonAlgorithm
            //
/*            this.buttonAlgorithm.Location = new System.Drawing.Point(60, 100);
            this.buttonAlgorithm.Name = "buttonAlgorithm";
            this.buttonAlgorithm.Size = new System.Drawing.Size(183, 30);
            this.buttonAlgorithm.TabIndex = 0;
            this.buttonAlgorithm.Text = "Применить алгоритм";
            this.buttonAlgorithm.UseVisualStyleBackColor = true;
            this.buttonAlgorithm.Click += new System.EventHandler(this.buttonApplyAlgorithm_Click);*/
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 572);
            this.Controls.Add(this.buttonDeffaultPosition);
            //this.Controls.Add(this.buttonAlgorithm);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Афинные преобразования";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDeffaultPosition;
        //private System.Windows.Forms.Button buttonAlgorithm;
    }
}


