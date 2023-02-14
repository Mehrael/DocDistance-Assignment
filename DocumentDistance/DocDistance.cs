using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            //throw new NotImplementedException();

            string text = File.ReadAllText(@doc1FilePath).ToLower();
            //text = text.ToLower();
            //Console.WriteLine("\n" + text + "\n");
            //Console.WriteLine();
            
            string text2 = File.ReadAllText(@doc2FilePath).ToLower();
            //text2 = text2.ToLower();
            //Console.WriteLine("\n" + text2 + "\n");
            //Console.WriteLine();

            Dictionary<string, double> first_file_words = new Dictionary<string, double>();
            Dictionary<string, double> second_file_words = new Dictionary<string, double>();

            HashSet<string> common = new HashSet<string>();

            Dictionary<string,double> freq_one = new Dictionary<string,double>(); 
            Dictionary<string,double> freq_two = new Dictionary<string,double>();   

            double lD1l2 = 0, lD2l2 = 0;

            string word = "";

            for (int i = 0; i < text.Length; i++)
            {
                if(Char.IsLetterOrDigit(text[i])) 
                        word += text[i];
                else if (word == "")
                    continue;
                else
                {

                    if(first_file_words.ContainsKey(word))
                        first_file_words[word]++;
                    else
                        first_file_words.Add(word, 1);


                    if (freq_one.ContainsKey(word))
                        //freq_one[word] = Math.Pow(first_file_words[word], 2);
                        freq_one[word] = first_file_words[word] * first_file_words[word];
                    else
                        freq_one.Add(word, 1);

                    word = "";
                }
            }

            if(word != "")
            {

                if (first_file_words.ContainsKey(word))
                    first_file_words[word]++;
                else
                    first_file_words.Add(word, 1);


                if (freq_one.ContainsKey(word))
                    //freq_one[word] = Math.Pow(first_file_words[word], 2);
                    freq_one[word] = first_file_words[word] * first_file_words[word];
                else
                    freq_one.Add(word, 1);

            }

            lD1l2 = freq_one.Sum(X => X.Value);


            word = "";
            for (int i = 0; i < text2.Length; i++)
            {
                if (Char.IsLetterOrDigit(text2[i])) 
                        word += text2[i];
                else if (word == "")
                    continue;
                else
                {

                    if (second_file_words.ContainsKey(word))
                        second_file_words[word]++;
                    else
                        second_file_words.Add(word, 1);

                    if(first_file_words.ContainsKey(word))   
                        common.Add(word);

                    if (freq_two.ContainsKey(word))
                        //freq_two[word] = Math.Pow(second_file_words[word], 2);
                    freq_two[word] = second_file_words[word] * second_file_words[word];
                    else
                        freq_two.Add(word, 1);

                    word = "";
                }
            }
            if(word != "")
            {

                if (second_file_words.ContainsKey(word))
                    second_file_words[word]++;
                else
                    second_file_words.Add(word, 1);

                if (first_file_words.ContainsKey(word))
                    common.Add(word);

                if (freq_two.ContainsKey(word))
                    //freq_two[word] = Math.Pow(second_file_words[word], 2);
                    freq_two[word] = second_file_words[word] * second_file_words[word];
                else
                    freq_two.Add(word, 1);

            }

            lD2l2 = freq_two.Sum(X => X.Value);

            double D1_D2 = 0;
            foreach (var value in common)
                D1_D2 += (first_file_words[value] * second_file_words[value]);


            //Console.WriteLine("                                               num = " + num + " common = " + common.Count + " file1 = " + first_file_words.Count + " file2 = " + second_file_words.Count);
            //double resultInRadian =;

            double result = (180 / Math.PI) * Math.Acos(D1_D2 / Math.Sqrt(lD1l2 * lD2l2));
                         
            return result;
        }
    }
}
