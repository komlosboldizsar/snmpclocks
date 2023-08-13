using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNMPclocks.Model
{
    public class ClockListSerializer
    {

        private string _filePath;
        private IList<Clock> _list;

        public ClockListSerializer(string filePath, IList<Clock> list)
        {
            _filePath = filePath;
            _list = list;
        }

        public bool Load()
        {
            _list.Clear();
            try
            {
                string json = File.ReadAllText(_filePath);
                Clock[] clocks = JsonSerializer.Deserialize<Clock[]>(json);
                foreach (Clock clock in clocks)
                    _list.Add(clock);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(_list, options);
            File.WriteAllText(_filePath, json);
        }

    }

}
