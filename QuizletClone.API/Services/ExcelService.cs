using QuizletClone.API.Payload;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;

namespace QuizletClone.API.Services
{
    public class ExcelService
    {
        public async Task<List<TermPayload>> ReadTermPayloadFromFile(string filePath)
        {
            List<TermPayload> termPayloads = new List<TermPayload>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            if (rowIndex != 0) // Skip the first row
                            {
                                string question = reader.GetString(0);
                                string answer = reader.GetString(1);

                                termPayloads.Add(new TermPayload()
                                {
                                    Question = question,
                                    Answer = answer
                                });
                            }

                            rowIndex++;
                        }

                    } while(reader.NextResult());
                }
            }

            return termPayloads;
        }
    }
}
