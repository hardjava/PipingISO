using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PipingISO
{
    public class Load
    {
        private List<string> list = new List<string>();
        public bool read(DataGridView dataGridView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV File(*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader streamReader = new StreamReader(fileStream);
                    streamReader.ReadLine();
                    while (!streamReader.EndOfStream)
                    {
                        list.Add(streamReader.ReadLine());
                    }
                    streamReader.Close();
                    fileStream.Close();
                    initGridView(dataGridView);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }

        private void initGridView(DataGridView dataGridView)
        {
            for(int i = 0; i < list.Count; i++)
            {
                String[] data = list[i].Split(',');
                dataGridView.Rows.Add(data[0], data[1], data[2], data[3], data[4], data[5]);
            }
            dataGridView.AllowUserToAddRows = false;
        }
    }
}
