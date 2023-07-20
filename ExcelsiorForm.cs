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

        // Button click event handler for selecting Source File 1
        private void sourceFile1BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Source File 1";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceFile1TextBox.Text = openFileDialog.FileName;
                }
            }
        }

        // Button click event handler for selecting Source File 2
        private void sourceFile2BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Source File 2";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceFile2TextBox.Text = openFileDialog.FileName;
                }
            }
        }

        // Button click event handler for selecting Source File 3
        private void sourceFile3BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"; // Set the desired file filter
                openFileDialog.Title = "Select Source File 3"; // Set the dialog title

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceFile3TextBox.Text = openFileDialog.FileName; // Set the selected file path to the TextBox
                }
            }
        }


        // Button click event handler for selecting Work File
        private void workFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Work File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workFileTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        // Button click event handler for performing comparison and export
        // Button click event handler for performing comparison and export
        private void compareAndExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the paths of the source files and work file
                string sourceFile1Path = sourceFile1TextBox.Text;
                string sourceFile2Path = sourceFile2TextBox.Text;
                string sourceFile3Path = sourceFile3TextBox.Text; // New Source File 3
                string workFilePath = workFileTextBox.Text;

                // Load the source files and work file into DataTables
                DataTable sourceFile1Data = LoadExcelFile(sourceFile1Path);
                DataTable sourceFile2Data = LoadExcelFile(sourceFile2Path);
                DataTable sourceFile3Data = LoadExcelFile(sourceFile3Path); // New Source File 3
                DataTable workFileData = LoadExcelFile(workFilePath);

                // Add the "Found In" column to the work file if it doesn't exist
                AddColumnIfNotExists(workFileData, "Found In", typeof(string));

                // Perform the comparison and update the "Found In" column
                foreach (DataRow row in workFileData.Rows)
                {
                    // Retrieve the Student ID for comparison
                    string key = row["Student ID"].ToString();

                    // Perform the comparison logic and update the "Found In" column
                    if (ContainsKey(sourceFile1Data, "Student ID", key))
                    {
                        row["Found In"] = "1. Master File";
                    }
                    else if (ContainsKey(sourceFile2Data, "Student ID", key))
                    {
                        row["Found In"] = "2. Pending File";
                    }
                    else if (ContainsKey(sourceFile3Data, "Student ID", key)) // Check Source File 3
                    {
                        row["Found In"] = "3. Freshman"; // Update "Found In" column with "3. Freshman"
                    }
                    else
                    {
                        row["Found In"] = "0. New Verification";
                    }
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


        // Helper method to add a column to a DataTable if it doesn't exist
        private void AddColumnIfNotExists(DataTable dataTable, string columnName, Type columnType, string insertAfterColumn = null)
        {
            if (!dataTable.Columns.Contains(columnName))
            {
                if (!string.IsNullOrEmpty(insertAfterColumn) && dataTable.Columns.Contains(insertAfterColumn))
                {
                    dataTable.Columns.Add(columnName, columnType).SetOrdinal(dataTable.Columns.IndexOf(insertAfterColumn) + 1);
                }
                else
                {
                    dataTable.Columns.Add(columnName, columnType);
                }
            }
        }

        // Button click event handler for exporting filtered data
        // Button click event handler for exporting filtered data
        private void exportFilteredOnlyButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the paths of the source files and work file
                string sourceFile1Path = sourceFile1TextBox.Text;
                string sourceFile2Path = sourceFile2TextBox.Text;
                string sourceFile3Path = sourceFile3TextBox.Text;
                string workFilePath = workFileTextBox.Text;

                // Load the source files and work file into DataTables
                DataTable sourceFile1Data = LoadExcelFile(sourceFile1Path);
                DataTable sourceFile2Data = LoadExcelFile(sourceFile2Path);
                DataTable sourceFile3Data = LoadExcelFile(sourceFile3Path);
                DataTable workFileData = LoadExcelFile(workFilePath);

                // Create a new DataTable to store the filtered data
                DataTable filteredData = workFileData.Clone();

                // Filter the rows not found in both source files
                foreach (DataRow row in workFileData.Rows)
                {
                    string key = row["Student ID"].ToString();

                    if (!ContainsKey(sourceFile1Data, "Student ID", key) && !ContainsKey(sourceFile2Data, "Student ID", key))
                    {
                        filteredData.ImportRow(row);

                        // Check if the student ID is found in Source File 3
                        if (ContainsKey(sourceFile3Data, "Student ID", key))
                        {
                            // Update "Student Type" and "MEETS or FAILS CREDIT-REQUIREM" columns
                            DataRow filteredRow = filteredData.Rows[filteredData.Rows.Count - 1];
                            filteredRow["Student Type"] = "6-First Term Freshman";
                            filteredRow["MEETS or FAILS CREDIT-REQUIREM"] = "1-Meets first-term freshman credit requirements";
                        }
                    }
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


        // Helper method to load an Excel file into a DataTable
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

        // Helper method to check if a DataTable contains a specific key value
        private bool ContainsKey(DataTable dataTable, string keyColumn, string key)
        {
            return dataTable.AsEnumerable().Any(row => row.Field<string>(keyColumn) == key);
        }

        // Helper method to export a DataTable to an Excel file
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

// Avijit Roy, July 20, 2023