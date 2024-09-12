module compString

open Expr
open ExprLex
open ExprPar
open Parse

let compString (input: string) : Expr.sinstr list =
    let expr = Parse.fromString input
    Expr.scomp expr []
