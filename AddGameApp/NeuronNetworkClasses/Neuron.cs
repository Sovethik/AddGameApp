using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddGameApp.NeuronNetworkPage
{
    public class Neuron
    {

        public double[] Weights { get; set; }
        public double Bias { get; set; }
        public double LearningRate { get; set; }
        public double Output { get; private set; }

        public Neuron(int numInputs, double learningRate)
        {
            Random rnd = new Random();
            Weights = new double[numInputs];
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = rnd.NextDouble() * 2 - 1;  // случайные веса от -1 до 1
            }
            Bias = rnd.NextDouble() * 2 - 1;  // случайное смещение от -1 до 1
            LearningRate = learningRate;
        }

        public void CalculateOutput(double[] inputs)
        {
            if (inputs.Length != Weights.Length)
            {
                throw new ArgumentException("Количество входов не соответствует количеству весов");
            }

            double output = Bias;  // начинаем с добавления смещения
            for (int i = 0; i < inputs.Length; i++)
            {
                output += inputs[i] * Weights[i];  // добавляем взвешенное значение каждого входа
            }
            Output = Sigmoid(output);  // применяем функцию активации и запоминаем результат
        }

        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));  // сигмоидная функция активации
        }

        public void Train(double[] inputs, double target)
        {
            CalculateOutput(inputs);  // рассчитываем выходное значение
            double error = target - Output;  // находим ошибку
            double delta = error * Output * (1 - Output);  // находим дельта-правило
            Bias += LearningRate * delta;  // корректируем смещение

            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] += LearningRate * delta * inputs[i];  // корректируем веса
            }
        }

    }
}
