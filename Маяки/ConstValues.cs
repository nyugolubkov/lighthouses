using System;

namespace Маяки
{
    /// <summary>
    /// Статический класс, хранящий константные значения
    /// </summary>
    public static class ConstValues
    {
        public const string BackGroundPath = @"..\..\Resources\Images\Waves.jpg";
        public const string LightHousePath = @"..\..\Resources\Images\Lighthouse.png";
        public const string LevelsDirectoryPath = @"..\..\Levels";
        public const string BoatPath = @"..\..\Resources\Images\Boat.png";
        public const string UnAvailPath = @"..\..\Resources\Images\UnAvail.png";
        public const string CrossesPath = @"..\..\Resources\Images\Crosses.png";
        public const string GameOverSoundPath = @"..\..\Resources\Sounds\GameOver.wav";
        public const string WinSoundPath = @"..\..\Resources\Sounds\Win.wav";
        public const string RecordsPath = @"..\..\Resources\Records.json";
        public const string ContinueLastGame = "Хотели бы вы продолжить последнюю игру?";
        public const string ContinueTitle = "Продолжить игру?";
        public const string TrialsText = "Количество оставшихся попыток: ";
        public static string InfoText => "Программа была разработана" + Environment.NewLine +
            "студентом 1-го курса" + Environment.NewLine +
            "Факультета Компьютерных Наук" + Environment.NewLine +
            "ОП \"Программная инженерия\"" + Environment.NewLine +
            "Голубковым Никитой Юрьевичем.";
        public static string MiniRulesText => "Нажатие левой кнопкой мыши" + Environment.NewLine +
            "по клетке циклически меняет ее" + Environment.NewLine +
            "состояние пустая - кораблик -" + Environment.NewLine +
            "крестик (нет корабля) - пустая;" + Environment.NewLine + Environment.NewLine + 
            "При нажатии левой кнопкой мыши" + Environment.NewLine +
            "на клетку с маяком, состояние " + Environment.NewLine +
            "всех клеток вокруг маяка переходит" + Environment.NewLine +
            "в \"крестик\".";
        public static string Rules => "   На каждом игровом поле размера NxN " +
            "спрятано N кораблей. Каждый корабль занимает одну клетку. " +
            "В клетках с цифрами расположены маяки, освещающие корабли. " +
            "Цифры указывают число кораблей, освещаемых данным маяком " +
            "(т.е.суммарное количество кораблей в строке и столбце, пересекающихся в данной клетке). " +
            "Каждый маяк светит по горизонтали и вертикали, но не по диагонали. " +
            "Корабли, расположенные на одной вертикали или горизонтали, " +
            "не заслоняют друг друга от света маяка. Корабли не могут касаться друг друга и маяков, " +
            "не могут располагаться на мелях (клетках с волнистыми линиями). " + Environment.NewLine +
            "   Задача состоит в определении расположения всех кораблей. " + Environment.NewLine +
            "   В игре присутствует 3 уровня сложности: Новичок, Совершенствующийся " +
            "и Опытный игрок. " + Environment.NewLine +
            "   Новичку дается 5 попыток на прохождение уровня и не дается возможности " +
            "ставить кораблики вокруг маяков и других кораблей. " +
            "Совершенствующемуся же дается 4 попытки на прохождение игры и во время проверки " +
            "указываются неправильные компоненты. Опытному же игроку дается 3 попытки и " +
            "не дается никаких других подсказок.";
    }
}
