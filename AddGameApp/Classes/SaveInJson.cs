using AddGameApp.NeuronNetworkPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddGameApp.Classes
{
    internal class SaveInJson
    {
        private readonly string PATH;

        public SaveInJson(string path)
        {
            PATH = path;
        }



        public NeuralNetwork LoadDataNeuralNetwork(int inputSize, int hiddenSize, int hiddenSise_2, int outputSize, double learningRate)
        {
            var fileExists = File.Exists(PATH);
            if(!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new NeuralNetwork(inputSize, hiddenSize, hiddenSise_2, outputSize, learningRate);
            }
            using(var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<NeuralNetwork>(fileText);
            }
        }

        public void SaveDataNeuralNetwork(NeuralNetwork neuralNetwork)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(neuralNetwork);
                writer.Write(output);
            }
        }


    }
}
