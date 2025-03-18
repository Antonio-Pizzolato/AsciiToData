# BinaryConverter

BinaryConverter is a .NET application designed to parse text files containing various types of records (L, B, P, R) and convert them into structured binary files (.lav, .bar, .pez, .res). The project makes extensive use of fixed-length structures with precise marshalling and manual field serialization in UTF-16 LE to ensure that the output binary files conform to an exact format specification.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Building the Project](#building-the-project)
- [Usage](#usage)
- [Customization](#customization)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## Overview

The **BinaryConverter** project processes a text file that contains multiple records identified by a leading character:
- **L** – LAVORO records
- **B** – BARRA records
- **P** – PEZZO records
- **R** – PEZZO_RESTANTE records

Each record type is defined as a fixed-length structure with fields (strings, numbers, arrays) that are serialized into binary format. The application uses helper classes for safe parsing of input fields and specialized writer classes to convert the data into binary files that adhere strictly to predefined specifications.

---

## Features

- **Robust Parsing:**  
  Utilizes helper methods (e.g., in `ParserUtils`) for safe extraction and conversion of fields from the input text file.

- **Fixed-Length Structures:**  
  Structures are defined using `[StructLayout(LayoutKind.Sequential, Pack = 1)]` and `[MarshalAs]` attributes to ensure exact binary layout.

- **Manual Field Serialization:**  
  Custom helper methods (e.g., `WriteFixedString`) guarantee that string fields are written with proper UTF-16 LE encoding and padding.

- **Multi-Record Processing:**  
  The application distinguishes record types (L, B, P, R) and groups them into collections (`ParsedRecords`), which are then written to separate binary output files.

- **Extensible Architecture:**  
  The use of interfaces (e.g., `IRecordProcessor`) allows for future expansion or modification of the record processing logic without impacting the rest of the code.

---

## Project Structure

```
BinaryConverter/
│
├── BinaryConverter.sln           # Visual Studio solution file
│
├── BinaryConverter.Models/       # Contains model classes and structs:
│     ├── DATI_LAVORO.cs          # Model for Lavoro records (L)
│     ├── DATI_BARRA.cs           # Model for Barra records (B)
│     ├── DATI_PEZZO.cs           # Model for Pezzo records (P)
│     └── DATI_PEZZO_RESTANTE.cs  # Model for Residuo records (R)
│
├── BinaryConverter.Services/     # Contains service classes:
│     ├── IRecordProcessor.cs     # Interface for record processing
│     ├── RecordProcessor.cs      # Implementation of the record processor that parses text files
│     ├── ParsedRecords.cs        # Container for parsed records of all types
│     ├── BinaryFileWriter.cs     # Utility for serializing and writing binary files
│     └── BinaryFileReader.cs     # Utility for reading binary files (if needed)
│
└── BinaryConverter.Utils/        # Contains utility classes:
      └── ParserUtils.cs          # Helper methods for safe parsing of text fields
```

---

## Prerequisites

- [.NET SDK 5.0 or later](https://dotnet.microsoft.com/download)
- Visual Studio 2019 (or later) or Visual Studio Code
- Basic knowledge of C# and .NET for building and modifying the project

---

## Building the Project

1. **Clone the Repository:**  
   Clone or download the repository to your local machine.

2. **Open the Solution:**  
   Open `BinaryConverter.sln` in Visual Studio or your preferred IDE.

3. **Restore NuGet Packages:**  
   Restore any required NuGet packages via Visual Studio’s Package Manager (if applicable).

4. **Build the Solution:**  
   Build the solution using the IDE’s build function or run the following command in the project directory:
   ```bash
   dotnet build
   ```

---

## Usage

1. **Prepare the Input File:**  
   The input text file should contain records starting with one of the identifiers (L, B, P, R), with fields separated by commas. Each record type should follow the expected format.

2. **Run the Application:**  
   Execute the application (e.g., via Visual Studio or command line). The `RecordProcessor` class reads the input file, parses each record into its corresponding model, and groups them in a `ParsedRecords` object.

3. **Generate Binary Files:**  
   The service classes (e.g., `BinaryFileWriter`) then serialize the collections of records into binary files:
   - **.lav** for LAVORO records
   - **.bar** for BARRA records
   - **.pez** for PEZZO records
   - **.res** for PEZZO_RESTANTE records

4. **Review Output:**  
   Verify the output using an editor like Notepad++ (with Hex Editor plugin) or HxD to ensure that the binary file matches the expected format.

---

## Contributing

Contributions are welcome! If you have suggestions for improvements, bug fixes, or additional features, please follow these guidelines:

1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Write your changes with clear comments and commit messages.
4. Submit a pull request describing your changes.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Contact

For any questions or support, please contact [antonio.pizzolato@hotmail.com](mailto:antonio.pizzolato@hotmail.com).
