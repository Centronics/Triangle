using System;
using System.Windows.Forms;

namespace Triangle
{
    public partial class Triangle : Form
    {
        string[] IgnoreStrs = { "e+", "e-", "e" };
        string StrA = string.Empty, StrB = string.Empty;

        public Triangle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вычисляет площадь прямоугольного треугольника
        /// </summary>
        /// <param name="A">Сторона А</param>
        /// <param name="B">Сторона Б</param>
        /// <returns>Возвращает площадь прямоугольного треугольника.
        /// В случае задания некорректных значений (меньше нуля) выбрасывается исключение ArgumentOutOfRangeException</returns>
        public static double TriangleSquare(double A, double B)
        {
            if (A < 0.0)
                throw new ArgumentOutOfRangeException("A", A, "Сторона А имеет некорректное значение");
            if (B < 0.0)
                throw new ArgumentOutOfRangeException("Б", B, "Сторона Б имеет некорректное значение");
            return (A * B) / 2.0;
        }

        /// <summary>
        /// Возвращает сторону А
        /// </summary>
        public double SideA
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(txtA.Text))
                        return 0.0;
                    foreach (string str in IgnoreStrs)
                        if (txtA.Text.EndsWith(str, StringComparison.OrdinalIgnoreCase))
                            return Convert.ToDouble(txtA.Text.Remove(txtA.Text.Length - str.Length));
                    return Convert.ToDouble(txtA.Text);
                }
                catch (Exception ex)
                {
                    throw new ArgumentOutOfRangeException("Сторона А", txtA.Text, ex.Message);
                }
            }
        }

        /// <summary>
        /// Возвращает сторону Б
        /// </summary>
        public double SideB
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(txtB.Text))
                        return 0.0;
                    foreach (string str in IgnoreStrs)
                        if (txtB.Text.EndsWith(str, StringComparison.OrdinalIgnoreCase))
                            return Convert.ToDouble(txtB.Text.Remove(txtB.Text.Length - str.Length));
                    return Convert.ToDouble(txtB.Text);
                }
                catch (Exception ex)
                {
                    throw new ArgumentOutOfRangeException("Сторона Б", txtB.Text, ex.Message);
                }
            }
        }

        /// <summary>
        /// Вызывается после ввода параметров. Исключения не выбрасываются
        /// </summary>
        void ParsInput()
        {
            try
            {
                double S = TriangleSquare(SideA, SideB);
                if (S < 0.0)
                {
                    MessageBox.Show(this, "Площадь не может быть меньше нуля: Ошибка в функции - программа будет закрыта", "Неизвестная ошибка");
                    Application.Exit();
                }
                txtResult.Text = (S > 0.0) ? S.ToString() : string.Empty;
                StrA = txtA.Text;
                StrB = txtB.Text;
            }//Порядок обработчиков должен быть именно такой, какой представлен ниже, т.к. в C# порядок обработчиков имеет значение
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка");
                txtA.Text = StrA;
                txtB.Text = StrB;
                if (txtA.Focused)
                    txtA.SelectionStart = txtA.Text.Length;
                else
                    if (txtB.Focused)
                        txtB.SelectionStart = txtB.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Неизвестная ошибка");
            }
        }

        /// <summary>
        /// Происходит при изменении параметра "Сторона А"
        /// </summary>
        /// <param name="sender">Ссылка на вызывающий элемент</param>
        /// <param name="e">Аргументы события</param>
        private void txtA_TextChanged(object sender, EventArgs e)
        {
            ParsInput();
        }

        /// <summary>
        /// Происходит при изменении параметра "Сторона Б"
        /// </summary>
        /// <param name="sender">Ссылка на вызывающий элемент</param>
        /// <param name="e">Аргументы события</param>
        private void txtB_TextChanged(object sender, EventArgs e)
        {
            ParsInput();
        }

        /// <summary>
        /// Происходит при отпускании клавиши над формой
        /// </summary>
        /// <param name="sender">Ссылка на вызывающий элемент</param>
        /// <param name="e">Аргументы события</param>
        private void Triangle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}