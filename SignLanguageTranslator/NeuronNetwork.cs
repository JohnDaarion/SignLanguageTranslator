using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignLanguageTranslator
{
    class NeuronNetwork : CommonMethods
    {
        //kod do testowania rozwiązań
        public void RunItMethod()
        {
            byte[][] input; 
            double[][] output = new double[88][]; 
            ActivationNetwork network = new ActivationNetwork(
                 new BipolarSigmoidFunction(2),
                 500,
                 12,
                 12,
                 1); 
            network.SetActivationFunction(new BipolarSigmoidFunction());
                 
            BackPropagationLearning teacher = new BackPropagationLearning(network);

            input = gettingDataFromXml<List<byte[]>>("D:\\dokumenty\\Visual Studio 2015\\Projects\\SignLanguageTranslator\\SignLanguageTranslator\\bin\\x64\\Debug\\baseDataDouble\\0.xml").ToArray();
            double[][] input2 = new double[88][]; 

            for (int i = 0; i < 88; i++)
            {
                input2[i] = new double[500];
                for (int j = 0; j < 500; j++)
                {
                    if (input[i][j] == 1)
                    {
                        input2[i][j] = 1;
                    }
                    else
                        input2[i][j] = 0;
                }
            }
            
            for (int i = 0; i < 44; i++) 
            {
                output[i] = new double[1];
                output[i][0] = 0.5;
            }
            for (int i = 44; i < 88; i++)
            {
                output[i] = new double[1];
                output[i][0] = 0;
            }

            teacher.Momentum = 0.2;
            teacher.LearningRate = 0.2;
            double error = 0.5;
            long numberOfEpochs = 0;
            while (error > 0.2)
            {
                error = teacher.RunEpoch(input2, output);
                numberOfEpochs++;
            }
        }
    }
}