namespace CG_LAB2
{
    partial class Form1
    {
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button algorithmButton;
        //private System.Windows.Forms.TextBox scanLinesTextBox;

        /// <summary>
        /// Метод инициализации компонентов формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.algorithmButton = new System.Windows.Forms.Button();
            //this.scanLinesTextBox = new System.Windows.Forms.TextBox();
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
            this.algorithmButton.Text = "Загрузка многоугольников из списка";
            this.algorithmButton.UseVisualStyleBackColor = true;
            this.algorithmButton.Click += new System.EventHandler(this.algorithmButton_Click); // Привязываем событие клика
            // 
            // scanLinesTextBox
            // 
           /* this.scanLinesTextBox.Location = new System.Drawing.Point(12, 12); // Устанавливаем расположение
            this.scanLinesTextBox.Name = "scanLinesTextBox"; // Имя TextBox
            this.scanLinesTextBox.Size = new System.Drawing.Size(100, 20); // Размер TextBox
            this.scanLinesTextBox.TabIndex = 0; // Индекс для табуляции*/
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1600, 1200);
            this.Controls.Add(this.drawButton); // Добавляем кнопку на форму
            this.Controls.Add(this.loadButton); // Добавляем кнопку на форму
            this.Controls.Add(this.algorithmButton); // Добавляем кнопку на форму
            //this.Controls.Add(this.scanLinesTextBox); // Добавляем TextBox в форму
            this.Name = "Form1";
            this.Text = "3D Polygon Renderer";
            this.ResumeLayout(false);
        }
    }
}
