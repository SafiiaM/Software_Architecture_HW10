namespace ClinicDesktop;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.ListView listViewClients;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        this.listViewClients = new System.Windows.Forms.ListView(); // Инициализация ListView
            
            this.SuspendLayout();

            // Настройка параметров для listViewClients
            this.listViewClients.Location = new System.Drawing.Point(12, 12); // Устанавливаем позицию на форме
            this.listViewClients.Name = "listViewClients"; // Имя для компонента
            this.listViewClients.Size = new System.Drawing.Size(776, 426); // Размер ListView
            this.listViewClients.TabIndex = 0; // Идентификатор для табуляции
            this.listViewClients.UseCompatibleStateImageBehavior = false; 
            this.listViewClients.View = System.Windows.Forms.View.Details; // Тип отображения (с деталями)

            // Добавляем колонки для отображения данных клиентов
            this.listViewClients.Columns.Add("ID", 50, HorizontalAlignment.Left);
            this.listViewClients.Columns.Add("Surname", 150, HorizontalAlignment.Left);
            this.listViewClients.Columns.Add("FirstName", 150, HorizontalAlignment.Left);
            this.listViewClients.Columns.Add("Patronymic", 150, HorizontalAlignment.Left);

            // Добавляем listViewClients на форму
            this.Controls.Add(this.listViewClients);

        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Form1";

        this.ResumeLayout(false);
    }



    #endregion
}
