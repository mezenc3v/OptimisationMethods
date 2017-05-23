using OptimisationMethods.Entities;
using System;
using System.Windows.Forms;

namespace OptimisationMethods
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //выставляем параметры
            functionsComboBox.Items.AddRange(new object[] {
            "(1) 2x^2 + 3e^(–x)",
            "(2) –e^(–х)ln x",
            "(3) 2x^2 – e^x",
            "(4) x^4 – 14x^3 + 60x^2 – 70x",
            "(5) 4x^3 – 3x^4, если x ≥ 0, 4x^3 + 3x^4, если x < 0",
            "(6) x^2 + 2x",
            "(7) 2x2 + 16/x",
            "(8) (10x^3 + 3x^2 + x + 5)^2",
            "(9) 3x^2 + (12/x^3) – 5",
            "(10) (x1)^2 + 3(x2)^2 + 2(x1)(x2)",
            "(11) 100((x2) – (x1)^2)^2 + (1 – (x1))^2",
            "(12) –12(x2) + 4(x1)^2 + 4(x2)^2 – 4(x1)(x2)",
            "(13) ((x1) – 2)^4 + ((x1) – 2(x2))^2",
            "(14) 4((x1) – 5)^2 + ((x2) – 6)^2",
            "(15) ((x1) – 2)^4 + ((x1) – 2(x2))^2",
            "(16) 2(x1)^3 + 4(x1)(x2)^3 – 10(x1)(x2) + (x2)^2",
            "(17) 8(x1)^2 + 4(x1)(x2) + 5(x2)^2",
            "(18) 4((x1) – 5)^2 + ((x2) – 6)^2",
            "(19) 100((x2) – (x1)^2)^2 + (1 – (x1))^2",
            "(20) ((x1) – 1)^2 + ((x2) – 3)^2 + 4((x3) + 5)^2",
            "(21) 8(x1)^2 + 4(x1)(x2) + 5(x2)^2",
            "(22) 4((x1) – 5)^2 + ((x2) – 6)^2",
            "(23) ((x2) – (x12))^2 + (1 –( x1))^2",
            "(24) ((x2) – (x1)^2)^2 + 100(1 – (x1)^2)^2",
            "(25) 3((x1) – 4)^2 + 5((x2) + 3)^2 + 7(2(x3) + 1)^2",
            "(26) (x1)^3 + (x2)^2 – (3x1) – 2(x2) + 2",
            "(27) –12x2 + 4x1^2 + 4x2^2 – 4x1x2",
            "(28) (x1 – 2)^4 + (x1 – 2x2)^2",
            "(29) (x1x2x3 – 1)^2 + 5[x3(x1 + x2) – 2]^2 + 2(x1 + x2 + x3 – 3)^2",
            "(30) 4x1^2 + 3x2^2 – 4x1x2^2 + x1",
            "(31) (x12 + x2 – 11)^2 + (x1 + x2^2 – 7)^2",
            "(32) 100(x2 – x1^3)^2 + (1 – x1)^2",
            "(33) [1.5 – x1(1 – x2)]^2 + [2.25 – x1(1 – x2^2)]^2 +[2.625 – x1(1 – x2^3)]^2",
            "(34) (x1 + 10x2)^2 + 5(x3 – x4)^2 + (x2 – 2x^3)^4 + 10(x1 – x4)^4",
            "(35) 100(x2 – x12)^2 + (1 – x1)^2 + 90(x4 – x3^2)^2 + (1 – x3)^3 +10.1[(x2 – 1)^2" +
                " + (x4 – 1)^2] + 19.8(x2 – 1)(x4 – 1)",
            "(36) (2x1^2 + 3x2^2)exp(x1^2 – x2^2)",
            "(37) 0.1(12 + x1^2 + (1 + x2^2)/x1^2 + (x1^2x2^2 + 100)/(x1^4x2^4))",
            "(38) 100[x3 – 0.25(x1 + x2)^2]^2 + (1 – x1)^2 + (1 – x2)^2",
            "(1) 4x1^2 + x2^2 – 12x2 + 4",
            "(2) x1^2 + 2 * x2^2 + 5 * x3^2 - 2 * x1 * x2 - 4 * x2 * x3 - 2 * x3",
            "(3) x1^2 + 3*x2^2 + 3*x1*x2 + x1",
            "(4) x1^2 + 2*x2^2 - 2*x1*x2 + x1",
            "(5) 2*x1^2 + x2^2 - 2*x1*x2 + x2",
            "(6) 2*x1^2 + 2*x2^2 - x1*x2 + x1 + 10"});
            methodsComboBox.Items.AddRange(new object[] {
            "Дихотомия",
            "Дихотомия-ДСК",
            "Коши",
            "Циклический покоординатный спуск",
            "Партан1",
            "Нелдера-Мида",
            "Хука-Дживса",
            "Дэвидона-Флетчера-Пауэлла",
            "Бройдена-Флетчера-Шенно",
            "Бройдена-Флетчера-Гольдфарба-Шенно",
            "Мак-Кормика",
            "Бройдена",
            "Пирсона-2",
            "Пирсона-3",
            "Проекции Заутендийка",
            "Даниеля",
            "Полака-Рибьера",
            "Флэтчера-Ривса",
            "Диксона",
            "Партан-2",
            "Ньютона",
            "Лабораторная работа 3",
            "Розенброка",
            "Пауэлла 2"});

            methodsComboBox.SelectedIndex = 22;
            functionsComboBox.SelectedIndex = 21;
        }

        private void SearchMinButton_Click(object sender, EventArgs e)
        {
            try
            {
                //поиск минимума выбранным методом
                SearchResults results = SearchPoint();
                //вывод результатов
                resultTextBox.Text = "Количество итераций: "
                    + results.Iterations + " , минимум: ";
                foreach (double min in results.EndPoint.matrix)
                {
                    resultTextBox.Text += min.ToString("F7") + " ; ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public SearchResults SearchPoint()
        {
            //заполнение входных параметров
            int functionIndex = functionsComboBox.SelectedIndex;
            int indexMethod = methodsComboBox.SelectedIndex;
            double error = Convert.ToDouble(errorTextBox.Text);
            int maxIteration = (int)maxIterationsNumericUpDown.Value;
            //сепаратор точек
            string[] startPointString = (startPointTextBox.Text).Split(';');
            string[] startDirectionString = (searchDirectionTextBox.Text).Split(';');
            Point startPoint = new Point(1, startPointString.Length, functionIndex);
            Point startDirection = new Point(1, startPointString.Length, functionIndex);
            for (int i = 0; i < startPointString.Length; i++)
            {
                if (i < startPointString.Length)
                {
                    startPoint.matrix[0, i] = Double.Parse(startPointString[i]);
                }
                if (i < startDirectionString.Length)
                {
                    startDirection.matrix[0, i] = Double.Parse(startDirectionString[i]);
                }
            }
            //создаем экземпляр сущности поиска
            Search search = new Search(startPoint, startDirection, error, functionIndex, maxIteration, indexMethod);
            //ищем выбранным методом
            SearchResults results = Methods.Method(search);
            //=возвращаем результаты
            return results;
        }
    }
}
