using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для Records.xaml
    /// Элемент управления, на котором изображен список рекордов
    /// для каждой сложности и каждого размера поля, наследник класса UserControl
    /// </summary>
    public partial class Records : UserControl
    {
        private readonly ContentControl control;
        private List<List<List<string>>> record;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public Records(object sender)
        {
            InitializeComponent();

            control = (ContentControl)sender;

            recordsTextBlock.Text = GenerateText();
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку возвращения в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }

        /// <summary>
        /// Метод, десериализующий сохраненные рекорды
        /// </summary>
        /// <returns>Коллекция рекордов</returns>
        public static List<List<List<string>>> RecordDeserializer()
        {
            List<List<List<string>>> records;

            DataContractJsonSerializer ser = 
                new DataContractJsonSerializer(typeof(List<List<List<string>>>));
            try
            {
                using (FileStream fs = new FileStream(ConstValues.RecordsPath, FileMode.Open))
                {
                    records = (List<List<List<string>>>)ser.ReadObject(fs);
                }
                
                if (records == null || records.Count() != 4)
                    throw new ArgumentNullException();

                for (int i = 0; i < 4; i++)
                {
                    if (records[i].Count() != 3)
                        throw new ArgumentNullException();

                    for (int j = 0; j < 3; j++)
                    {
                        if (records[i][j].Count() != 3)
                            throw new ArgumentNullException();
                    }
                }
            }
            catch (Exception e)
            {
                records = new List<List<List<string>>>();

                for (int i = 0; i < 4; i++)
                {
                    List<List<string>> rec1 = new List<List<string>>();

                    for (int j = 0; j < 3; j++)
                    {
                        List<string> rec2 = new List<string>();

                        for (int k = 0; k < 3; k++)
                        {
                            rec2.Add(null);
                        }

                        rec1.Add(rec2);
                    }

                    records.Add(rec1);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        records[i][j][k] = (records[i][j][k] == null)
                            ? "-" : records[i][j][k];
                    }
                }
            }

            return records;
        }

        /// <summary>
        /// Метод, генерирующий текст для вывода на TextBlock
        /// </summary>
        /// <returns>Сгенерированный текст</returns>
        private string GenerateText()
        {
            record = RecordDeserializer();

            return "Достижения участников:" + Environment.NewLine +
                GenerateFieldText(6, 0) + Environment.NewLine +
                GenerateFieldText(8, 1) + Environment.NewLine +
                GenerateFieldText(10, 2) + Environment.NewLine +
                GenerateFieldText(12, 3);
        }

        /// <summary>
        /// Статический метод для сохранения рекордов
        /// </summary>
        /// <param name="rec">Коллекция рекордов</param>
        public static void SaveRecords(List<List<List<string>>> rec)
        {
            DataContractJsonSerializer ser = 
                new DataContractJsonSerializer(typeof(List<List<List<string>>>));

            try
            {
                using (FileStream fs = new FileStream(ConstValues.RecordsPath, FileMode.Create))
                {
                    ser.WriteObject(fs, rec);
                }

                MessageBox.Show("Поздравляем, новое время в рекордах!");
            }
            catch (Exception)
            {
                MessageBox.Show("Неудалось сохранить новый рекорд.");
            }
        }

        /// <summary>
        /// Метод, дополняющий GenerateText. Генерирует повторяющийся промежуток текста
        /// </summary>
        /// <param name="size">Размер поля</param>
        /// <param name="dif">Сложность поля</param>
        /// <returns>Часть сгенерированного текста</returns>
        private string GenerateFieldText(int size, int dif)
            => $"   {size}x{size}:" + Environment.NewLine +
                "      Новичок:" + Environment.NewLine +
                $"\t1. {record[dif][0][0]}" + Environment.NewLine +
                $"\t2. {record[dif][0][1]}" + Environment.NewLine +
                $"\t3. {record[dif][0][2]}" + Environment.NewLine +
                "      Совершенствующийся:" + Environment.NewLine +
                $"\t1. {record[dif][1][0]}" + Environment.NewLine +
                $"\t2. {record[dif][1][1]}" + Environment.NewLine +
                $"\t3. {record[dif][1][2]}" + Environment.NewLine +
                "      Опытный игрок:" + Environment.NewLine +
                $"\t1. {record[dif][2][0]}" + Environment.NewLine +
                $"\t2. {record[dif][2][1]}" + Environment.NewLine +
                $"\t3. {record[dif][2][2]}";
    }
}
