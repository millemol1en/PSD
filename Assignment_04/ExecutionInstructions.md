### The (`.zshrc`) file
Use the command (`source .zshrc`) to instantiate the shell file, making it so you can just write (`fslex`) and (`fsyacc`)
to run initiate the lexer and parser creation.

#### Command to create ExprPar.fs file:
`fsyacc --module FunPar FunPar.fsy`
#### Command to create ExprLex.fs file: 
`fslex --unicode FunLex.fsl`
#### Command to run is:
`dotnet fsi -r ../fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs Fun.fs ParseAndRun.fs`
#### Command to open the environment - write in Terminal:
`open ParseAndRun`
#### To quit the environment:
`#q;;`