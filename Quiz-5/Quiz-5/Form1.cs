using System.Text;

namespace Quiz_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] readAlline = File.ReadAllLines(openFileDialog.FileName);
                string readAlltext = File.ReadAllText(openFileDialog.FileName);
                this.dataGridView1.Text = readAlline[0];
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[2].Value = textBox3.Text;

                for (int i = 0; i < readAlltext.Length; i++)
                {
                    string listRAW = readAlline[i];
                    string[] listSplted = listRAW.Split(',');
                    IncomeAndExpenese list = new IncomeAndExpenese(listSplted[0]);
                    _ = new IncomeAndExpenese(listSplted[1]);
                    _ = new IncomeAndExpenese(listSplted[2]);


                }
            }    
        }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = String.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv (*.csv) | *.csv"; 
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != String.Empty)
                {
                    filepath = saveFileDialog.FileName;

                    //save file
                    File.WriteAllText(filepath, this.textBox1.Text, Encoding.UTF8);
                }
            }
        }
        int sumin = 0, sumex = 0, inIn = 0, inEx = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV(*.csv)|*.csv";
                bool fileError = false;
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    if(!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = " ";
                            string[] outputcsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputcsv[0] += columnNames;
                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputcsv[i]+= dataGridView1.Rows[i-1].Cells[j].Value.ToString();  
                                }
                            }
                            File.WriteAllLines(sfd.FileName, outputcsv, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        { 
                            MessageBox.Show("Error;" + ex.Message);
                        }
                    }   
                }
            }
        }
    }   
}