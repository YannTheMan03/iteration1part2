using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace iteration1
{
    internal class Leaderboard
    {
        private Dictionary<string, int> _scores = new Dictionary<string, int>();

        public void AddOrUpdateScore(string playersName, int Score)
        {
            if (_scores.ContainsKey(playersName))
            {
                _scores[playersName] = Score;
            }
            else
            {
                _scores.Add(playersName, Score);
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetTopScores(int topN = 5)
        {
            return _scores
                .OrderByDescending(pair => pair.Value)
                .Take(topN);
        }

        public void Save(string filePath)
        {
            string json = JsonSerializer.Serialize(_scores);
            File.WriteAllText(filePath, json);
          
        }
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _scores = new Dictionary<string, int>();
                return;
            }
            string json = File.ReadAllText(filePath);
            _scores = JsonSerializer.Deserialize<Dictionary<string, int>>(json)
                ?? new Dictionary<string, int>();
        }
        
    }
}
