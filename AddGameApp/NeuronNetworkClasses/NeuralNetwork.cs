using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddGameApp.NeuronNetworkPage
{
    public class NeuralNetwork
    {

        public bool IsLearn;

        public List<Neuron> InputLayer { get; private set; }
        public List<Neuron> HiddenLayer { get; private set; }
        public List<Neuron> HiddenLayer_2 {  get; private set; }
        public List<Neuron> OutputLayer { get; private set; }

        public NeuralNetwork(int inputSize, int hiddenSize, int hiddenSise_2 , int outputSize, double learningRate)
        {
            InputLayer = new List<Neuron>();
            HiddenLayer = new List<Neuron>();
            HiddenLayer_2 = new List<Neuron>();
            OutputLayer = new List<Neuron>();

            for (int i = 0; i < inputSize; i++)
            {
                InputLayer.Add(new Neuron(1, learningRate));  // 1 - количество входов у каждого нейрона
            }

            for (int i = 0; i < hiddenSize; i++)
            {
                HiddenLayer.Add(new Neuron(inputSize, learningRate));  // каждый нейрон получает входы от всех нейронов предыдущего слоя
            }

            for(int i =0; i < hiddenSise_2; i++)
            {
                HiddenLayer_2.Add(new Neuron(hiddenSize, learningRate));
            }

            for (int i = 0; i < outputSize; i++)
            {
                OutputLayer.Add(new Neuron(hiddenSise_2, learningRate));  // каждый нейрон получает входы от всех нейронов скрытого слоя
            }
        }

        public double[] FeedForward(double[] inputs)
        {

            double[] inputForInputsLayer = new double[1];


            double[] inputLayerOutputs = new double[InputLayer.Count];
            for (int i = 0; i < InputLayer.Count; i++)
            {
                inputForInputsLayer[0] = inputs[i];
                InputLayer[i].CalculateOutput(inputForInputsLayer);
                inputLayerOutputs[i] = InputLayer[i].Output;
            }


            double[] hiddenLayerOutputs = new double[HiddenLayer.Count];

            for (int i = 0; i < HiddenLayer.Count; i++)
            {
                HiddenLayer[i].CalculateOutput(inputLayerOutputs);  // рассчитываем выходные значения скрытого слоя
                hiddenLayerOutputs[i] = HiddenLayer[i].Output;
            }

            double[] hiddenLayerOutputs_2 = new double[HiddenLayer_2.Count];

            for(int i =0; i < HiddenLayer_2.Count; i++)
            {
                HiddenLayer_2[i].CalculateOutput(hiddenLayerOutputs);
                hiddenLayerOutputs_2[i] = HiddenLayer_2[i].Output;
            }

            double[] outputLayerOutputs = new double[OutputLayer.Count];
            for (int i = 0; i < OutputLayer.Count; i++)
            {
                OutputLayer[i].CalculateOutput(hiddenLayerOutputs_2);  // рассчитываем выходные значения выходного слоя
                outputLayerOutputs[i] = OutputLayer[i].Output;
            }

            return outputLayerOutputs;  // возвращаем результат
        }

        public void Train(double[] inputs, double targets)
        {
            double[] inputForInputsLayer = new double[1];


            double[] inputLayerOutputs = new double[InputLayer.Count];
            for (int i = 0; i < InputLayer.Count; i++)
            {
                inputForInputsLayer[0] = inputs[i];
                InputLayer[i].Train(inputForInputsLayer, targets);
                inputLayerOutputs[i] = InputLayer[i].Output;
            }


            double[] hiddenLayerOutputs = new double[HiddenLayer.Count];
            for (int i = 0; i < HiddenLayer.Count; i++)
            {
                HiddenLayer[i].Train(inputLayerOutputs, targets);
                /*HiddenLayer[i].CalculateOutput(inputs);*/  // рассчитываем выходные значения скрытого слоя
                hiddenLayerOutputs[i] = HiddenLayer[i].Output;
            }

            double[] hiddenLayerOutputs_2 = new double[HiddenLayer_2.Count];
            for(int i = 0; i < HiddenLayer_2.Count; i++)
            {
                HiddenLayer_2[i].Train(hiddenLayerOutputs, targets);
                hiddenLayerOutputs_2[i] = HiddenLayer_2[i].Output;
            }

            double[] outputLayerOutputs = new double[OutputLayer.Count];
            for (int i = 0; i < OutputLayer.Count; i++)
            {
                OutputLayer[i].Train(hiddenLayerOutputs_2, targets);  // обучаем каждый нейрон выходного слоя
                outputLayerOutputs[i] = OutputLayer[i].Output;
            }
        }

    }
}
