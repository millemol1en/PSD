module PSD.Assignment_02.Absyn

type expr = 
    | CstI of int
    | Var of string
    | Let of string * expr * expr
    | Prim of string * expr * expr