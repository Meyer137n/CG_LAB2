namespace CG_LAB2
{
    partial class Form1
    {
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button algorithmButton;

        /// <summary>
        /// Метод инициализации компонентов формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.algorithmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(20, 20); // Положение кнопки
            this.drawButton.Size = new System.Drawing.Size(120, 70); // Размер кнопки
            this.drawButton.Text = "Алгоритм интервального построчного сканирования";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click); // Привязываем событие клика
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(150, 20); // Положение кнопки
            this.loadButton.Size = new System.Drawing.Size(120, 70); // Размер кнопки
            this.loadButton.Text = "Загрузка многоугольников из списка";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click); // Привязываем событие клика
            // 
            // algorithmButton
            // 
            this.algorithmButton.Location = new System.Drawing.Point(280, 20); // Положение кнопки
            this.algorithmButton.Size = new System.Drawing.Size(120, 70); // Размер кнопки
            this.algorithmButton.Text = "Обработка невидимых частей";
            this.algorithmButton.UseVisualStyleBackColor = true;
            this.algorithmButton.Click += new System.EventHandler(this.algorithmButton_Click); // Привязываем событие клика                                                                                   
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1800, 1300);
            this.Controls.Add(this.drawButton); // Добавляем кнопку на форму
            this.Controls.Add(this.loadButton); // Добавляем кнопку на форму
            this.Controls.Add(this.algorithmButton); // Добавляем кнопку на форму
            this.Name = "Form1";
            this.Text = "Обработка скрытых граней";
            this.ResumeLayout(false);
        }
    }
}
