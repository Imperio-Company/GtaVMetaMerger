# GtaVMetaMerger
Tool made to easily merge multiple GTA5 meta files.

Based in the work of [mmleczek](https://github.com/mmleczek) in his resource [mmVehiclesMetaMerger](https://github.com/mmleczek/mmVehiclesMetaMerger), adapted to C# with expanded functionalities.

# Download
Click [here](https://github.com/Imperio-Company/GtaVMetaMerger/releases/latest) to go to the releases page and download it.

# Usage
You need [.net runtime 6.0](https://dotnet.microsoft.com/es-es/download/dotnet/6.0) or higher to run or compile this program
Extract the zip file from the releases section, when you run the `GtaVMetaMerger.exe` file the `output` folder will be created with the output folders of each category.
Select the category with which you want to work and within it the section to work, the path where are the individual resources to unify and proceed to make the union of the same generating the unified file within the output folder in the corresponding category.

# Disclaimer
At the moment the tool only validate that the source files are correctly formed xml files, does not recognize if they comply with the shape of the specific file type of gta.
When merging stream files, if a file with the same name already exists in the destination path, it overwrites it and notifies it.
