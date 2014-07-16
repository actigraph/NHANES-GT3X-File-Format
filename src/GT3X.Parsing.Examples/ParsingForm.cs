using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CsvHelper;
using GT3X.Parsing.Library;
using Newtonsoft.Json;

namespace GT3X.Parsing.Examples
{
    public partial class ParsingForm : Form
    {
        private string _fileName;

        public ParsingForm()
        {
            InitializeComponent();
            buttonOpenFile.Click += (sender, args) => OpenFile();
            buttonExport.Click += (sender, args) => ExportFile();
        }

        private void ExportFile()
        {
            //make sure file is still good
            Gt3XFile g;

            try
            {
                g = new Gt3XFile(_fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to open gt3x File:\r\n\r\n" + ex.Message, "Invalid gt3x File",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string exportFileName;

            using (var savefile = new SaveFileDialog())
            {
                savefile.Filter = radioButtonCSV.Checked ? "CSV File (*.csv)|*.csv" : "JSON File (*.json)|*.json";
                savefile.Title = "Save Export";
                if (savefile.ShowDialog() != DialogResult.OK)
                    return;

                exportFileName = savefile.FileName;
            }

            using (var writer = new StreamWriter(exportFileName))
            {
                if (radioButtonActivity.Checked)
                {
                    if (radioButtonJSON.Checked)
                        writer.Write(JsonConvert.SerializeObject(g.ActivityEnumerator(), Formatting.Indented));
                    else
                    {
                        var csv = new CsvWriter(writer);
                        csv.Configuration.RegisterClassMap<AcclererationClassMap>();
                        csv.WriteRecords(g.ActivityEnumerator());
                    }
                }
                else if (radioButtonLux.Checked)
                {
                    if (radioButtonJSON.Checked)
                        writer.Write(JsonConvert.SerializeObject(g.LuxEnumerator(), Formatting.Indented));
                    else
                    {
                        var csv = new CsvWriter(writer);
                        csv.Configuration.RegisterClassMap<LuxClassMap>();
                        csv.WriteRecords(g.LuxEnumerator());
                    }
                }
            }

            Process.Start(exportFileName);
        }

        private void OpenFile()
        {
            using (FileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Files (*.gt3x)|*.gt3x";
                fileDialog.Title = "Select GT3X File(s)";
                fileDialog.FileName = "";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;

                    //make sure it's a valid GT3X file
                    Gt3XFile g;

                    try
                    {
                        g = new Gt3XFile(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Unable to open gt3x File:\r\n\r\n" + ex.Message, "Invalid gt3x File",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _fileName = fileName;
                    textBoxFilename.Text = _fileName;
                    panelOptions.Enabled = true;
                }
            }
        }
    }
}
