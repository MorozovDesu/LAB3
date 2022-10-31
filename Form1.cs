using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Температура, задаваемая в виде пары (значение, тип), 
//допустимые типы:
//градус Цельсия,
//градус Фаренгейта,
//градус Ранкина,
//Кельвины

//сложение
//вычитание
//умножение на число
//сравнение
//вывод значения в любом типе

namespace LAB3_свой_тип
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var measureItems = new string[]
        {
            "c",
            "f",
            "r",
            "k",
        };

            // привязываем списки значений к каждому комбобоксу
            cmbFirstType.DataSource = new List<string>(measureItems);
            cmbSecondType.DataSource = new List<string>(measureItems);
            cmbResultType.DataSource = new List<string>(measureItems);
        }
        private MeasureType GetMeasureType(ComboBox comboBox)
        {
            MeasureType measureType;
            switch (comboBox.Text)
            {
                case "c":
                    measureType = MeasureType.c;
                    break;
                case "f":
                    measureType = MeasureType.f;
                    break;
                case "r":
                    measureType = MeasureType.r;
                    break;
                case "k":
                    measureType = MeasureType.k;
                    break;
                default:
                    measureType = MeasureType.c;
                    break;
            }
            return measureType;
        }
        private void Calculate()
        {
            try
            {
                var firstValue = double.Parse(txtFirst.Text);
                var secondValue = double.Parse(txtSecond.Text);

                // вместо трех страшных свитчей, три вызова нашей новой функции
                MeasureType firstType = GetMeasureType(cmbFirstType);
                MeasureType secondType = GetMeasureType(cmbSecondType);
                MeasureType resultType = GetMeasureType(cmbResultType);

                // тут сразу тип полученный передаем в момент создания экземпляра класса
                var firstLength = new Temperatura(firstValue, firstType);
                var secondLength = new Temperatura(secondValue, secondType);

                Temperatura sumLength = null;

                switch (cmbOperation.Text)
                {
                    case "+":
                        sumLength = firstLength + secondLength;
                        break;
                    case "-":
                        sumLength = firstLength - secondLength;
                        break;
                    case "*":
                        sumLength = firstLength * secondLength;
                        break;
                    case "/":
                        sumLength = firstLength / secondLength;
                        break;
                    default:
                        sumLength = firstLength + secondLength;
                        break;

                }

                // тут конвертируем через To(resultType) в указанный тип
                txtResult.Text = sumLength.To(resultType).Verbose();
            }
            catch (FormatException)
            {
                // если тип преобразовать не смогли
            }
        }

        
        private void onValueChanged(object sender, EventArgs e)
        {
            // вызов функции все тот же
            Calculate();
        }

        private void cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}
