using System;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2019.day08
{
    public class problem01
    {
        public static void Run()
        {
            
            Console.WriteLine(calculateOutput(processInput(25,6)));   

            int [,] image = renderImage(processInput(25,6));
        }

        public static List<ImageLayer> processInput(int width, int height)
        {            
            string lines = System.IO.File.ReadAllText("day08_input.txt");
            List<int[]> layerLines = new List<int[]>();
            List<ImageLayer> image = new List<ImageLayer>();

            for(int x = 0; x < lines.Length; x += width){
                string line = lines.Substring(x, width);
                
                layerLines.Add(line.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray());
            }

            for(int y = 0; y < layerLines.Count; y += height)
            {
                ImageLayer layer= new ImageLayer();

                layer.Layers.AddRange(layerLines.GetRange(y, height));
                image.Add(layer);
            }
            
            return image;
        }

        public static int calculateOutput(List<ImageLayer> image){
            int lowestZeroCount = int.MaxValue;
            int oneCount = -1;
            int twoCount = -1;

            foreach(ImageLayer layer in image){
                int zeros = layer.ZeroCount();
                Console.WriteLine(string.Format("Zero count: {0}", zeros));
                if(zeros < lowestZeroCount){
                    oneCount = layer.OneCount();
                    twoCount = layer.TwoCount();
                    lowestZeroCount = zeros;
                }
            }

            Console.WriteLine(string.Format("Smallest number of zeros: {0}", lowestZeroCount));
            return oneCount * twoCount;
        }

        public static int[,] renderImage(List<ImageLayer> image){
            int[,] finalImage = new int[25,6];

            for(int x = 0; x < 6; x++){
                ImageLayer line = image[x];
                for(int y = 0; y < 25; y++){
                    if(line.Layers[x][y] != 2){
                        finalImage[x,y] = line.Layers[x][y];
                        Console.WriteLine(finalImage[x,y]);
                        break;
                    }
                }
                Console.WriteLine();
            }

            return finalImage;
        }
    }

    public class ImageLayer{
        public List<int[]> Layers {get; set;}

        public ImageLayer() {
            Layers = new List<int[]>();
        }
        public int ZeroCount() {
            int count = 0;
            foreach (int[] layer in Layers)
            {
                count += layer.Where(i => i == 0).ToArray().Count();
                //Console.WriteLine(string.Join(",", layer));
            }
            return count;
        }

        public int OneCount(){
            int count = 0;
            foreach (int[] layer in Layers)
            {
                count += layer.Where(i => i == 1).ToArray().Count();
            }
            return count;
        }

        public int TwoCount(){
            int count = 0;
            foreach (int[] layer in Layers)
            {
                count += layer.Where(i => i == 2).ToArray().Count();
            }
            return count;
        }
    }
}