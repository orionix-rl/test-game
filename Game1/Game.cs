using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    //Класс Game.cs содержит универсальную логику игры для разных размеров поля.
    class Game
    {
        private int size;                   //Переменная содержащая информацию про размер поля. Инициализируется в конструкторе.
        private int[,] map;                 //Массив, который содержит значения кнопок. Инициализируется в конструкторе.
        private int spaceX, spaceY;         //Переменные, содержащие значения координат пустой кнопки.
        static Random rand = new Random();  //Объект класса создан статическим, чтобы решить проблему повторения случайных чисел при их генерации.

        //Конструктор, который принимает значение размера игрового поля.
        public Game(int size)
        {
            if (size < 2) size = 2;             //Проверяем на минимальное значение поля. Минимальный возможный размер - 2х2.
            if (size > 5) size = 5;             //Проверяем на максимальный размер поля. Максимальный возможный размер - 5х5.
            this.size = size;                   //Инициализируем переменную для размера поля.
            this.map = new int[size, size];     //Устанавливаем размер массива.
        }

        //Метод, который подготавливает массив к игре заполняя его числами от 1 до (size*size-1).
        //Последний элемент заполняет как 0. Инициализация координат для пусто кнопки.
        public void Start()
        {
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    map[x, y] = CoordsToPosition(x, y) + 1;
            spaceX = spaceY = size - 1;
            map[spaceX, spaceY] = 0;
        }

        //Метод, который получая позицию кнопки по порядку возвращает ее значение из массива (надпись на кнопке).
        public int GetNumber(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            if (x < 0 || x >= size || y < 0 || y >= size) return 0; //Защита от переполнения массива.
            return map[x, y];
        }

        //Метод, который конвертирует координаты кнопки в ее порядковый номер.
        //Пример для поля 4х4: кнопка с координатами [1,2] будем иметь порядковый номер 6.
        private int CoordsToPosition (int x, int y)
        {
            return y * size + x;
        }

        //Метод, который конвертирует порядковый номер кнопки в ее координаты.
        private void PositionToCoords(int position, out int x, out int y)
        {
            x = position % size;
            y = position / size;
        }

        //Метод для перемещения числа из нажатой кнопки в пустую. ?можно ли сделать try-catch для переполнения массива?
        public void Shift (int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            if (Math.Abs(spaceX - x) + Math.Abs(spaceY - y) != 1) //Проверка на расположение кнопок.
                return;
            map[spaceX, spaceY] = map[x, y];
            map[x, y] = 0;
            spaceX = x;
            spaceY = y;
        }

        //Метод для случайного перемешивания массива.
        public void ShiftRandom ()
        {
            int x = spaceX;
            int y = spaceY;
            switch (rand.Next(0, size))
            {
                case 0: x++; break;
                case 1: x--; break;
                case 2: y++; break;
                case 3: y--; break; 
            }

            //Ловит исключение переполнения.
            try
            {
                if (map[x, y] != null)
                    Shift(CoordsToPosition(x, y));
            }
            catch
            {
                return;
            }
        }

        //Проверяет массив на верный порядок чисел.
        public bool CheckNumbers()
        {
            if (!(spaceX == size - 1 && spaceY == size - 1))
                return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (CoordsToPosition(x, y) + 1 != size * size)
                        if (map[x, y] != CoordsToPosition(x, y) + 1)
                            return false;
            return true;
        }
    }
}
