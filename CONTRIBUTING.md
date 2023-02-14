# Contributing

Thank you for your interest in contributing to the Digital Twins Definition Language (DTDL) Parser library. As an open source effort, we're excited to welcome feedback and contributions from the community.

## Project Overview

This repository holds sources and resources for software artifacts related to parsing, validating, transforming, and inspecting models written in DTDL.

This repository does not accept changes to the language definition itself.
It accepts changes to the codebase that are independent of the language, such as software architecture, documentation, and portions of the parser API.

## Folder Structure

### Top-Level Folders

The main top-level folders in the repository are as follows:

* :file_folder: **dtdl** - Declarative definitions of the current and next versions of DTDL.
* :open_file_folder: **dotnet**
  * :file_folder: **gen** - Source code of utilities that perform extraction, codegen, and analysis that produces other files and projects.
  * :file_folder: **src** - Source code (both handwritten and generated) of the parser and tools.
  * :file_folder: **tests** - Source code (both handwritten and generated) of tests that validate correctness of the parser and sub-components.
* :file_folder: **test-cases** - Declarative tests cases for validating DTDL parsing.
* :open_file_folder: **samples** - Markdown files illustrating use of the parser.
  * :file_folder: **projects** - C# project, source, and expectation files generated from the Markdown samples.
* :file_folder: **scripts** - CMD files used by the [Develop.sh](#building-the-projects) script.

### Generated (Read-Only) Code

The following folders contain generated files and inputs that feed into the production of generated files.
PRs shall not include changes to files within:

* :file_folder: **dtdl**
* :file_folder: **test-cases**
* :file_folder: **pipelines**
* :file_folder: **scripts**
* :file_folder: **dotnet/gen**
* :file_folder: **dotnet/src/Parser/generated**
* :file_folder: **dotnet/src/Remodel/generated**
* :file_folder: **dotnet/tests/ParserUnitTest/generated**
* :file_folder: **images/generated**
* :file_folder: **images/input/generated**
* :file_folder: **samples/projects**
* :file_folder: **api-docs**

## Building the Projects

The projects can be built with the following command:

```Console
dotnet build dotnet/DTDLParser.sln
```

Once built, the projects can be tested with the following commands:

```Console
dotnet test dotnet/tests/DtmiUnitTest 
dotnet test dotnet/tests/ResultFormatterUnitTest
dotnet test dotnet/tests/ParserUnitTest
```

## Code Change Sequence

The following steps will be taken to submit changes to this repository:

1. Create a fork of the repository.
1. Modify code and/or other resource files.
1. Execute the commands described above to build projects and execute tests.
1. Create a PR back to this repository.
1. Review pipelines failures and make appropriate changes.

## Further Reading

For details on contributing to DTDL Parser development via changes to generation code, please refer to [CONTRIBUTING.detailed][contributing_detailed]

<!-- LINKS -->
[contributing_detailed]: CONTRIBUTING.detailed.md
