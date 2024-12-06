alias genLex="dotnet ../fsharp/FsLexYacc.11.3.0/build/fslex/net6.0/fslex.dll --unicode ./FunLex.fsl"
alias genPar="dotnet ../fsharp/FsLexYacc.11.3.0/build/fsyacc/net6.0/fsyacc.dll --module FunPar ./FunPar.fsy"
alias run="dotnet fsi -r ../fsharp/FsLexYacc.Runtime.11.3.0/lib/netstandard2.0/FsLexYacc.Runtime.dll Util.fs Absyn.fs FunPar.fs FunLex.fs Parse.fs Fun.fs ParseAndRun.fs"
alias clean="rm FunLex.fs FunLex.fsi FunPar.fs FunPar.fsi"