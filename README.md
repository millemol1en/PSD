# PSD

Assignmet_03

## How to run fslex

### intented from book: fslex--unicode ExprLex.fsl

### Ours, with relative path from root because we havent setup the short cuts

dotnet fsharp\FsLexYacc.11.3.0\build\fslex\net6.0\fslex.dll --unicode Assignment_03\Expr\ExprLex.fsl

## How to run fsyacc

## intented from book: fsyacc--module ExprPar ExprPar.fsy

### Ours, with relative path from root because we havent setup the short cuts

dotnet fsharp/FsLexYacc.11.3.0/build/fsyacc/net6.0/fsyacc.dll --module ExprPar Assignment_03\Expr\ExprPar.fsy

fsi-r FSharp.PowerPack.dll Absyn.fs ExprPar.fs ExprLex.fs ^
Parse.fs

dotnet fsi -r fsharp/FsLexYacc.Runtime.11.3.0/lib/netstandard2.0/FsLexYacc.Runtime.dll Assignment_03/Expr/Absyn.fs Assignment_03/Expr/ExprPar.fs Assignment_03/Expr/ExprLex.fs Assignment_03/Expr/Parse.fs
dotnet fsi -r fsharp/FsLexYacc.Runtime.11.3.0/lib/netstandard2.0/FsLexYacc.Runtime.dll Assignment_03/Expr/Absyn.fs Assignment_03/Expr/ExprPar.fs Assignment_03/Expr/ExprLex.fs Assignment_03/Expr/Parse.fs Assignment_03\Expr\compString.fs
