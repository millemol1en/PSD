alias genlex="dotnet ../../fsharp/fslex.dll --unicode CLex.fsl"
alias genpar="dotnet ../../fsharp/fsyacc.dll --module CPar CPar.fsy"
alias run="dotnet fsi -r ../../fsharp/FsLexYacc.Runtime.dll Util.fs Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs"
alias clean="rm ./CLex.fs ./CLex.fsi ./CPar.fs ./CPar.fsi"