using System;
using System.Collections.Concurrent;
using System.IO;

namespace GetLicence
{
    public class Mutation
    {
        private ConcurrentDictionary<string, bool> _winners = new ConcurrentDictionary<string, bool>();

        public bool DidIWin(string nick, string emailAddress)
        {
            if (_winners.Count < 2)
            {
                var winner = $"{nick}:{emailAddress}";
                if (_winners.ContainsKey(winner))
                    return false;
                _winners.TryAdd(winner, true);
                Console.WriteLine($"Winner: {nick}");
                File.AppendAllText("winners.txt", winner);
                return true;
            }

            return false;
        }
    }
}