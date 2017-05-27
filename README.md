# Statement Analyzer

This is a Visual Studio project that analyzes bank/super fund statements, and creates a Microsoft Excel spreasheet with the provided information. The spreasheet contains the following columns:
1. Transaction_Date
2. Amount
3. Text (the description of the transaction)

This program can be useful to tax/super fund accountants, in the cases that they are given PDF files without the respective spreasheets. Following the accounting standards, debit is depicted as a positive number and the credit as a negative one.

Supported financial institutions:
* Asgard
* ANZ
* Linear
* Westpac

## Getting Started

### Prerequisites

Visual Studio
Microsoft Windows Excel

### Installing

Download/clone the files, and open StatementAnalyzer.sln with Visual Studio.

###Dependencies
* [iTextSharp](https://www.nuget.org/packages/iTextSharp/) - .NET library for parsing PDF files

## Built With

* Visual Studio
* [iTextSharp](https://www.nuget.org/packages/iTextSharp/)

## Authors

**Kostas Karvounis** [kael89](https://github.com/kael89)

## License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE.md](LICENSE.md) file for details