# Student Data Comparison and Verification

This program automates the comparison of student data based on their ID between the Master File and the Pending Verification file. It is developed using .NET Framework 6.0 and Visual Studio 2019 (version 17.6.4).

## Features

### Compare and Export
The "Compare and Export" feature compares student data from the Master File and the Pending Verification file based on their ID. It creates a new file that indicates whether each student is found in the Master File, the Pending Verification file, or if they require new verification.

### Filtered Export
The "Filtered Export" feature allows you to filter out rows that are not found in either the Master File or the Pending Verification file. It creates a new file with only the filtered rows, making it easier to focus on the students that require verification.  Additionally, it adds additional columns to the work file with relevant information. 

### Duplicate Row Finder
This program can also be modified to function as a duplicate row finder within the current work file by using the Student ID as the key. It compares the rows from the Master File and the Pending Verification file to identify any duplicate entries based on matching Student IDs.

## System Requirements
- .NET Framework 6.0 or higher
- Visual Studio 2019 (version 17.6.4) or higher

## Getting Started
1. Clone or download the repository.
2. Open the project in Visual Studio.
3. Build the solution to ensure all dependencies are resolved.
4. Run the application.

## License
This project is licensed under the Apache License 2.0. See the [LICENSE](LICENSE) file for more details.

## Contribution
Contributions to this project are welcome. Feel free to open issues and submit pull requests.

If you have any questions or need further assistance, please don't hesitate to reach out.

Enjoy using the Student Data Comparison and Verification program!
