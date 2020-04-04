namespace Autocomplete.Async
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private string _indicator = "";
        private CancellationTokenSource _token;

        private SimilarLine[] SubResult = new SimilarLine[3];

        private void ResetSubResult()
        {
            SubResult[0] = new SimilarLine(String.Empty, 0);
            SubResult[1] = new SimilarLine(String.Empty, 0);
            SubResult[2] = new SimilarLine(String.Empty, 0);
        }

        private async Task<string> FindBestSimilarAsync(string example)
        {
            _token?.Cancel();
            _token = new CancellationTokenSource();

            ResetSubResult();

            var findStgNames = BestSimilarInArrayAsync(StageNames, example, _token);
            var findMvTitles = BestSimilarInArrayAsync(MovieTitles, example, _token);
            var findSmpWords = BestSimilarInArrayAsync(SimpleWords, example, _token);

            var allTasks = new List<Task> { findStgNames, findMvTitles, findSmpWords };
            while (allTasks.Any())
            {
                if(_indicator != example)
                {
                    _token.Cancel();
                }

                Task finished = await Task.WhenAny(allTasks);

                if (finished == findStgNames)
                {
                    SubResult[0] = findStgNames.Result;
                }
                else if (finished == findMvTitles)
                {
                    SubResult[1] = findMvTitles.Result;
                }
                else if (finished == findSmpWords)
                {
                    SubResult[2] = findSmpWords.Result;
                }
                allTasks.Remove(finished);
            }

            if (SubResult[2].SimilarityScore > SubResult[1].SimilarityScore && SubResult[2].SimilarityScore > SubResult[0].SimilarityScore)
            {
                return SubResult[2].Line;
            }
            
            if (SubResult[1].IsBetterThan(SubResult[0]))
            {
                return SubResult[1].Line;
            }
            
            return SubResult[0].Line;

            #region SecondAlgorithm
            // return SubResult.Aggregate((best, current) =>
            // {
            //     if ((current.SimilarityScore > best.SimilarityScore)
            //         || (current.SimilarityScore == best.SimilarityScore
            //             && current.Line.Length < best.Line.Length))
            //     {
            //         return current;
            //     }
            //
            //     return best;
            // }).Line;
            #endregion
        }
        
        #region macOsVariant
        // public void HandleTyping(HintedControl control)
        // {
        //     _indicator = control.LastWord;
        //     control.Hint =  FindBestSimilarAsync(control.LastWord).Result;
        // }
        #endregion
        
        #region WindowsVariant
        public async void HandleTyping(HintedControl control)
        {
            _indicator = control.LastWord;
            control.Hint = await FindBestSimilarAsync(control.LastWord);
        }
        #endregion


        private async Task<SimilarLine> BestSimilarInArrayAsync(string[] lines, string example, CancellationTokenSource token)
        {         
            var best = new SimilarLine(string.Empty, 0);

            if (token.IsCancellationRequested)
            {
                return best;
            }

            var task = Task.Factory.StartNew<SimilarLine>((o) =>
            {
                var ct = (CancellationTokenSource)o;
                
                foreach (var line in lines)
                {
                    var currentLine = new SimilarLine(line, line.Similarity(example));

                    if (currentLine.IsBetterThan(best))
                    {
                        best = currentLine;
                    }
                }
                
                return best;
            }, token);

            return await task;
        }
    }
}
