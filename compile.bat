cd Assemblies
del Project.dll
del Project.pdb
del BrushTool.dll
del BrushTool.pdb
cd ..
cd Source
dotnet build
cd ..
cd Assemblies

rename Project.dll BrushTool.dll
rename Project.pdb BrushTool.pdb