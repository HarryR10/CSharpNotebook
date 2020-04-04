namespace Autocomplete.Basic
{
    using System.IO;
    using System.Linq;
    using System.Threading;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");


        private SimilarLine _oneSimpleWord = new SimilarLine("", 0);
        private SimilarLine _oneMovieTitle = new SimilarLine("", 0);
        private SimilarLine _oneStageName = new SimilarLine("", 0);
        
        private Thread _searchMovieTitles;
        private Thread _searchStageNames;
        private Thread _anotherThread;
        
        public string FindBestSimilar()
        {
            if (_oneSimpleWord.SimilarityScore > _oneMovieTitle.SimilarityScore &&
                _oneSimpleWord.SimilarityScore > _oneStageName.SimilarityScore)
            {
                return _oneSimpleWord.Line;
            }
            return (_oneStageName.IsBetterThan(_oneMovieTitle) ? _oneStageName : _oneMovieTitle).Line;
        }

        public void HandleTyping(HintedControl control)
        {
            KillThread(_anotherThread);
            
            _anotherThread = new Thread(() =>
                control.Hint = ThreadControl(control));

            _anotherThread.Name = "MainSearchThread"; //для отладки
            _anotherThread.Start();
        }

        private static void KillThread(Thread current)
        {
            if (current != null && current.IsAlive)
            {
                current.Abort();
            }
        }

        private static SimilarLine BestSimilarInArray(string[] lines, string example)
        {
            return lines.Aggregate(
                new SimilarLine(string.Empty, 0),
                (SimilarLine best, string line) =>
                {
                    var current = new SimilarLine(line, line.Similarity(example));

                    return current.IsBetterThan(best) ? current : best;
                });
        }

        private string ThreadControl(HintedControl control)
        {
            KillThread(_searchMovieTitles);
            KillThread(_searchStageNames);

            _searchMovieTitles = new Thread(() =>
                _oneMovieTitle = BestSimilarInArray(MovieTitles, control.LastWord)) {Name = "MovieTitlesSearchThread"};
            _searchMovieTitles.Start();
            
            _searchStageNames = new Thread(() => 
                _oneStageName = BestSimilarInArray(StageNames, control.LastWord)) {Name = "StageNamesSearchThread"};
            _searchStageNames.Start();
            
            _oneSimpleWord = BestSimilarInArray(SimpleWords, control.LastWord);
            
            _searchMovieTitles.Join();
            _searchStageNames.Join();
            
            return FindBestSimilar();
        }
    }
}
