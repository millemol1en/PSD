alias genlex="dotnet ../../fsharp/FsLexYacc.11.3.0/build/fslex/net6.0/fslex.dll --unicode CLex.fsl"
alias genpar="dotnet ../../fsharp/FsLexYacc.11.3.0/build/fsyacc/net6.0/fsyacc.dll --module CPar CPar.fsy"
alias run="dotnet fsi -r ../../fsharp/FsLexYacc.Runtime.11.3.0/lib/netstandard2.0/FsLexYacc.Runtime.dll Util.fs Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs"
alias clean="rm ./CLex.fs ./CLex.fsi ./CPar.fs ./CPar.fsi"