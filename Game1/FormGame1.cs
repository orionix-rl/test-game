using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class FormGame1 : Form
    {
        Game game; //Создание экземпляра класса Game.cs

        public FormGame1()
        {
            InitializeComponent();
            game = new Game(4); //Инициализация экземпляра класса Game.cs
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt16(((Button)sender).Tag); //Определяем номер кнопки по ее тегу (0-15).
            game.Shift(position);
            refresh();
            if(game.CheckNumbers())
            {
                MessageBox.Show("Победа!");
                StartGame();
            }
        }

        //Событие клика по меню.
        private void menu_start_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        //Событие загрузки формы.
        private void FormGame1_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        //Функция возвращает объект той кнопки, которая нажата.
        private Button button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        //Метод отвечающий за начальное заполение массива и заполнение значений в сетке.
        private void StartGame()
        {
            game.Start();
            for (int i = 0; i < 200; i++)
                game.ShiftRandom();
            refresh();
        }

        //Метод, который обновляет значения на кнопках в доставая их из массива в классе Game.cs
        private void refresh ()
        {
            for (int position = 0; position < 16; position++)
            {
                int num = game.GetNumber(position);
                button(position).Text = num.ToString();
                button(position).Visible = (num > 0); //Вернет false при значении ноль и скроет кнопку с этим значением.
            }
        }
    }
}
