namespace OptimisationMethods
{
    partial class mainForm
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
            this.labelResults = new System.Windows.Forms.Label();
            this.methodsComboBox = new System.Windows.Forms.ComboBox();
            this.labelMethods = new System.Windows.Forms.Label();
            this.labelFunctions = new System.Windows.Forms.Label();
            this.labelMaxIterations = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.labelStartDirection = new System.Windows.Forms.Label();
            this.labelStartPoint = new System.Windows.Forms.Label();
            this.maxIterationsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.functionsComboBox = new System.Windows.Forms.ComboBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.searchDirectionTextBox = new System.Windows.Forms.TextBox();
            this.startPointTextBox = new System.Windows.Forms.TextBox();
            this.searchMinButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.maxIterationsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(212, 50);
            this.labelResults.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(106, 13);
            this.labelResults.TabIndex = 46;
            this.labelResults.Text = "Вывод результатов";
            // 
            // methodsComboBox
            // 
            this.methodsComboBox.FormattingEnabled = true;
            this.methodsComboBox.Location = new System.Drawing.Point(352, 31);
            this.methodsComboBox.Name = "methodsComboBox";
            this.methodsComboBox.Size = new System.Drawing.Size(375, 21);
            this.methodsComboBox.TabIndex = 45;
            // 
            // labelMethods
            // 
            this.labelMethods.AutoSize = true;
            this.labelMethods.Location = new System.Drawing.Point(212, 31);
            this.labelMethods.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMethods.Name = "labelMethods";
            this.labelMethods.Size = new System.Drawing.Size(80, 13);
            this.labelMethods.TabIndex = 44;
            this.labelMethods.Text = "Выбор метода";
            // 
            // labelFunctions
            // 
            this.labelFunctions.AutoSize = true;
            this.labelFunctions.Location = new System.Drawing.Point(212, 10);
            this.labelFunctions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFunctions.Name = "labelFunctions";
            this.labelFunctions.Size = new System.Drawing.Size(135, 13);
            this.labelFunctions.TabIndex = 43;
            this.labelFunctions.Text = "Выбор тестовой функции";
            // 
            // labelMaxIterations
            // 
            this.labelMaxIterations.AutoSize = true;
            this.labelMaxIterations.Location = new System.Drawing.Point(11, 72);
            this.labelMaxIterations.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMaxIterations.Name = "labelMaxIterations";
            this.labelMaxIterations.Size = new System.Drawing.Size(105, 13);
            this.labelMaxIterations.TabIndex = 42;
            this.labelMaxIterations.Text = "Макс.кол.итераций";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(11, 50);
            this.labelError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(75, 13);
            this.labelError.TabIndex = 41;
            this.labelError.Text = "Погрешность";
            // 
            // labelStartDirection
            // 
            this.labelStartDirection.AutoSize = true;
            this.labelStartDirection.Location = new System.Drawing.Point(11, 31);
            this.labelStartDirection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStartDirection.Name = "labelStartDirection";
            this.labelStartDirection.Size = new System.Drawing.Size(114, 13);
            this.labelStartDirection.TabIndex = 40;
            this.labelStartDirection.Text = "Направление поиска";
            // 
            // labelStartPoint
            // 
            this.labelStartPoint.AutoSize = true;
            this.labelStartPoint.Location = new System.Drawing.Point(11, 10);
            this.labelStartPoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStartPoint.Name = "labelStartPoint";
            this.labelStartPoint.Size = new System.Drawing.Size(93, 13);
            this.labelStartPoint.TabIndex = 39;
            this.labelStartPoint.Text = "Начальная точка";
            // 
            // maxIterationsNumericUpDown
            // 
            this.maxIterationsNumericUpDown.Location = new System.Drawing.Point(136, 70);
            this.maxIterationsNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.maxIterationsNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.maxIterationsNumericUpDown.Name = "maxIterationsNumericUpDown";
            this.maxIterationsNumericUpDown.Size = new System.Drawing.Size(67, 20);
            this.maxIterationsNumericUpDown.TabIndex = 38;
            this.maxIterationsNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // functionsComboBox
            // 
            this.functionsComboBox.FormattingEnabled = true;
            this.functionsComboBox.Location = new System.Drawing.Point(352, 9);
            this.functionsComboBox.Name = "functionsComboBox";
            this.functionsComboBox.Size = new System.Drawing.Size(375, 21);
            this.functionsComboBox.TabIndex = 37;
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(215, 69);
            this.resultTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(516, 51);
            this.resultTextBox.TabIndex = 36;
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(136, 49);
            this.errorTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.Size = new System.Drawing.Size(67, 20);
            this.errorTextBox.TabIndex = 35;
            this.errorTextBox.Text = "0.00001";
            // 
            // searchDirectionTextBox
            // 
            this.searchDirectionTextBox.Location = new System.Drawing.Point(136, 30);
            this.searchDirectionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.searchDirectionTextBox.Name = "searchDirectionTextBox";
            this.searchDirectionTextBox.Size = new System.Drawing.Size(67, 20);
            this.searchDirectionTextBox.TabIndex = 34;
            this.searchDirectionTextBox.Text = "0;1";
            // 
            // startPointTextBox
            // 
            this.startPointTextBox.Location = new System.Drawing.Point(136, 9);
            this.startPointTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.startPointTextBox.Name = "startPointTextBox";
            this.startPointTextBox.Size = new System.Drawing.Size(67, 20);
            this.startPointTextBox.TabIndex = 33;
            this.startPointTextBox.Text = "8;9";
            // 
            // searchMinButton
            // 
            this.searchMinButton.Location = new System.Drawing.Point(91, 95);
            this.searchMinButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchMinButton.Name = "searchMinButton";
            this.searchMinButton.Size = new System.Drawing.Size(111, 26);
            this.searchMinButton.TabIndex = 32;
            this.searchMinButton.Text = "найти минимум";
            this.searchMinButton.UseVisualStyleBackColor = true;
            this.searchMinButton.Click += new System.EventHandler(this.SearchMinButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 132);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.methodsComboBox);
            this.Controls.Add(this.labelMethods);
            this.Controls.Add(this.labelFunctions);
            this.Controls.Add(this.labelMaxIterations);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelStartDirection);
            this.Controls.Add(this.labelStartPoint);
            this.Controls.Add(this.maxIterationsNumericUpDown);
            this.Controls.Add(this.functionsComboBox);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.searchDirectionTextBox);
            this.Controls.Add(this.startPointTextBox);
            this.Controls.Add(this.searchMinButton);
            this.Name = "mainForm";
            this.Text = "Методы оптимизации";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.maxIterationsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.ComboBox methodsComboBox;
        private System.Windows.Forms.Label labelMethods;
        private System.Windows.Forms.Label labelFunctions;
        private System.Windows.Forms.Label labelMaxIterations;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label labelStartDirection;
        private System.Windows.Forms.Label labelStartPoint;
        private System.Windows.Forms.NumericUpDown maxIterationsNumericUpDown;
        private System.Windows.Forms.ComboBox functionsComboBox;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.TextBox errorTextBox;
        private System.Windows.Forms.TextBox searchDirectionTextBox;
        private System.Windows.Forms.TextBox startPointTextBox;
        private System.Windows.Forms.Button searchMinButton;
    }
}

