using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Excelsior
{
    public partial class excelsiorForm : Form
    {
        public excelsiorForm()
        {
            InitializeComponent();
        }

        private void sourceFile1BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"; // Set the desired file filter
                openFileDialog.Title = "Select Source File 1"; // Set the dialog title

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceFile1TextBox.Text = openFileDialog.FileName; // Set the selected file path to the TextBox
                }
            }
        }

        private void sourceFile2BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"; // Set the desired file filter
                openFileDialog.Title = "Select Source File 2"; // Set the dialog title

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceFile2TextBox.Text = openFileDialog.FileName; // Set the selected file path to the TextBox
                }
            }
        }

        private void workFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"; // Set the desired file filter
                openFileDialog.Title = "Select Work File"; // Set the dialog title

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workFileTextBox.Text = openFileDialog.FileName; // Set the selected file path to the TextBox
                }
            }
        }

        private void compareAndExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the paths of the source files and work file
                string sourceFile1Path = sourceFile1TextBox.Text;
                string sourceFile2Path = sourceFile2TextBox.Text;
                string workFilePath = workFileTextBox.Text;

                // Load the source files and work file into DataTables
                DataTable sourceFile1Data = LoadExcelFile(sourceFile1Path);
                DataTable sourceFile2Data = LoadExcelFile(sourceFile2Path);
                DataTable workFileData = LoadExcelFile(workFilePath);

                // Add additional columns to the source file DataTables
                sourceFile1Data.Columns.Add("Assigned To", typeof(string));
                sourceFile1Data.Columns.Add("College Name", typeof(string));
                sourceFile2Data.Columns.Add("Assigned To", typeof(string));
                sourceFile2Data.Columns.Add("College Name", typeof(string));

                // Add additional columns to the work file at the appropriate positions
                workFileData.Columns.Add("Assigned To", typeof(string)).SetOrdinal(workFileData.Columns.IndexOf("Student ID"));
                workFileData.Columns.Add("College Name", typeof(string)).SetOrdinal(workFileData.Columns.IndexOf("Assigned To") + 1);
                workFileData.Columns.Add("Break In Terms", typeof(string)).SetOrdinal(workFileData.Columns.IndexOf("INFO-PENDING") + 1);
                workFileData.Columns.Add("Credit Per Semester 15+", typeof(string)).SetOrdinal(workFileData.Columns.IndexOf("TOTAL CREDITS-EARNED") + 1);
                workFileData.Columns.Add("Found In", typeof(string));

                // Perform the comparison and update the new columns
                foreach (DataRow row in workFileData.Rows)
                {
                    // Retrieve the Student ID for comparison
                    string key = row["Student ID"].ToString();

                    // Perform the comparison logic and update the additional columns
                    if (ContainsKey(sourceFile1Data, "Student ID", key))
                    {
                        row["Assigned To"] = sourceFile1Data.Rows[0]["Assigned To"];
                        row["College Name"] = sourceFile1Data.Rows[0]["College Name"];
                        row["Found In"] = "1. Master File";
                    }
                    else if (ContainsKey(sourceFile2Data, "Student ID", key))
                    {
                        row["Assigned To"] = sourceFile2Data.Rows[0]["Assigned To"];
                        row["College Name"] = sourceFile2Data.Rows[0]["College Name"];
                        row["Found In"] = "2. Pending File";
                    }
                    else
                    {
                        row["Found In"] = "0. New Verification";
                    }

                    row["Break In Terms"] = string.Empty;
                    row["Credit Per Semester 15+"] = string.Empty;
                }

                // Export the modified work file
                ExportToExcel(workFileData, workFilePath, "_compared.xlsx");

                MessageBox.Show("Comparison and export completed successfully.", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportFilteredOnlyButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the paths of the source files and work file
                string sourceFile1Path = sourceFile1TextBox.Text;
                string sourceFile2Path = sourceFile2TextBox.Text;
                string workFilePath = workFileTextBox.Text;

                // Load the source files and work file into DataTables
                DataTable sourceFile1Data = LoadExcelFile(sourceFile1Path);
                DataTable sourceFile2Data = LoadExcelFile(sourceFile2Path);
                DataTable workFileData = LoadExcelFile(workFilePath);

                // Filter the rows not found in both source files
                DataTable filteredData = workFileData.Clone();
                foreach (DataRow row in workFileData.Rows)
                {
                    string key = row["Student ID"].ToString();

                    if (!ContainsKey(sourceFile1Data, "Student ID", key) && !ContainsKey(sourceFile2Data, "Student ID", key))
                    {
                        filteredData.ImportRow(row);
                    }
                }

                // Add additional columns to the filtered data at the appropriate positions
                filteredData.Columns.Add("Assigned To", typeof(string)).SetOrdinal(filteredData.Columns.IndexOf("Student ID"));
                filteredData.Columns.Add("College Name", typeof(string)).SetOrdinal(filteredData.Columns.IndexOf("Assigned To") + 1);
                filteredData.Columns.Add("Break In Terms", typeof(string)).SetOrdinal(filteredData.Columns.IndexOf("INFO-PENDING") + 1);
                filteredData.Columns.Add("Credit Per Semester 15+", typeof(string)).SetOrdinal(filteredData.Columns.IndexOf("TOTAL CREDITS-EARNED") + 1);
                //filteredData.Columns.Add("Found In", typeof(string));

                // Perform the additional column updates (set them to empty values)
                foreach (DataRow row in filteredData.Rows)
                {
                    row["Assigned To"] = string.Empty;
                    row["College Name"] = string.Empty;
                    row["Break In Terms"] = string.Empty;
                    row["Credit Per Semester 15+"] = string.Empty;
                    //row["Found In"] = string.Empty;
                }

                // Export the filtered data to a new Excel file
                ExportToExcel(filteredData, workFilePath, "_filtered.xlsx");

                MessageBox.Show("Filtered export completed successfully.", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable LoadExcelFile(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);

                // Read the header row
                var headerRow = worksheet.FirstRow();
                foreach (var cell in headerRow.CellsUsed())
                {
                    dataTable.Columns.Add(cell.Value.ToString());
                }

                // Read the data rows
                var dataRows = worksheet.RowsUsed().Skip(1);
                foreach (var dataRow in dataRows)
                {
                    var newRow = dataTable.NewRow();
                    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                    {
                        newRow[columnIndex] = dataRow.Cell(columnIndex + 1).Value.ToString();
                    }
                    dataTable.Rows.Add(newRow);
                }
            }

            return dataTable;
        }

        private bool ContainsKey(DataTable dataTable, string keyColumn, string key)
        {
            return dataTable.AsEnumerable().Any(row => row.Field<string>(keyColumn) == key);
        }

        private void ExportToExcel(DataTable dataTable, string workFilePath, string suffix)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Exported File";
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(workFilePath) + suffix;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Sheet1");

                        // Write the header row
                        var headerRow = worksheet.Row(1);
                        for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                        {
                            headerRow.Cell(columnIndex + 1).Value = dataTable.Columns[columnIndex].ColumnName;
                        }

                        // Write the data rows
                        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                        {
                            var dataRow = dataTable.Rows[rowIndex];
                            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                            {
                                worksheet.Cell(rowIndex + 2, columnIndex + 1).Value = dataRow[columnIndex].ToString();
                            }
                        }

                        workbook.SaveAs(filePath);
                    }
                }
            }
        }
    }
}
